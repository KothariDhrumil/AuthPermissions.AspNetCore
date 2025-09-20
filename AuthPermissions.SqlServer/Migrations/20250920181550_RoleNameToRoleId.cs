using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthPermissions.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RoleNameToRoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TenantPlanRoles",
                schema: "authp",
                columns: table => new
                {
                    TenantPlanId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantPlanRoles", x => new { x.TenantPlanId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TenantPlanRoles_RoleToPermissions_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "authp",
                        principalTable: "RoleToPermissions",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantPlanRoles_TenantPlans_TenantPlanId",
                        column: x => x.TenantPlanId,
                        principalSchema: "authp",
                        principalTable: "TenantPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantPlanRoles_RoleId",
                schema: "authp",
                table: "TenantPlanRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantPlanRoles",
                schema: "authp");
        }
    }
}
