using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameRoleField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplicableToUser",
                table: "AspNetRoles",
                newName: "ApplicableToMember");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplicableToMember",
                table: "AspNetRoles",
                newName: "ApplicableToUser");
        }
    }
}
