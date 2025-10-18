using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthPermissions.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSupportTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "authp",
                table: "SupportTickets",
                newName: "TicketStatus");

            migrationBuilder.RenameColumn(
                name: "State",
                schema: "authp",
                table: "SupportTickets",
                newName: "StatusCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TicketStatus",
                schema: "authp",
                table: "SupportTickets",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StatusCode",
                schema: "authp",
                table: "SupportTickets",
                newName: "State");
        }
    }
}
