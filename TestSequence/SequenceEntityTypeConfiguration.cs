using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestSequence
{
    public class SequenceEntityTypeConfiguration
   : IEntityTypeConfiguration<Sequence>
    {
        public void Configure(EntityTypeBuilder<Sequence> sequenceBuilder)
        {
            sequenceBuilder.ToTable("Sequences");

            sequenceBuilder.HasKey(item => item.LastNumber);

            sequenceBuilder.Property(item => item.StoreId)
                .HasMaxLength(50)
                .IsRequired();

            sequenceBuilder.Property(item => item.Type)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}