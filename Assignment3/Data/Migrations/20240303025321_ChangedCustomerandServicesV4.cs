using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCustomerandServicesV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerService_Customers_CustomerId",
                table: "CustomerService");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerService_Services_ServiceId",
                table: "CustomerService");

            migrationBuilder.DropColumn(
                name: "testField",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "testField",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "CustomerService",
                newName: "ServicesServiceId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "CustomerService",
                newName: "CustomersCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerService_ServiceId",
                table: "CustomerService",
                newName: "IX_CustomerService_ServicesServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerService_Customers_CustomersCustomerId",
                table: "CustomerService",
                column: "CustomersCustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerService_Services_ServicesServiceId",
                table: "CustomerService",
                column: "ServicesServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerService_Customers_CustomersCustomerId",
                table: "CustomerService");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerService_Services_ServicesServiceId",
                table: "CustomerService");

            migrationBuilder.RenameColumn(
                name: "ServicesServiceId",
                table: "CustomerService",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "CustomersCustomerId",
                table: "CustomerService",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerService_ServicesServiceId",
                table: "CustomerService",
                newName: "IX_CustomerService_ServiceId");

            migrationBuilder.AddColumn<string>(
                name: "testField",
                table: "Employees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "testField",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerService_Customers_CustomerId",
                table: "CustomerService",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerService_Services_ServiceId",
                table: "CustomerService",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
