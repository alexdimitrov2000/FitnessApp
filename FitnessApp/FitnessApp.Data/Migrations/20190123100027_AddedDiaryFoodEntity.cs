using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessApp.Data.Migrations
{
    public partial class AddedDiaryFoodEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_FoodDiaries_FoodDiaryId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_FoodDiaryId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "FoodDiaryId",
                table: "Foods");

            migrationBuilder.CreateTable(
                name: "DiaryFoods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FoodId = table.Column<int>(nullable: false),
                    Multiplier = table.Column<decimal>(nullable: false),
                    FoodDiaryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaryFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaryFoods_FoodDiaries_FoodDiaryId",
                        column: x => x.FoodDiaryId,
                        principalTable: "FoodDiaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiaryFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiaryFoods_FoodDiaryId",
                table: "DiaryFoods",
                column: "FoodDiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_DiaryFoods_FoodId",
                table: "DiaryFoods",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiaryFoods");

            migrationBuilder.AddColumn<int>(
                name: "FoodDiaryId",
                table: "Foods",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodDiaryId",
                table: "Foods",
                column: "FoodDiaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_FoodDiaries_FoodDiaryId",
                table: "Foods",
                column: "FoodDiaryId",
                principalTable: "FoodDiaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
