using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinic_management_system.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MedicalReports_AdmissionId",
                table: "MedicalReports",
                column: "AdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_DoctorId",
                table: "Admissions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_PatientId",
                table: "Admissions",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Doctors_DoctorId",
                table: "Admissions",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Patients_PatientId",
                table: "Admissions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalReports_Admissions_AdmissionId",
                table: "MedicalReports",
                column: "AdmissionId",
                principalTable: "Admissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Doctors_DoctorId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Patients_PatientId",
                table: "Admissions");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalReports_Admissions_AdmissionId",
                table: "MedicalReports");

            migrationBuilder.DropIndex(
                name: "IX_MedicalReports_AdmissionId",
                table: "MedicalReports");

            migrationBuilder.DropIndex(
                name: "IX_Admissions_DoctorId",
                table: "Admissions");

            migrationBuilder.DropIndex(
                name: "IX_Admissions_PatientId",
                table: "Admissions");
        }
    }
}
