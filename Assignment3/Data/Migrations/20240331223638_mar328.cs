using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mar328 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeService");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ServiceId",
                table: "Employees",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Services_ServiceId",
                table: "Employees",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Services_ServiceId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ServiceId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "EmployeeService",
                columns: table => new
                {
                    EmployeesEmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ServicesServiceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeService", x => new { x.EmployeesEmployeeId, x.ServicesServiceId });
                    table.ForeignKey(
                        name: "FK_EmployeeService_Employees_EmployeesEmployeeId",
                        column: x => x.EmployeesEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeService_Services_ServicesServiceId",
                        column: x => x.ServicesServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeService_ServicesServiceId",
                table: "EmployeeService",
                column: "ServicesServiceId");
        }
    }
}
