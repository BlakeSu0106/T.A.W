using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Application.Dtos.Company;

public class CreateCompanyDto : EntityDto
{
    internal new Guid Id { get; set; }

    public Guid CorporationId { get; set; }

    public string Name { get; set; }

    public string code { get; set; }

    public string Description { get; set; }
}
