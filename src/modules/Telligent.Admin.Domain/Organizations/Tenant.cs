using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Admin.Domain.Organizations;

[Table("tenant")]
public class Tenant : Entity
{
    /// <summary>
    /// 租戶名稱
    /// </summary>
    [Column("name")]
    public string Name { get; set; }
}
