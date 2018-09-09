using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpyStore.DAL.Migrations
{
    public partial class RemainingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                schema: "Store",
                table: "ShoppingCartRecords",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "Store",
                table: "ShoppingCartRecords",
                type: "datetime",
                nullable: true,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitCost",
                schema: "Store",
                table: "Products",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                schema: "Store",
                table: "Products",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShipDate",
                schema: "Store",
                table: "Orders",
                type: "datetime",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                schema: "Store",
                table: "Orders",
                type: "datetime",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitCost",
                schema: "Store",
                table: "OrderDetails",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "LineItemTotal",
                schema: "Store",
                table: "OrderDetails",
                type: "money",
                nullable: true,
                computedColumnSql: "[Quantity]*[UnitCost]",
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart",
                schema: "Store",
                table: "ShoppingCartRecords",
                columns: new[] { "Id", "ProductId", "CustomerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers",
                schema: "Store",
                table: "Customers",
                column: "EmailAddress",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart",
                schema: "Store",
                table: "ShoppingCartRecords");

            migrationBuilder.DropIndex(
                name: "IX_Customers",
                schema: "Store",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                schema: "Store",
                table: "ShoppingCartRecords",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "Store",
                table: "ShoppingCartRecords",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true,
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitCost",
                schema: "Store",
                table: "Products",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                schema: "Store",
                table: "Products",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShipDate",
                schema: "Store",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                schema: "Store",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitCost",
                schema: "Store",
                table: "OrderDetails",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<decimal>(
                name: "LineItemTotal",
                schema: "Store",
                table: "OrderDetails",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true,
                oldComputedColumnSql: "[Quantity]*[UnitCost]");
        }
    }
}
