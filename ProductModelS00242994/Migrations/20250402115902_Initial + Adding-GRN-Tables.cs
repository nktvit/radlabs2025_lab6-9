using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductModel.Migrations
{
    /// <inheritdoc />
    public partial class InitialAddingGRNTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GRNs",
                columns: table => new
                {
                    GrnID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    StockUpdated = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRNs", x => x.GrnID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ReorderLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    ReorderQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<double>(type: "REAL", nullable: false),
                    StockOnHand = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GRNLines",
                columns: table => new
                {
                    LineID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GrnID = table.Column<int>(type: "INTEGER", nullable: false),
                    StockID = table.Column<int>(type: "INTEGER", nullable: false),
                    QtyDelivered = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRNLines", x => x.LineID);
                    table.ForeignKey(
                        name: "FK_GRNLines_GRNs_GrnID",
                        column: x => x.GrnID,
                        principalTable: "GRNs",
                        principalColumn: "GrnID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GRNLines_Products_StockID",
                        column: x => x.StockID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "ReorderLevel", "ReorderQuantity", "StockOnHand", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Chai", 10, 14, 9, 10.0 },
                    { 2, "Syrup", 25, 8, 10, 5.0 },
                    { 3, "Cajun Seasoning", 10, 17, 7, 7.0 },
                    { 4, "Olive Oil", 10, 16, 8, 8.0 },
                    { 5, "Boysenberry Spread", 25, 19, 5, 6.0 },
                    { 6, "Dried Pears", 10, 23, 7, 6.0 },
                    { 7, "Curry Sauce", 10, 30, 6, 9.0 },
                    { 8, "Walnuts", 10, 17, 1, 2.0 },
                    { 9, "Fruit Cocktail", 10, 29, 8, 7.0 },
                    { 10, "Chocolate Biscuits Mix", 5, 7, 1, 2.0 },
                    { 11, "Marmalade", 10, 61, 2, 10.0 },
                    { 12, "Scones", 5, 8, 5, 6.0 },
                    { 13, "Beer", 15, 11, 4, 7.0 },
                    { 14, "Crab Meat", 30, 14, 3, 2.0 },
                    { 15, "Clam Chowder", 10, 7, 6, 10.0 },
                    { 16, "Coffee", 25, 35, 1, 3.0 },
                    { 17, "Chocolate", 25, 10, 1, 10.0 },
                    { 18, "Dried Apples", 10, 40, 3, 1.0 },
                    { 19, "Long Grain Rice", 25, 5, 3, 7.0 },
                    { 20, "Gnocchi", 30, 29, 3, 8.0 },
                    { 21, "Ravioli", 20, 15, 7, 5.0 },
                    { 22, "Hot Pepper Sauce", 10, 16, 7, 6.0 },
                    { 23, "Tomato Sauce", 20, 13, 5, 9.0 },
                    { 24, "Mozzarella", 10, 26, 4, 9.0 },
                    { 25, "Almonds", 5, 8, 5, 9.0 },
                    { 26, "Mustard", 15, 10, 3, 4.0 },
                    { 27, "Dried Plums", 50, 3, 3, 7.0 },
                    { 28, "Green Tea", 100, 2, 2, 3.0 },
                    { 29, "Granola", 20, 2, 2, 2.0 },
                    { 30, "Potato Chips", 30, 1, 3, 5.0 },
                    { 31, "Brownie Mix", 10, 9, 2, 8.0 },
                    { 32, "Cake Mix", 10, 11, 8, 7.0 },
                    { 33, "Tea", 20, 2, 3, 4.0 },
                    { 34, "Pears", 10, 1, 10, 1.0 },
                    { 35, "Peaches", 10, 1, 7, 7.0 },
                    { 36, "Pineapple", 10, 1, 1, 6.0 },
                    { 37, "Cherry Pie Filling", 10, 1, 10, 2.0 },
                    { 38, "Green Beans", 10, 1, 2, 9.0 },
                    { 39, "Corn", 10, 1, 3, 6.0 },
                    { 40, "Peas", 10, 1, 3, 6.0 },
                    { 41, "Tuna Fish", 30, 1, 8, 2.0 },
                    { 42, "Smoked Salmon", 30, 2, 9, 10.0 },
                    { 43, "Hot Cereal", 50, 3, 8, 6.0 },
                    { 44, "Vegetable Soup", 100, 1, 4, 9.0 },
                    { 45, "Chicken Soup", 100, 1, 2, 8.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GRNLines_GrnID",
                table: "GRNLines",
                column: "GrnID");

            migrationBuilder.CreateIndex(
                name: "IX_GRNLines_StockID",
                table: "GRNLines",
                column: "StockID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GRNLines");

            migrationBuilder.DropTable(
                name: "GRNs");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
