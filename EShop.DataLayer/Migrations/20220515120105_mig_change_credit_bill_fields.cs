using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_change_credit_bill_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CreditId",
                table: "CreditBills",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "CreditBills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CreditBills_UserId",
                table: "CreditBills",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreditBills_Users_UserId",
                table: "CreditBills",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreditBills_Users_UserId",
                table: "CreditBills");

            migrationBuilder.DropIndex(
                name: "IX_CreditBills_UserId",
                table: "CreditBills");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CreditBills");

            migrationBuilder.AlterColumn<long>(
                name: "CreditId",
                table: "CreditBills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
