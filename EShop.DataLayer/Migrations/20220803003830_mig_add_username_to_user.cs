using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_username_to_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "CooperationRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CooperationRequests_UserId",
                table: "CooperationRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CooperationRequests_Users_UserId",
                table: "CooperationRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CooperationRequests_Users_UserId",
                table: "CooperationRequests");

            migrationBuilder.DropIndex(
                name: "IX_CooperationRequests_UserId",
                table: "CooperationRequests");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CooperationRequests");
        }
    }
}
