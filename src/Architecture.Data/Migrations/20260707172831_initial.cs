using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Architecture.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "E_Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Document = table.Column<string>(type: "varchar(9)", nullable: false),
                    SupplierType = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_E_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "E_Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_E_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_E_Products_E_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "E_Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "P_Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "varchar(200)", nullable: false),
                    Number = table.Column<string>(type: "varchar(20)", nullable: false),
                    Complement = table.Column<string>(type: "varchar(100)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(20)", nullable: false),
                    Neighborhood = table.Column<string>(type: "varchar(100)", nullable: false),
                    City = table.Column<string>(type: "varchar(100)", nullable: false),
                    State = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_P_Addresses_E_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "E_Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_E_Products_SupplierId",
                table: "E_Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_P_Addresses_SupplierId",
                table: "P_Addresses",
                column: "SupplierId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "E_Products");

            migrationBuilder.DropTable(
                name: "P_Addresses");

            migrationBuilder.DropTable(
                name: "E_Suppliers");
        }
    }
}
