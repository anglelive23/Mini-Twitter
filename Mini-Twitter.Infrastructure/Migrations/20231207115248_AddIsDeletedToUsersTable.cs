using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Twitter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "security",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "security",
                table: "Users");
        }
    }
}
