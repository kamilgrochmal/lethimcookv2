using LetHimCookV2.API.Domain.Patients;
using LetHimCookV2.API.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetHimCookV2.API.Infrastructure.DAL.Configurations;

internal sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(a => a.PatientId);
        builder.Property(a => a.PatientId)
            .HasConversion(a => a.Id, a => new PatientId(a)).ValueGeneratedOnAdd();
        builder.Property(a => a.UserId).HasConversion(a => a.Value, a => new UserId(a));
    }
}