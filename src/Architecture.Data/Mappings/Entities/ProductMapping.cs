using Architecture.Business.Models.Internals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Data.Mappings.Entities
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("E_Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.SupplierId)
                .IsRequired()
                .HasColumnType("uniqueidentifier");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.RegisterDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(p => p.Active)
                .IsRequired()
                .HasColumnType("bit");


        }
    }
}
