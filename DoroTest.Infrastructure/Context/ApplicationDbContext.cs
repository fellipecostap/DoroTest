using DoroTest.Domain.Common;
using DoroTest.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DoroTest.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<UserEntity> User { get; set; }
    public DbSet<RefreshTokenEntity> RefreshToken { get; set; }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == "id")?.Value;
                    entry.Entity.Created = DateTime.UtcNow;
                    break;
                    
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == "id")?.Value;
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
