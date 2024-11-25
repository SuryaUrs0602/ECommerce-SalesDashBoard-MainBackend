using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesDashBoardApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSalesPerformanceAndRevenueEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MostSoldProduct",
                table: "SalesPerformances",
                newName: "MostOrderedProduct");

            migrationBuilder.RenameColumn(
                name: "Profit",
                table: "Revenues",
                newName: "RevenueGrowthRate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "SalesPerformances",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "CountOfOrderedUser",
                table: "SalesPerformances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountOfUnitSold",
                table: "SalesPerformances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountOfusers",
                table: "SalesPerformances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SalesGrowthRate",
                table: "SalesPerformances",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Revenues",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<double>(
                name: "AverageCostPerOrder",
                table: "Revenues",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AverageRevenuePerOrder",
                table: "Revenues",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfOrderedUser",
                table: "SalesPerformances");

            migrationBuilder.DropColumn(
                name: "CountOfUnitSold",
                table: "SalesPerformances");

            migrationBuilder.DropColumn(
                name: "CountOfusers",
                table: "SalesPerformances");

            migrationBuilder.DropColumn(
                name: "SalesGrowthRate",
                table: "SalesPerformances");

            migrationBuilder.DropColumn(
                name: "AverageCostPerOrder",
                table: "Revenues");

            migrationBuilder.DropColumn(
                name: "AverageRevenuePerOrder",
                table: "Revenues");

            migrationBuilder.RenameColumn(
                name: "MostOrderedProduct",
                table: "SalesPerformances",
                newName: "MostSoldProduct");

            migrationBuilder.RenameColumn(
                name: "RevenueGrowthRate",
                table: "Revenues",
                newName: "Profit");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "SalesPerformances",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Revenues",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
