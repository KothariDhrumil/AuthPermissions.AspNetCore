using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthPermissions.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantInRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleToPermissionsTenant_RoleToPermissions_TenantRolesRoleName",
                schema: "authp",
                table: "RoleToPermissionsTenant");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToRoles_RoleToPermissions_RoleName",
                schema: "authp",
                table: "UserToRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToRoles",
                schema: "authp",
                table: "UserToRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserToRoles_RoleName",
                schema: "authp",
                table: "UserToRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleToPermissionsTenant",
                schema: "authp",
                table: "RoleToPermissionsTenant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleToPermissions",
                schema: "authp",
                table: "RoleToPermissions");

            migrationBuilder.DropColumn(
                name: "RoleName",
                schema: "authp",
                table: "UserToRoles");

            migrationBuilder.DropColumn(
                name: "TenantRolesRoleName",
                schema: "authp",
                table: "RoleToPermissionsTenant");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                schema: "authp",
                table: "UserToRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantRolesRoleId",
                schema: "authp",
                table: "RoleToPermissionsTenant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                schema: "authp",
                table: "RoleToPermissions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                schema: "authp",
                table: "RoleToPermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToRoles",
                schema: "authp",
                table: "UserToRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleToPermissionsTenant",
                schema: "authp",
                table: "RoleToPermissionsTenant",
                columns: new[] { "TenantRolesRoleId", "TenantsTenantId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleToPermissions",
                schema: "authp",
                table: "RoleToPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToRoles_RoleId",
                schema: "authp",
                table: "UserToRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleToPermissionsTenant_RoleToPermissions_TenantRolesRoleId",
                schema: "authp",
                table: "RoleToPermissionsTenant",
                column: "TenantRolesRoleId",
                principalSchema: "authp",
                principalTable: "RoleToPermissions",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToRoles_RoleToPermissions_RoleId",
                schema: "authp",
                table: "UserToRoles",
                column: "RoleId",
                principalSchema: "authp",
                principalTable: "RoleToPermissions",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleToPermissionsTenant_RoleToPermissions_TenantRolesRoleId",
                schema: "authp",
                table: "RoleToPermissionsTenant");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToRoles_RoleToPermissions_RoleId",
                schema: "authp",
                table: "UserToRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToRoles",
                schema: "authp",
                table: "UserToRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserToRoles_RoleId",
                schema: "authp",
                table: "UserToRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleToPermissionsTenant",
                schema: "authp",
                table: "RoleToPermissionsTenant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleToPermissions",
                schema: "authp",
                table: "RoleToPermissions");

            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "authp",
                table: "UserToRoles");

            migrationBuilder.DropColumn(
                name: "TenantRolesRoleId",
                schema: "authp",
                table: "RoleToPermissionsTenant");

            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "authp",
                table: "RoleToPermissions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "authp",
                table: "RoleToPermissions");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                schema: "authp",
                table: "UserToRoles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenantRolesRoleName",
                schema: "authp",
                table: "RoleToPermissionsTenant",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToRoles",
                schema: "authp",
                table: "UserToRoles",
                columns: new[] { "UserId", "RoleName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleToPermissionsTenant",
                schema: "authp",
                table: "RoleToPermissionsTenant",
                columns: new[] { "TenantRolesRoleName", "TenantsTenantId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleToPermissions",
                schema: "authp",
                table: "RoleToPermissions",
                column: "RoleName");

            migrationBuilder.CreateIndex(
                name: "IX_UserToRoles_RoleName",
                schema: "authp",
                table: "UserToRoles",
                column: "RoleName");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleToPermissionsTenant_RoleToPermissions_TenantRolesRoleName",
                schema: "authp",
                table: "RoleToPermissionsTenant",
                column: "TenantRolesRoleName",
                principalSchema: "authp",
                principalTable: "RoleToPermissions",
                principalColumn: "RoleName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToRoles_RoleToPermissions_RoleName",
                schema: "authp",
                table: "UserToRoles",
                column: "RoleName",
                principalSchema: "authp",
                principalTable: "RoleToPermissions",
                principalColumn: "RoleName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
