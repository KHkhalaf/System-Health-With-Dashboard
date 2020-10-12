using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Advantage.API.Migrations
{
    public partial class _InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total",
                table: "orders");

            migrationBuilder.AddColumn<decimal>(
                name: "OrderTotal",
                table: "orders",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTotal",
                table: "orders");

            migrationBuilder.AddColumn<decimal>(
                name: "total",
                table: "orders",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
