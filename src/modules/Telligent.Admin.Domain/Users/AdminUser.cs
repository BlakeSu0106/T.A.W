using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Admin.Domain.Users;

[Table("admin_user")]
public class AdminUser : Entity
{
    /// <summary>
    /// 管理者帳號
    /// </summary>
    [Column("user_id")]
    public string UserId { get; set; }

    /// <summary>
    /// 管理者密碼
    /// </summary>
    [Column("password")]
    public string Password { get; set; }

    /// <summary>
    /// 管理者信箱
    /// </summary>
    [Column("email")]
    public string Email { get; set; }

    /// <summary>
    /// 管理者名稱
    /// </summary>
    [Column("name")]
    public string Name { get; set; }

}
