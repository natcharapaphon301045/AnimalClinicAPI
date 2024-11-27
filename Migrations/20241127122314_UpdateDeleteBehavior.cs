using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinicAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_MedicalRecords_MedicalRecordRecord_ID",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_MedicalRecordRecord_ID",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_Pet_ID1",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Pet_ID1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MedicalRecordRecord_ID",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "Pet_ID1",
                table: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "Pet_ID1",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_Record_ID",
                table: "Medicines",
                column: "Record_ID");

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

            migrationBuilder.DropIndex(
                name: "IX_Medicines_Record_ID",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecords_Pet_ID",
                table: "MedicalRecords");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Pet_ID",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "MedicalRecordRecord_ID",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Medicines_MedicalRecordRecord_ID",
                table: "Medicines",
                column: "MedicalRecordRecord_ID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_MedicalRecords_MedicalRecordRecord_ID",
                table: "Medicines",
                column: "MedicalRecordRecord_ID",
                principalTable: "MedicalRecords",
                principalColumn: "Record_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
