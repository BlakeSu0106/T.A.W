using Telligent.Admin.Domain.Shared;
using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Application.Dtos.Corporation;

public class CreateCorporationDto : EntityDto
{
    internal new Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public EnterpriseType EnterpriseType { get; set; }
}
