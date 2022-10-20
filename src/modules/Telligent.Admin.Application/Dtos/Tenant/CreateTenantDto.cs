using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Application.Dtos.Tenant;

public class CreateTenantDto : EntityDto
{
    internal new Guid Id { get; set; }
    /// <summary>
    /// 租戶名稱
    /// </summary>
    public string Name { get; set; }
}
