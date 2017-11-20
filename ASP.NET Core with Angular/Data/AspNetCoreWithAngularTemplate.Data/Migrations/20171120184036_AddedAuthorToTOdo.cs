using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AspNetCoreWithAngularTemplate.Data.Migrations
{
    public partial class AddedAuthorToTOdo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "TodoItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_AuthorId",
                table: "TodoItems",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_AspNetUsers_AuthorId",
                table: "TodoItems",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_AspNetUsers_AuthorId",
                table: "TodoItems");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_AuthorId",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "TodoItems");
        }
    }
}
