using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelExperts.DataAccess.Migrations
{
    public partial class CustomerTableEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add Username and Password columns to the existing Customers table
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Customers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the Username and Password columns from the existing Customers table
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customers");
        }
    }
}
