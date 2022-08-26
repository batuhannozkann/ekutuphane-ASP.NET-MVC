using Microsoft.EntityFrameworkCore.Migrations;

namespace ekutuphane.data.Migrations
{
    public partial class upmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "AuthorName", "BookName", "BookPage", "Description", "ImageUrl" },
                values: new object[] { "Sabahattin Ali", "Kuyucaklı Yusuf", 220, "Kuyucaklı Yusuf konusu itibariyle ailesinin katledilmesiyle sahipsiz kalan dokuz yaşındaki Yusuf’un olayı soruşturmak için Kuyucak’a gelen Nazilli Kaymakamı Selahattin Bey tarafından evlatlık alınması ve çocuğun daha sonraki hayatı anlatılmaktadır. Edebiyat eleştirmenlerine göre Yusuf karakteri, köyden şehre göç edip şehir hayatına uyum sağlayamayan insan tipinin habercisi olarak değerlendirilmektedir.", "kuyucaklıyusuf.jpg" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "AuthorName", "BookName", "BookPage", "Description", "ImageUrl" },
                values: new object[] { "Orhan Veli Kanık", "Garip", 72, " 'Bu kitap sizi alışılmış şeylerden şüpheye davet edecektir' kapak şeridiyle çıkan Garip, şiirimizde bir büyük çığır açmıştı. Garipçiler'i yüzüncü yaşlarında sırayla selamladığımız bugünlerde, Orhan Veli'nin öncülüğünde çıkan Garip, bu özel ve tek baskıda yeniden okuruyla buluşuyor.", "orhan-veli-garip.jpg" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "AuthorName", "BookName", "BookPage", "Description", "ImageUrl" },
                values: new object[] { "Oğuz Atay", "Tutunamayanlar", 671, "Güzel bir roman", "tutunamayanlar.jpg" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                columns: new[] { "AuthorName", "BookName", "BookPage", "Description", "ImageUrl" },
                values: new object[] { "Richard Bach", "Martı", 147, "Martı Jonathan Livingston' nın sabır ve azim dolu yaşamı", "marti.jpg" });
        }
    }
}
