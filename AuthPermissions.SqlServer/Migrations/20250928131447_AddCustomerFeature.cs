using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthPermissions.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerAccounts",
                schema: "authp",
                columns: table => new
                {
                    GlobalCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    GlobalUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ConcurrencyToken = table.Column<byte[]>(type: "ROWVERSION", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccounts", x => x.GlobalCustomerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTenantLinks",
                schema: "authp",
                columns: table => new
                {
                    CustomerTenantLinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlobalCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyToken = table.Column<byte[]>(type: "ROWVERSION", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTenantLinks", x => x.CustomerTenantLinkId);
                    table.ForeignKey(
                        name: "FK_CustomerTenantLinks_CustomerAccounts_GlobalCustomerId",
                        column: x => x.GlobalCustomerId,
                        principalSchema: "authp",
                        principalTable: "CustomerAccounts",
                        principalColumn: "GlobalCustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccounts_GlobalUserId",
                schema: "authp",
                table: "CustomerAccounts",
                column: "GlobalUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccounts_PhoneNumber",
                schema: "authp",
                table: "CustomerAccounts",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTenantLinks_GlobalCustomerId_TenantId",
                schema: "authp",
                table: "CustomerTenantLinks",
                columns: new[] { "GlobalCustomerId", "TenantId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerTenantLinks_TenantId",
                schema: "authp",
                table: "CustomerTenantLinks",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerTenantLinks",
                schema: "authp");

            migrationBuilder.DropTable(
                name: "CustomerAccounts",
                schema: "authp");
        }
    }
}
