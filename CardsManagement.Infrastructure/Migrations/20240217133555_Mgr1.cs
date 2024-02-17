using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardsManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mgr1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleNavigationId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserNavigationId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_RoleNavigationId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserNavigationId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "RoleNavigationId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UserNavigationId",
                table: "AspNetUserRoles");

            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                defaultValue: "Member",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OTP",
                table: "AspNetUsers",
                type: "nvarchar(7)",
                maxLength: 7,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedBy",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldDefaultValue: "Member");

            migrationBuilder.AlterColumn<string>(
                name: "OTP",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(7)",
                oldMaxLength: 7,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedBy",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleNavigationId",
                table: "AspNetUserRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserNavigationId",
                table: "AspNetUserRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleNavigationId",
                table: "AspNetUserRoles",
                column: "RoleNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserNavigationId",
                table: "AspNetUserRoles",
                column: "UserNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleNavigationId",
                table: "AspNetUserRoles",
                column: "RoleNavigationId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserNavigationId",
                table: "AspNetUserRoles",
                column: "UserNavigationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
