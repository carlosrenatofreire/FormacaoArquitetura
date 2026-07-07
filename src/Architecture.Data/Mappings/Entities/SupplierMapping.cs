using Architecture.Business.Models.Internals.Entities;
using Architecture.Business.Models.Internals.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.Data.Mappings.Entities
{
    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("E_Suppliers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Document)
                .IsRequired()
                .HasColumnType("varchar(9)");

            builder.Property(p => p.SupplierType)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.Active)
                .IsRequired()
                .HasColumnType("bit");

            // 1 : 1 relationship between Supplier and Address
            builder.HasOne(s => s.Address)
                .WithOne(a => a.Supplier)
                .HasForeignKey<Address>(a => a.SupplierId);

            // 1 : N relationship between Supplier and Products
            builder.HasMany(s => s.Products)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);

        }
    }
}
