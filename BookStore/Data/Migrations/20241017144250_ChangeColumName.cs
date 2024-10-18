using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Publishers",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Categories",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Books",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Authors",
                newName: "ImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Publishers",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Categories",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Books",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Authors",
                newName: "Image");
        }
    }
}
