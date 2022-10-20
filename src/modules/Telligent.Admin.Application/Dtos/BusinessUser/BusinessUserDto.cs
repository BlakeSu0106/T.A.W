using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Application.Dtos.BusinessUser;

public class BusinessUserDto : EntityDto
{
    public string UserId { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }
}
