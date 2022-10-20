using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Telligent.Admin.Application.Dtos.BusinessUser;
using Telligent.Admin.Domain.Users;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Encryption;
using Telligent.Core.Infrastructure.Generators;

namespace Telligent.Admin.Application.AppServices;

public class BusinessUserAppService : CrudAppService<BusinessUser, BusinessUserDto, CreateBusinessUserDto, BusinessUserDto>
{
    private readonly UnitOfWork _uow;
    private readonly CorporationAppService _corporationAppService;
    private readonly CompanyAppService _companyAppService;

    public BusinessUserAppService(
        IRepository<BusinessUser> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        CorporationAppService corporationAppService,
        CompanyAppService companyAppService,
        UnitOfWork uow)
        : base(repository, mapper, httpContextAccessor)
    {
        _uow = uow;
        _corporationAppService = corporationAppService;
        _companyAppService = companyAppService;

    }

    /// <summary>
    /// 建立企業後臺使用者帳號
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<BusinessUserDto> CreateBusinessUserAsync(CreateBusinessUserDto dto)
    {

        var corporation = await _corporationAppService.GetAsync(c => c.Id.Equals(dto.CorporationId));
        if (corporation == null) throw new ValidationException("無法取得集團資訊");

        var company = await _companyAppService.GetAsync(c => c.Id.Equals(dto.CompanyId));
        if (company == null) throw new ValidationException("無法取得公司資訊");

        var sha1Password = EncryptionHelper.EncryptSha1(dto.Password);

        var businessUserDto = await GetAsync(m => m.UserId.Equals(dto.UserId)
                                           || m.Email.Equals(dto.Email));

        if (businessUserDto != null) throw new ValidationException("帳號已重複");

        var businessUserEntity = Mapper.Map<BusinessUser>(dto);

        businessUserEntity.Id = SequentialGuidGenerator.Instance.GetGuid();
        businessUserEntity.TenantId = corporation.TenantId;
        businessUserEntity.Password = sha1Password;
        businessUserEntity.CreatorId = businessUserEntity.Id;

        await _uow.BusinessUserRepository.CreateAsync(businessUserEntity);
        await _uow.SaveChangeAsync();

        return await GetAsync(businessUserEntity.Id);
    }
}
