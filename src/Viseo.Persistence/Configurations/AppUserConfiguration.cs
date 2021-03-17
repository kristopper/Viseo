using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Viseo.Domain.Entities;

namespace Viseo.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUser");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(i => i.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(i => i.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(i => i.UserName)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(i => i.CreatedBy)
                .WithMany(r => r.CreatedUsers)
                .HasForeignKey(o => o.CreatedById);

            builder.HasOne(i => i.LastModifiedBy)
                .WithMany(r => r.ModifiedUsers)
                .HasForeignKey(o => o.LastModifiedById);
        }
    }
}