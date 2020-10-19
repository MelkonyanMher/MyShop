﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Tionit.ShopOnline.Persistence.Migrations
{
    public partial class _1_0_0_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrderItem",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "OrderItem",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
