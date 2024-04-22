using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinic_management_system.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalReportId",
                table: "Admissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_MedicalReportId",
                table: "Admissions",
                column: "MedicalReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_MedicalReports_MedicalReportId",
                table: "Admissions",
                column: "MedicalReportId",
                principalTable: "MedicalReports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_MedicalReports_MedicalReportId",
                table: "Admissions");

            migrationBuilder.DropIndex(
                name: "IX_Admissions_MedicalReportId",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "MedicalReportId",
                table: "Admissions");
        }
    }
}
