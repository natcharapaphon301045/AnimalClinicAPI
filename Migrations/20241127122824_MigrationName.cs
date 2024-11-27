using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinicAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_MedicalRecords_Record_ID",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_Record_ID",
                table: "Medicines");

            migrationBuilder.AddColumn<int>(
                name: "MedicalRecordRecord_ID",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicalRecordRecord_ID",
                table: "Medicines",
                column: "MedicalRecordRecord_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_MedicalRecords_MedicalRecordRecord_ID",
                table: "Medicines",
                column: "MedicalRecordRecord_ID",
                principalTable: "MedicalRecords",
                principalColumn: "Record_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_MedicalRecords_MedicalRecordRecord_ID",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_MedicalRecordRecord_ID",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "MedicalRecordRecord_ID",
                table: "Medicines");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_Record_ID",
                table: "Medicines",
                column: "Record_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_MedicalRecords_Record_ID",
                table: "Medicines",
                column: "Record_ID",
                principalTable: "MedicalRecords",
                principalColumn: "Record_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
