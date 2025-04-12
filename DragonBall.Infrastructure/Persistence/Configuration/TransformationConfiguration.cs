using DragonBall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DragonBall.Infrastructure.Persistence.Configuration
{
    internal class TransformationConfiguration : IEntityTypeConfiguration<Transformation>
    {
        public void Configure(EntityTypeBuilder<Transformation> builder)
        {
            builder.ToTable("Transformations");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(25);
            builder.Property(t => t.Ki)
                .HasMaxLength(35);
        }
    }
}
