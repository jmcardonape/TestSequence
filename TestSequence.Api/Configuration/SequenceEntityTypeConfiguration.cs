using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestSequence.Api.Model;

namespace TestSequence.Api.Configuration
{
    public class SequenceEntityTypeConfiguration
   : IEntityTypeConfiguration<Sequence>
    {
        public void Configure(EntityTypeBuilder<Sequence> sequenceBuilder)
        {
            sequenceBuilder.ToTable("Sequences");

            sequenceBuilder.HasKey(item => item.Id);

            sequenceBuilder.Property(item => item.LastNumber)
                .IsRequired();

            sequenceBuilder.Property(item => item.StoreId)
                .HasMaxLength(50)
                .IsRequired();

            sequenceBuilder.Property(item => item.Type)
                .HasMaxLength(200)
                .IsRequired();

            sequenceBuilder.HasIndex(item => new { item.StoreId, item.Type })
                .IsUnique();
        }
    }
}