using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SeCumple.CrossCutting.Entities;
using SeCumple.Domain.Entities;

namespace SeCumple.Infrastructure.Persistence.Context;

public class SeCumpleDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SeCumpleDbContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public SeCumpleDbContext(DbContextOptions<SeCumpleDbContext> options,IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var clientIp = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        foreach (var entry in ChangeTracker.Entries<Base>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Status = '1';
                    entry.Entity.CreationIp = clientIp;
                    entry.Entity.CreationDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModificationIp = clientIp;
                    entry.Entity.ModificationDate = DateTime.Now;
                    break;
                default:
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    
    public DbSet<Document> Documents { get; init; }
    public DbSet<Parameter> Parameters { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(e =>
        {
            e.HasOne(d=>d.DocumentType)
                .WithMany()
                .HasForeignKey(d=>d.DocumentTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        });
        
        modelBuilder.Entity<Parameter>();
        
        base.OnModelCreating(modelBuilder);
    }
}