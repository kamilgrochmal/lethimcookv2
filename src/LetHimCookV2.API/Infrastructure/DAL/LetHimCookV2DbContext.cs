using LetHimCookV2.API.Domain.Catalogs;
using LetHimCookV2.API.Domain.Documents;
using LetHimCookV2.API.Domain.Patients;
using LetHimCookV2.API.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LetHimCookV2.API.Infrastructure.DAL;

public sealed class LetHimCookV2DbContext : DbContext
{
    public LetHimCookV2DbContext(DbContextOptions<LetHimCookV2DbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<User> Users { get; set; }
}