using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Admin.Domain.Users;

[Table("business_user")]
public class BusinessUser : Entity
{
    /// <summary>
    /// 集團識別碼
    /// </summary>
    [Column("corporation_id")]
    public Guid CorporationId { get; set; }

    /// <summary>
    /// 公司識別碼
    /// </summary>
    [Column("company_id")]
    public Guid CompanyId { get; set; }

    /// <summary>
    /// 使用者帳號
    /// </summary>
    [Column("user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// 使用者密碼
    /// </summary>
    [Column("password")]
    public string Password { get; set; }

    /// <summary>
    /// 使用者信箱
    /// </summary>
    [Column("email")]
    public string Email { get; set; }

    /// <summary>
    /// 使用者名稱
    /// </summary>
    [Column("name")]
    public string Name { get; set; }
}
