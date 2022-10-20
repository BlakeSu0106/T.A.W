using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Telligent.Admin.Application.Dtos.Tenant;
using Telligent.Admin.Domain.Organizations;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Generators;

namespace Telligent.Admin.Application.AppServices;

public class TenantAppService : CrudAppService<Tenant,TenantDto, TenantDto, TenantDto>
{
    private readonly UnitOfWork _uow;
    private readonly AdminUserAppService _adminAppService;

    private string _userId;

    public TenantAppService(
        IRepository<Tenant> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        AdminUserAppService adminAppService,
        UnitOfWork uow)
        : base(repository, mapper, httpContextAccessor)
    {
        _adminAppService = adminAppService;
        _uow = uow;

        if (httpContextAccessor.HttpContext == null) return;

        _userId = httpContextAccessor.HttpContext.Request.Headers["User"].ToString();

        DataInitializeAsync().Wait();
    }

    public async Task<IList<TenantDto>> GetAsync()
    {
        if (!Guid.TryParse(_userId, out var creatorId)) throw new ValidationException("無法取得維護人員資訊");

        return await GetListAsync(m => m.EntityStatus);
    }

    /// <summary>
    /// 建立租戶
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<TenantDto> CreateTenantAsync(CreateTenantDto dto)
    {
        if (!Guid.TryParse(_userId, out var creatorId)) throw new ValidationException("無法取得維護人員資訊");
        
        if (dto.Name == null) throw new ValidationException("無法建立租戶");
        
        var tenant = await Repository.GetAsync(t => t.Name.Equals(dto.Name));
        if (tenant != null) throw new ValidationException("租戶名稱重複");

        var tenantEntity = Mapper.Map<Tenant>(dto);

        tenantEntity.Id = SequentialGuidGenerator.Instance.GetGuid();
        tenantEntity.CreatorId = creatorId;

        await _uow.TenantRepository.CreateAsync(tenantEntity);
        await _uow.SaveChangeAsync();

        return await GetAsync(tenantEntity.Id);
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
