using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthPermissions.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByTenantIdInRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyToken",
                schema: "authp",
                table: "RoleToPermissionsTenant");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByTenantId",
                schema: "authp",
                table: "RoleToPermissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleToPermissions_CreatedByTenantId",
                schema: "authp",
                table: "RoleToPermissions",
                column: "CreatedByTenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleToPermissions_RoleName_CreatedByTenantId",
                schema: "authp",
                table: "RoleToPermissions",
                columns: new[] { "RoleName", "CreatedByTenantId" },
                unique: true,
                filter: "[CreatedByTenantId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleToPermissions_Tenants_CreatedByTenantId",
                schema: "authp",
                table: "RoleToPermissions",
                column: "CreatedByTenantId",
                principalSchema: "authp",
                principalTable: "Tenants",
                principalColumn: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleToPermissions_Tenants_CreatedByTenantId",
                schema: "authp",
                table: "RoleToPermissions");

            migrationBuilder.DropIndex(
                name: "IX_RoleToPermissions_CreatedByTenantId",
                schema: "authp",
                table: "RoleToPermissions");

            migrationBuilder.DropIndex(
                name: "IX_RoleToPermissions_RoleName_CreatedByTenantId",
                schema: "authp",
                table: "RoleToPermissions");

            migrationBuilder.DropColumn(
                name: "CreatedByTenantId",
                schema: "authp",
                table: "RoleToPermissions");

            migrationBuilder.AddColumn<byte[]>(
                name: "ConcurrencyToken",
                schema: "authp",
                table: "RoleToPermissionsTenant",
                type: "ROWVERSION",
                rowVersion: true,
                nullable: true);
        }
    }
}
