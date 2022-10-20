using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Application.Dtos.Company;

public class CompanyDto : EntityDto
{
    public Guid TenantId { get; set; }

    public Guid CorporationId { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
