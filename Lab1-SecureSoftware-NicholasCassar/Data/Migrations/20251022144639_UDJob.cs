using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1_SecureSoftware_NicholasCassar.Data.Migrations
{
    /// <inheritdoc />
    public partial class UDJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobAddress",
                table: "JobListing",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobAddress",
                table: "JobListing");
        }
    }
}
