﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tionit.ShopOnline.Persistence.Migrations
{
    public partial class _1_0_0_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthTokenId",
                table: "Customer",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthTokenId",
                table: "Customer");
        }
    }
}
