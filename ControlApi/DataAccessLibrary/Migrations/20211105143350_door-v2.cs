using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLibrary.Migrations
{
    public partial class doorv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doors_Tenants_TenantId",
                table: "Doors");

            migrationBuilder.DropIndex(
                name: "IX_Doors_TenantId",
                table: "Doors");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Doors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Doors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Doors_TenantId",
                table: "Doors",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doors_Tenants_TenantId",
                table: "Doors",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
