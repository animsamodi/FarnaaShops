using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class variant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VariantVoteDetial",
                table: "VariantVoteDetial");

            migrationBuilder.DropColumn(
                name: "VariantVoteDetialId",
                table: "VariantVoteDetial");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "VariantVoteDetial",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "VariantVoteDetial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "VariantVoteDetial",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "VariantVoteDetial",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<long>(
                name: "CmpId",
                table: "Promotion",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VariantVoteDetial",
                table: "VariantVoteDetial",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VariantVoteDetial",
                table: "VariantVoteDetial");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VariantVoteDetial");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "VariantVoteDetial");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "VariantVoteDetial");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "VariantVoteDetial");

            migrationBuilder.AddColumn<int>(
                name: "VariantVoteDetialId",
                table: "VariantVoteDetial",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<long>(
                name: "CmpId",
                table: "Promotion",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VariantVoteDetial",
                table: "VariantVoteDetial",
                column: "VariantVoteDetialId");
        }
    }
}
