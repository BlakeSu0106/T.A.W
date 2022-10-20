using AutoMapper;
using Telligent.Admin.Application.Dtos.AdminUser;
using Telligent.Admin.Application.Dtos.Company;
using Telligent.Admin.Application.Dtos.Tenant;
using Telligent.Admin.Application.Dtos.Corporation;
using Telligent.Admin.Application.Dtos.Channel;
using Telligent.Admin.Domain.Channel;
using Telligent.Admin.Domain.Organizations;
using Telligent.Admin.Domain.Users;
using Telligent.Admin.Application.Dtos.BusinessUser;

namespace Telligent.Admin.Application;

public class AdminApplicationAutoMapperProfile : Profile
{
    public AdminApplicationAutoMapperProfile()
    {
        ShouldMapProperty = prop =>
            prop.GetMethod is not null && (prop.GetMethod.IsAssembly || prop.GetMethod.IsPublic);

        CreateMap<AdminUser, AdminUserDto>();
        CreateMap<AdminUserDto, AdminUser>();

        CreateMap<Tenant, TenantDto>();
        CreateMap<TenantDto, Tenant>();
        CreateMap<CreateTenantDto, Tenant>();

        CreateMap<CreateCompanyDto, Company>();
        CreateMap<CompanyDto, Company>();
        CreateMap<Company, CompanyDto>();

        CreateMap<Corporation, CorporationDto>();
        CreateMap<CorporationDto, Corporation>();
        CreateMap<CreateCorporationDto, Corporation>();

        CreateMap<BusinessUser, BusinessUserDto>();
        CreateMap<BusinessUserDto, BusinessUser>();
        CreateMap<CreateBusinessUserDto, BusinessUser>();

        CreateMap<Channel, ChannelDto>();
        CreateMap<ChannelDto, Channel>();
        CreateMap<CreateChannelDto, Channel>();
    }
}
