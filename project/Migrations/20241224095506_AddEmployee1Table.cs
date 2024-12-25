using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployee1Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_NewEmployees_EmployeeId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewEmployees",
                table: "NewEmployees");

            migrationBuilder.RenameTable(
                name: "NewEmployees",
                newName: "Employees1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees1",
                table: "Employees1",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Employees1_EmployeeId",
                table: "Appointments",
                column: "EmployeeId",
                principalTable: "Employees1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Employees1_EmployeeId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees1",
                table: "Employees1");

            migrationBuilder.RenameTable(
                name: "Employees1",
                newName: "NewEmployees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewEmployees",
                table: "NewEmployees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_NewEmployees_EmployeeId",
                table: "Appointments",
                column: "EmployeeId",
                principalTable: "NewEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
