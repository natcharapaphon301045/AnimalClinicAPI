using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinicAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateAppointmentTable : Migration
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

            migrationBuilder.DropTable(
                name: "Medicines");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    MedicalRecordRecord_ID = table.Column<int>(type: "int", nullable: false),
                    Medicine_amount = table.Column<int>(type: "int", nullable: false),
                    Medicine_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Record_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Medicines_MedicalRecords_MedicalRecordRecord_ID",
                        column: x => x.MedicalRecordRecord_ID,
                        principalTable: "MedicalRecords",
                        principalColumn: "Record_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Pet_ID",
                table: "MedicalRecords",
                column: "Pet_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Pet_ID",
                table: "Appointments",
                column: "Pet_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_MedicalRecordRecord_ID",
                table: "Medicines",
                column: "MedicalRecordRecord_ID");

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
        }
    }
}
