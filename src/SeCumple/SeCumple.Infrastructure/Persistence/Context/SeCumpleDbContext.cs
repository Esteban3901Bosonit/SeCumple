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

    public SeCumpleDbContext(DbContextOptions<SeCumpleDbContext> options, IHttpContextAccessor httpContextAccessor) :
        base(options)
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
            e.HasOne(d => d.DocumentType)
                .WithMany()
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Plan>(e =>
        {
            e.HasOne(p => p.Document)
                .WithMany()
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasMany(p=>p.Assigments)
                .WithOne(a=>a.Plan)
                .HasForeignKey(a=>a.PlanId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Parameter>();
        modelBuilder.Entity<Assignment>();
        modelBuilder.Entity<ParameterDetail>();
        modelBuilder.Entity<Axis>();
        modelBuilder.Entity<Sector>();
        modelBuilder.Entity<InterventionAssignment>();
        modelBuilder.Entity<Plan>();
        modelBuilder.Entity<Intervention>(e =>
        {
            e.HasOne(i=>i.GuideLine).WithMany()
                .HasForeignKey(i=>i.GuidelineId)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne(i=>i.OrganicUnit).WithMany()
                .HasForeignKey(i=>i.OrganicUnitId)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne(i=>i.OrganicUnit).WithMany()
                .HasForeignKey(i=>i.OrganicUnitId)
                .OnDelete(DeleteBehavior.NoAction);
        });
        modelBuilder.Entity<GuideLine>(e =>
        {
            e.HasOne(i=>i.Axis).WithMany()
                .HasForeignKey(i=>i.AxisId)
                .OnDelete(DeleteBehavior.NoAction);
        });
        modelBuilder.Entity<OrganicUnit>(e =>
        {
            e.HasOne(i=>i.Sector).WithMany()
                .HasForeignKey(i=>i.SectorId)
                .OnDelete(DeleteBehavior.NoAction);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}