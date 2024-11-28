using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinicAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixMultipleCascadePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PetOwners_Customer_ID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Pets_Pet_ID",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PetOwners_Customer_ID",
                table: "Appointments",
                column: "Customer_ID",
                principalTable: "PetOwners",
                principalColumn: "Customer_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pets_Pet_ID",
                table: "Appointments",
                column: "Pet_ID",
                principalTable: "Pets",
                principalColumn: "Pet_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PetOwners_Customer_ID",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Pets_Pet_ID",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PetOwners_Customer_ID",
                table: "Appointments",
                column: "Customer_ID",
                principalTable: "PetOwners",
                principalColumn: "Customer_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Pets_Pet_ID",
                table: "Appointments",
                column: "Pet_ID",
                principalTable: "Pets",
                principalColumn: "Pet_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
