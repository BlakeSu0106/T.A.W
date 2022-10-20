using Microsoft.EntityFrameworkCore;
using Telligent.Core.Infrastructure.Database;
using Telligent.Admin.Domain;

namespace Telligent.Admin.Database;

public class MemberDbContext : BaseDbContext
{
    public MemberDbContext(DbContextOptions<MemberDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Company { get; set; }

    public DbSet<Corporation> Corporation { get; set; }

    public DbSet<Tenant> Tenant { get; set; }

    public DbSet<Channel> Channel { get; set; }

    public DbSet<AdminUser> AdminUser { get; set; }

    public DbSet<BusinessUser> BusinessUser { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Tenant>().Ignore(t => t.TenantId);
        builder.Entity<AdminUser>().Ignore(t => t.TenantId);
        base.OnModelCreating(builder);
    }
}
