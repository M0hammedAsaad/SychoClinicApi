using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SychoClinicApi.Migrations
{
    public partial class AddServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Symptoms",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Treatment",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Symptoms",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Treatment",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "AspNetUsers");
        }
    }
}
