using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace QualitySouvenir.Data.Migrations
{
    public partial class AddMaxLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Souvenir_SouvenirID1",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Souvenir_SouvenirID",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_SouvenirID1",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "SouvenirID1",
                table: "CartItem");

            migrationBuilder.AlterColumn<int>(
                name: "SouvenirID",
                table: "OrderItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "OrderID",
                table: "OrderItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Order",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Order",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Order",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Order",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Order",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SouvenirID",
                table: "CartItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_SouvenirID",
                table: "CartItem",
                column: "SouvenirID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Souvenir_SouvenirID",
                table: "CartItem",
                column: "SouvenirID",
                principalTable: "Souvenir",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Souvenir_SouvenirID",
                table: "OrderItem",
                column: "SouvenirID",
                principalTable: "Souvenir",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Souvenir_SouvenirID",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Souvenir_SouvenirID",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_SouvenirID",
                table: "CartItem");

            migrationBuilder.AlterColumn<int>(
                name: "SouvenirID",
                table: "OrderItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderID",
                table: "OrderItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Order",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Order",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Order",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Order",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Order",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "SouvenirID",
                table: "CartItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SouvenirID1",
                table: "CartItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_SouvenirID1",
                table: "CartItem",
                column: "SouvenirID1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Souvenir_SouvenirID1",
                table: "CartItem",
                column: "SouvenirID1",
                principalTable: "Souvenir",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Souvenir_SouvenirID",
                table: "OrderItem",
                column: "SouvenirID",
                principalTable: "Souvenir",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
