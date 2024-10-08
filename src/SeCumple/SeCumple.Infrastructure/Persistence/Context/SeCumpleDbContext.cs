using Microsoft.EntityFrameworkCore;
using SeCumple.CrossCutting.Entities;
using SeCumple.Domain.Entities;

namespace SeCumple.Infrastructure.Persistence.Context;

public class SeCumpleDbContext : DbContext
{
    public SeCumpleDbContext() { }

    public SeCumpleDbContext(DbContextOptions<SeCumpleDbContext> options):base(options) { }
    
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