using Telligent.Admin.Domain.Channel;
using Telligent.Admin.Domain.Organizations;
using Telligent.Admin.Domain.Users;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Database;

namespace Telligent.Admin.Application;

public class UnitOfWork : IDisposable
{
    private bool _disposed;

    public UnitOfWork(
        BaseDbContext context,
        IRepository<Tenant> tenantRepository,
        IRepository<Company> companyRepository,
        IRepository<Corporation> corporationRepository,
        IRepository<Channel> channelRepository,
        IRepository<AdminUser> adminUserRepository,
        IRepository<BusinessUser> businessUserRepository)
    {
        Context = context;
        TenantRepository = tenantRepository;
        CompanyRepository = companyRepository;
        CorporationRepository = corporationRepository;
        ChannelRepository = channelRepository;
        AdminUserRepository = adminUserRepository;
        BusinessUserRepository = businessUserRepository;
    }

    public IRepository<Tenant> TenantRepository { get; }

    public IRepository<Company> CompanyRepository { get; }

    public IRepository<Corporation> CorporationRepository { get; }

    public IRepository<Channel> ChannelRepository { get; }

    public IRepository<AdminUser> AdminUserRepository { get; }

    public IRepository<BusinessUser> BusinessUserRepository { get; }

    public BaseDbContext Context { get; private set; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (! _disposed)
            if (disposing)
            {
                Context.Dispose();
                Context = null;
            }
        _disposed = true;
    }

    public async Task<int> SaveChangeAsync()
    {
        return await Context.SaveChangesAsync();
    }
}
