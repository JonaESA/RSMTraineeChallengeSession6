using DragonBall.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DragonBall.Infrastructure.Persistence.Configuration
{
    public class CharacterConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable("Characters");

            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Transformations)
                .WithOne(t => t.Character)
                .HasForeignKey(t => t.CharacterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(25);
            builder.Property(c => c.Ki)
                .HasMaxLength(35);
            builder.Property(c => c.Race)
                .HasMaxLength(25);
            builder.Property(c => c.Gender)
                .HasMaxLength(20);
            builder.Property(c => c.Description);
            builder.Property(c => c.Affiliation)
                .HasMaxLength(35);
        }
    }
}
