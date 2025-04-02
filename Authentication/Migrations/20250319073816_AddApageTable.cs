using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Migrations
{
    /// <inheritdoc />
    public partial class AddApageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Client2",
                table: "Client2");

            migrationBuilder.RenameTable(
                name: "Client2",
                newName: "ApageTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApageTable",
                table: "ApageTable",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ApageTable",
                table: "ApageTable");

            migrationBuilder.RenameTable(
                name: "ApageTable",
                newName: "Client2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client2",
                table: "Client2",
                column: "Id");
        }
    }
}
