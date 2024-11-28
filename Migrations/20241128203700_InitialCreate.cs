using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinicAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetOwners",
                columns: table => new
                {
                    Customer_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone_number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Customer_firstname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Customer_lastname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetOwners", x => x.Customer_ID);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Pet_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_ID = table.Column<int>(type: "int", nullable: false),
                    Pet_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pet_Breed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pet_Gender = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Pet_Age = table.Column<int>(type: "int", nullable: false),
                    Pet_Color = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PetOwnerCustomer_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Pet_ID);
                    table.ForeignKey(
                        name: "FK_Pets_PetOwners_PetOwnerCustomer_ID",
                        column: x => x.PetOwnerCustomer_ID,
                        principalTable: "PetOwners",
                        principalColumn: "Customer_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Appointment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pet_ID = table.Column<int>(type: "int", nullable: false),
                    Customer_ID = table.Column<int>(type: "int", nullable: false),
                    StatusAppointment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Appointment_ID);
                    table.ForeignKey(
                        name: "FK_Appointments_PetOwners_Customer_ID",
                        column: x => x.Customer_ID,
                        principalTable: "PetOwners",
                        principalColumn: "Customer_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Pets_Pet_ID",
                        column: x => x.Pet_ID,
                        principalTable: "Pets",
                        principalColumn: "Pet_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Record_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pet_ID = table.Column<int>(type: "int", nullable: false),
                    TreatmentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TreatmentDetail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Pet_Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Medical_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Medicineget = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Record_ID);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Pets_Pet_ID",
                        column: x => x.Pet_ID,
                        principalTable: "Pets",
                        principalColumn: "Pet_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Customer_ID",
                table: "Appointments",
                column: "Customer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Pet_ID",
                table: "Appointments",
                column: "Pet_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_Pet_ID",
                table: "MedicalRecords",
                column: "Pet_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_PetOwnerCustomer_ID",
                table: "Pets",
                column: "PetOwnerCustomer_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "PetOwners");
        }
    }
}
