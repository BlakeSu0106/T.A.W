using AutoMapper;
using Microsoft.AspNetCore.Http;
using Telligent.Admin.Application.Dtos.AdminUser;
using Telligent.Admin.Domain.Users;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;

namespace Telligent.Admin.Application.AppServices;

public class AdminUserAppService : CrudAppService<AdminUser,AdminUserDto, AdminUserDto, AdminUserDto>
{
    private readonly UnitOfWork _uow;

    public AdminUserAppService(
        IRepository<AdminUser> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        UnitOfWork uow)
        : base(repository, mapper, httpContextAccessor)
    {
        _uow = uow;
    }
    
    public async Task<AdminUserDto> GetAdminAsync(string id)
    {
        var userDto = await GetAsync(m => m.Id.ToString().Equals(id)
                                          && m.EntityStatus);

        return userDto;
    }
}
