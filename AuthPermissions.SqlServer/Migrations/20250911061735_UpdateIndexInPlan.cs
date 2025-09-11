using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthPermissions.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndexInPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "authp",
                table: "Plans",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantPlans_TenentId_IsActive",
                schema: "authp",
                table: "TenantPlans",
                columns: new[] { "TenentId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_Name",
                schema: "authp",
                table: "Plans",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TenantPlans_TenentId_IsActive",
                schema: "authp",
                table: "TenantPlans");

            migrationBuilder.DropIndex(
                name: "IX_Plans_Name",
                schema: "authp",
                table: "Plans");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "authp",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
