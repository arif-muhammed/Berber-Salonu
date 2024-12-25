using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project.Migrations
{
    /// <inheritdoc />
    public partial class RenameEmployeesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // حذف مفتاح الربط الخارجي القديم
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Employees_EmployeeId",
                table: "Appointments");

            // حذف المفتاح الأساسي القديم
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            // إعادة تسمية الجدول
            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "NewEmployees");

            // إضافة المفتاح الأساسي الجديد
            migrationBuilder.AddPrimaryKey(
                name: "PK_NewEmployees",
                table: "NewEmployees",
                column: "Id");

            // تحديث مفتاح الربط الخارجي إلى الجدول الجديد
            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_NewEmployees_EmployeeId",
                table: "Appointments",
                column: "EmployeeId",
                principalTable: "NewEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // حذف مفتاح الربط الخارجي الجديد
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_NewEmployees_EmployeeId",
                table: "Appointments");

            // حذف المفتاح الأساسي الجديد
            migrationBuilder.DropPrimaryKey(
                name: "PK_NewEmployees",
                table: "NewEmployees");

            // إعادة تسمية الجدول إلى الاسم القديم
            migrationBuilder.RenameTable(
                name: "NewEmployees",
                newName: "Employees");

            // إضافة المفتاح الأساسي القديم
            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            // إعادة مفتاح الربط الخارجي إلى الجدول القديم
            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Employees_EmployeeId",
                table: "Appointments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
