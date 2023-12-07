using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Twitter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRetweetReply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Tweets_TweetId",
                table: "Replies");

            migrationBuilder.AlterColumn<int>(
                name: "TweetId",
                table: "Replies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "RetweetId",
                table: "Replies",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_RetweetId",
                table: "Replies",
                column: "RetweetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Retweets_RetweetId",
                table: "Replies",
                column: "RetweetId",
                principalTable: "Retweets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Tweets_TweetId",
                table: "Replies",
                column: "TweetId",
                principalTable: "Tweets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Retweets_RetweetId",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Tweets_TweetId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_RetweetId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "RetweetId",
                table: "Replies");

            migrationBuilder.AlterColumn<int>(
                name: "TweetId",
                table: "Replies",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Tweets_TweetId",
                table: "Replies",
                column: "TweetId",
                principalTable: "Tweets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
