using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Admin.Domain.Shared;
using Telligent.Core.Domain.Entities;

namespace Telligent.Admin.Domain.Organizations;

[Table("corporation")]
public class Corporation : Entity
{
    /// <summary>
    /// 名稱
    /// </summary>
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// 縮寫名稱
    /// </summary>
    [Column("short_name")]
    public string ShortName { get; set; }

    /// <summary>
    /// 企業型態
    /// </summary>
    [Column("enterprise_type")]
    public EnterpriseType EnterpriseType { get; set; }
}
