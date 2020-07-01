using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopeeChat.WebApi.Migrations
{
    public partial class addFieldsToTheShopeeUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "ShopeeUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Locale",
                table: "ShopeeUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpcEc",
                table: "ShopeeUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "ShopeeUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "ShopeeUsers");

            migrationBuilder.DropColumn(
                name: "Locale",
                table: "ShopeeUsers");

            migrationBuilder.DropColumn(
                name: "SpcEc",
                table: "ShopeeUsers");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "ShopeeUsers");
        }
    }
}