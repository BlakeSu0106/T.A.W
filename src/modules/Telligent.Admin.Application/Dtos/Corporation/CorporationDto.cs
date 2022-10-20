using Telligent.Admin.Domain.Shared;
using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Application.Dtos.Corporation;

public class CorporationDto : EntityDto
{
    public Guid TenantId { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public EnterpriseType EnterpriseType { get; set; }
}
