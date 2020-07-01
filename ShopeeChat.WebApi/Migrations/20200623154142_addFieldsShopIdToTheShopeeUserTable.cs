using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopeeChat.WebApi.Migrations
{
    public partial class addFieldsShopIdToTheShopeeUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShopId",
                table: "ShopeeUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "ShopeeUsers");
        }
    }
}