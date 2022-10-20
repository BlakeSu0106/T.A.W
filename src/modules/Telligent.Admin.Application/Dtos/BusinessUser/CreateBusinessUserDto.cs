using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Application.Dtos.BusinessUser;

public class CreateBusinessUserDto : EntityDto
{
    internal new Guid Id { get; set; }

    public Guid CorporationId { get; set; }

    public Guid CompanyId { get; set; }

    public string UserId { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }
}
