using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalClinicAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentsToPetOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PetOwners_PetOwnerCustomer_ID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PetOwnerCustomer_ID",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PetOwnerCustomer_ID",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Customer_ID",
                table: "Appointments",
                column: "Customer_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PetOwners_Customer_ID",
                table: "Appointments",
                column: "Customer_ID",
                principalTable: "PetOwners",
                principalColumn: "Customer_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PetOwners_Customer_ID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Customer_ID",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "PetOwnerCustomer_ID",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PetOwnerCustomer_ID",
                table: "Appointments",
                column: "PetOwnerCustomer_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PetOwners_PetOwnerCustomer_ID",
                table: "Appointments",
                column: "PetOwnerCustomer_ID",
                principalTable: "PetOwners",
                principalColumn: "Customer_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
