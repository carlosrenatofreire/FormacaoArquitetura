using Architecture.Business.Models.Internals.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Architecture.Data.Mappings.Parameters
{
    internal class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("P_Addresses");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Street)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Number)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.Complement)
                .HasColumnType("varchar(100)");

            builder.Property(p => p.PostalCode)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.Neighborhood)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.City)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.State)
                .IsRequired()
                .HasColumnType("varchar(100)");

        }
    }
}
