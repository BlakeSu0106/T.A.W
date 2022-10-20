using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Admin.Domain.Shared;
using Telligent.Core.Domain.Entities;

namespace Telligent.Admin.Domain.Channel;

[Table("channel")]
public class Channel : Entity
{
    /// <summary>
    /// 公司識別碼
    /// </summary>
    [Column("company_id")]
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 渠道類別
    /// </summary>
    [Column("channel_type")]
    public ChannelType ChannleType { get; set; }

    /// <summary>
    /// 渠道名稱
    /// </summary>
    [Column("name")]
    public string Name { get; set; }
}
