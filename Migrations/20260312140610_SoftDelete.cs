using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEstudo.Migrations
{
    /// <inheritdoc />
    public partial class SoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Tarefas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Excluida",
                table: "Tarefas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "Excluida",
                table: "Tarefas");
        }
    }
}
