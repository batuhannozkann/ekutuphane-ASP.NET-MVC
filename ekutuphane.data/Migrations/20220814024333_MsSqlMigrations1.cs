using Microsoft.EntityFrameworkCore.Migrations;

namespace ekutuphane.data.Migrations
{
    public partial class MsSqlMigrations1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(maxLength: 70, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AuthorName = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Recommended = table.Column<bool>(nullable: false),
                    PdfUrl = table.Column<string>(nullable: true),
                    BookPage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true),
                    CategoryUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "BooksCategories",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksCategories", x => new { x.BookId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BooksCategories_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorName", "BookName", "BookPage", "Description", "ImageUrl", "PdfUrl", "Recommended" },
                values: new object[,]
                {
                    { 1, "Oğuz Atay", "Tutunamayanlar", 671, "Güzel bir roman", "tutunamayanlar.jpg", null, false },
                    { 2, "Richard Bach", "Martı", 147, "Martı Jonathan Livingston' nın sabır ve azim dolu yaşamı", "marti.jpg", null, false }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "CategoryUrl" },
                values: new object[,]
                {
                    { 1, "Roman", "roman" },
                    { 2, "Hikaye", "hikaye" }
                });

            migrationBuilder.InsertData(
                table: "BooksCategories",
                columns: new[] { "BookId", "CategoryId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 1, 2 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksCategories_CategoryId",
                table: "BooksCategories",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksCategories");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
