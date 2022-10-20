using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Telligent.Admin.Application.Dtos.AdminUser;
using Telligent.Admin.Application.Dtos.Company;
using Telligent.Admin.Domain.Organizations;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Encryption;
using Telligent.Core.Infrastructure.Generators;

namespace Telligent.Admin.Application.AppServices;

public class CompanyAppService : CrudAppService<Company,CompanyDto,CreateCompanyDto,CompanyDto>
{
    private readonly UnitOfWork _uow;
    private readonly AdminUserAppService _adminUserAppService;
    private readonly CorporationAppService _corporationAppService;

    private string _userId;
    private string _token;

    public CompanyAppService(
        IRepository<Company> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        AdminUserAppService adminUserAppService,
        CorporationAppService corporationAppService,
        UnitOfWork uow)
        : base(repository, mapper, httpContextAccessor)
    {
        _uow = uow;
        _adminUserAppService = adminUserAppService;
        _corporationAppService = corporationAppService;

        if (httpContextAccessor.HttpContext == null) return;

        _userId = httpContextAccessor.HttpContext.Request.Headers["User"].ToString();
        //_token = httpContextAccessor.HttpContext.Request.Headers["Authorization"];

        DataInitializeAsync().Wait();
    }

    /// <summary>
    /// 取得全部公司
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public override async Task<IList<CompanyDto>> GetAllAsync()
    {
        if (!Guid.TryParse(_userId, out var creatorId)) throw new ValidationException("無法取得維護人員資訊");

        return await GetListAsync(m => m.EntityStatus);
    }

    /// <summary>
    /// 建立公司
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto dto)
    {
        if (!Guid.TryParse(_userId, out var creatorId)) throw new ValidationException("無法取得維護人員資訊");

        var company = await Repository.GetAsync(c => c.Name.Equals(dto.Name) && c.EntityStatus);
        if (company != null) throw new ValidationException("公司名稱重複");

        var corporation = await _corporationAppService.GetAsync(c => c.Id.Equals(dto.CorporationId) && c.EntityStatus);
        if (corporation == null) throw new ValidationException("無法取得集團資訊");

        var companyEntity = Mapper.Map<Company>(dto);

        companyEntity.Id = SequentialGuidGenerator.Instance.GetGuid();
        companyEntity.TenantId = corporation.TenantId;
        companyEntity.CreatorId = creatorId;

        await _uow.CompanyRepository.CreateAsync(companyEntity);
        await _uow.SaveChangeAsync();

        return await GetAsync(companyEntity.Id);
    }


    private async Task DataInitializeAsync()
    {
        if (!string.IsNullOrEmpty(_userId))
        {
            var userDto = await _adminUserAppService.GetAdminAsync(_userId);

            if (userDto != null)
                _userId = userDto.Id.ToString();
            else
                _userId = null;

        }
    }
}
