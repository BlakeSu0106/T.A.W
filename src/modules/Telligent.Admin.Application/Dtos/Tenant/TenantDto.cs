using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Application.Dtos.Tenant;

public class TenantDto : EntityDto
{
    /// <summary>
    /// 租戶名稱
    /// </summary>
    public string Name { get; set; }
}
