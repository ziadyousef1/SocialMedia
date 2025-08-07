using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoSupportToPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoFileName",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoFileName",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Posts");
        }
    }
}
