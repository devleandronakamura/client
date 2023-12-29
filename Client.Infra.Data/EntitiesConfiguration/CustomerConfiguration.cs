using Client.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Client.Infra.Data.EntitiesConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.FirstName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.LastName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(p => p.Address)
                   .HasMaxLength(500);

            builder.Property(p => p.DocumentNumber)
                   .HasMaxLength(15);
        }
    }
}
