using Microsoft.EntityFrameworkCore;
using SeCumple.CrossCutting.Entities;
using SeCumple.Domain.Entities;

namespace SeCumple.Infrastructure.Persistence.Context;

public class SeCumpleDbContext : DbContext
{
    public SeCumpleDbContext()
    {
        
    }

    public SeCumpleDbContext(DbContextOptions<SeCumpleDbContext> options):base(options)
    {
        
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<Base>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreationDate = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModificationDate = DateTime.Now;
                    break;
                default:
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    
    public DbSet<Document> Documents { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>().ToTable("MaeDispositivo");
        base.OnModelCreating(modelBuilder);
    }
}