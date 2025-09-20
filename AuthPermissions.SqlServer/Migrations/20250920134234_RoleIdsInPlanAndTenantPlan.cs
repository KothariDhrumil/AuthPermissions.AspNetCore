using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthPermissions.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RoleIdsInPlanAndTenantPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantPlans_Tenants_TenantId",
                schema: "authp",
                table: "TenantPlans");

            migrationBuilder.DropIndex(
                name: "IX_TenantPlans_TenantId",
                schema: "authp",
                table: "TenantPlans");

            migrationBuilder.DropColumn(
                name: "Permissions",
                schema: "authp",
                table: "TenantPlans");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "authp",
                table: "TenantPlans");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "authp",
                table: "RoleToPermissions");

            migrationBuilder.DropColumn(
                name: "Features",
                schema: "authp",
                table: "Plans");

            migrationBuilder.CreateTable(
                name: "PlanToRoles",
                schema: "authp",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanToRoles", x => new { x.PlanId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PlanToRoles_Plans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "authp",
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanToRoles_RoleToPermissions_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "authp",
                        principalTable: "RoleToPermissions",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantPlans_TenentId",
                schema: "authp",
                table: "TenantPlans",
                column: "TenentId",
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_PlanToRoles_RoleId",
                schema: "authp",
                table: "PlanToRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantPlans_Tenants_TenentId",
                schema: "authp",
                table: "TenantPlans",
                column: "TenentId",
                principalSchema: "authp",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantPlans_Tenants_TenentId",
                schema: "authp",
                table: "TenantPlans");

            migrationBuilder.DropTable(
                name: "PlanToRoles",
                schema: "authp");

            migrationBuilder.DropIndex(
                name: "IX_TenantPlans_TenentId",
                schema: "authp",
                table: "TenantPlans");

            migrationBuilder.AddColumn<string>(
                name: "Permissions",
                schema: "authp",
                table: "TenantPlans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                schema: "authp",
                table: "TenantPlans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                schema: "authp",
                table: "RoleToPermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Features",
                schema: "authp",
                table: "Plans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TenantPlans_TenantId",
                schema: "authp",
                table: "TenantPlans",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_TenantPlans_Tenants_TenantId",
                schema: "authp",
                table: "TenantPlans",
                column: "TenantId",
                principalSchema: "authp",
                principalTable: "Tenants",
                principalColumn: "TenantId");
        }
    }
}
