using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShop.DataLayer.Migrations
{
    public partial class mig_add_banner_type_colleauge_setting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Sliders",
                type: "int",
                maxLength: 150,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ColleaugeSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopImageBannerWeb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopImageBannerWebUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopImageBannerMobileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopImageBannerWebTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopImageBannerMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopImageBannerMobileTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowTopImageBannerWeb = table.Column<bool>(type: "bit", nullable: false),
                    ShowTopImageBannerMobile = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColleaugeSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColleaugeSettings");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Sliders");
        }
    }
}
