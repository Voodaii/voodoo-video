using Microsoft.EntityFrameworkCore.Migrations;

namespace Voodoo.Video.Migrations
{
    public partial class AddIpAndResolutionToCamera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Cameras",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResHeight",
                table: "Cameras",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResWidth",
                table: "Cameras",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "ResHeight",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "ResWidth",
                table: "Cameras");
        }
    }
}
