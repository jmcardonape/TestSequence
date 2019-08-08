using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestSequence.Api.Model;

namespace TestSequence.Api.Configuration
{
    public class CreditEntityTypeConfiguration
   : IEntityTypeConfiguration<Credit>
    {
        public void Configure(EntityTypeBuilder<Credit> creditBuilder)
        {
            creditBuilder.ToTable("Credits");

            creditBuilder.HasKey(item => item.Id);

            creditBuilder.Property(item => item.CreditNumber)
                .IsRequired();

            creditBuilder.Property(item => item.StoreId)
                .HasMaxLength(50)
                .IsRequired();

            creditBuilder.HasIndex(item => new { item.CreditNumber, item.StoreId })
                .IsUnique();
        }
    }
}