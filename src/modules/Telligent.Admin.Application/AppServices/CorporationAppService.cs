using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Telligent.Admin.Application.Dtos.Corporation;
using Telligent.Admin.Domain.Organizations;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Generators;

namespace Telligent.Admin.Application.AppServices;

public class CorporationAppService : CrudAppService<Corporation,CorporationDto,CreateCorporationDto, CorporationDto>
{
    private readonly UnitOfWork _uow;
    private readonly AdminUserAppService _adminAppService;
    private readonly TenantAppService _tenantAppService;

    private string _userId;

    public CorporationAppService(
        IRepository<Corporation> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        AdminUserAppService adminAppService,
        TenantAppService tenantAppService,
        UnitOfWork uow)
        : base(repository, mapper, httpContextAccessor)
    {
        _uow = uow;
        _adminAppService = adminAppService;
        _tenantAppService = tenantAppService;

        if (httpContextAccessor.HttpContext == null) return;

        _userId = httpContextAccessor.HttpContext.Request.Headers["User"].ToString();

        DataInitializeAsync().Wait();
    }

    /// <summary>
    /// 取得全部集團
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<IList<CorporationDto>> GetAsync()
    {
        if (!Guid.TryParse(_userId, out var creatorId)) throw new ValidationException("無法取得維護人員資訊");

        return await GetListAsync(m => m.EntityStatus);
    }

    /// <summary>
    /// 建立集團
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<CorporationDto> CreateCorporationAsync(CreateCorporationDto dto)
    {
        if (!Guid.TryParse(_userId, out var creatorId)) throw new ValidationException("無法取得維護人員資訊");
        
        var corporation = await Repository.GetAsync(c => c.Name.Equals(dto.Name));
        if (corporation != null) throw new ValidationException("集團名稱重複");

        var tenant = await _tenantAppService.GetAsync(t => t.Id.Equals(dto.TenantId));
        if (tenant == null) throw new ValidationException("無法取得租戶資訊");

        var corporationEntity = Mapper.Map<Corporation>(dto);
        
        corporationEntity.Id = SequentialGuidGenerator.Instance.GetGuid();
        corporationEntity.CreatorId = creatorId;

        await _uow.CorporationRepository.CreateAsync(corporationEntity);
        await _uow.SaveChangeAsync();

        return await GetAsync(corporationEntity.Id);
    }

    private async Task DataInitializeAsync()
    {
        if (!string.IsNullOrEmpty(_userId))
        {
            var userDto = await _adminAppService.GetAdminAsync(_userId);

            if (userDto != null)
                _userId = userDto.Id.ToString();
            else
                _userId = null;

        }
    }
}
