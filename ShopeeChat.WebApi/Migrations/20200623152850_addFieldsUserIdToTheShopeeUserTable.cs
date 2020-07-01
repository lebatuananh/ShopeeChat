using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopeeChat.WebApi.Migrations
{
    public partial class addFieldsUserIdToTheShopeeUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Userid",
                table: "ShopeeUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Userid",
                table: "ShopeeUsers");
        }
    }
}