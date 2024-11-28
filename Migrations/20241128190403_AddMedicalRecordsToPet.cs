using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinicAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMedicalRecordsToPet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Pets_Pet_ID1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_Pets_Pet_ID1",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_Pet_ID1",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Pet_ID1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Pet_ID1",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Pet_ID1",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Pet_ID",
                table: "MedicalRecords",
                column: "Pet_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Pet_ID",
                table: "Appointments",
                column: "Pet_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pets_Pet_ID",
                table: "Appointments",
                column: "Pet_ID",
                principalTable: "Pets",
                principalColumn: "Pet_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Pets_Pet_ID",
                table: "MedicalRecords",
                column: "Pet_ID",
                principalTable: "Pets",
                principalColumn: "Pet_ID",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_Pet_ID",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Pet_ID",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Pet_ID1",
                table: "MedicalRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pet_ID1",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Pet_ID1",
                table: "MedicalRecords",
                column: "Pet_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Pet_ID1",
                table: "Appointments",
                column: "Pet_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pets_Pet_ID1",
                table: "Appointments",
                column: "Pet_ID1",
                principalTable: "Pets",
                principalColumn: "Pet_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_Pets_Pet_ID1",
                table: "MedicalRecords",
                column: "Pet_ID1",
                principalTable: "Pets",
                principalColumn: "Pet_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
