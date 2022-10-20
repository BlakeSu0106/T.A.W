using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Admin.Domain.Organizations;

[Table("company")]
public class Company : Entity
{
    /// <summary>
    /// 集團識別碼
    /// </summary>
    [Column("corporation_id")]
    public Guid CorporationId { get; set; }

    [Column("code")]
    public string Code { get; set; }

    /// <summary>
    /// 名稱
    /// </summary>
    [Column("name")]
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [Column("description")]
    public string Description { get; set; }
}