using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinicAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Pets_Pet_ID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Pets_Pet_ID",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_MedicalRecords_Record_ID",
                table: "Medicines");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pets_Pet_ID",
                table: "Appointments",
                column: "Pet_ID",
                principalTable: "Pets",
                principalColumn: "Pet_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Pets_Pet_ID",
                table: "MedicalRecords",
                column: "Pet_ID",
                principalTable: "Pets",
                principalColumn: "Pet_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_MedicalRecords_Record_ID",
                table: "Medicines",
                column: "Record_ID",
                principalTable: "MedicalRecords",
                principalColumn: "Record_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Pets_Pet_ID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Pets_Pet_ID",
                table: "MedicalRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_MedicalRecords_Record_ID",
                table: "Medicines");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pets_Pet_ID",
                table: "Appointments",
                column: "Pet_ID",
                principalTable: "Pets",
                principalColumn: "Pet_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Pets_Pet_ID",
                table: "MedicalRecords",
                column: "Pet_ID",
                principalTable: "Pets",
                principalColumn: "Pet_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_MedicalRecords_Record_ID",
                table: "Medicines",
                column: "Record_ID",
                principalTable: "MedicalRecords",
                principalColumn: "Record_ID");
        }
    }
}
