using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Twitter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableEntityToRetweetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Retweets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Retweets",
                type: "boolean",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Retweets",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Retweets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Retweets");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Retweets");
        }
    }
}
