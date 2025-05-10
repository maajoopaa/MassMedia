using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MassMedia.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MassMediaImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "MassMedias",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "MassMedias");
        }
    }
}
