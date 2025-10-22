using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab1_SecureSoftware_NicholasCassar.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateJobListingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobListing",
                columns: table => new
                {
                    JobId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Urgency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobListing", x => x.JobId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobListing");
        }
    }
}