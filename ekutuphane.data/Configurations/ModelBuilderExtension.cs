using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.entity;
using Microsoft.EntityFrameworkCore;

namespace ekutuphane.data.Configurations
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Book>().HasData(
                new Book{BookId=1,BookName="Kuyucaklı Yusuf",BookPage=220,AuthorName="Sabahattin Ali",Description="Kuyucaklı Yusuf konusu itibariyle ailesinin katledilmesiyle sahipsiz kalan dokuz yaşındaki Yusuf’un olayı soruşturmak için Kuyucak’a gelen Nazilli Kaymakamı Selahattin Bey tarafından evlatlık alınması ve çocuğun daha sonraki hayatı anlatılmaktadır. Edebiyat eleştirmenlerine göre Yusuf karakteri, köyden şehre göç edip şehir hayatına uyum sağlayamayan insan tipinin habercisi olarak değerlendirilmektedir.",ImageUrl="kuyucaklıyusuf.jpg"},
                new Book{BookId=2,BookName="Garip",BookPage=72,AuthorName="Orhan Veli Kanık",Description=" 'Bu kitap sizi alışılmış şeylerden şüpheye davet edecektir' kapak şeridiyle çıkan Garip, şiirimizde bir büyük çığır açmıştı. Garipçiler'i yüzüncü yaşlarında sırayla selamladığımız bugünlerde, Orhan Veli'nin öncülüğünde çıkan Garip, bu özel ve tek baskıda yeniden okuruyla buluşuyor.",ImageUrl="orhan-veli-garip.jpg"}
            );
            builder.Entity<Category>().HasData(
                new Category{CategoryId=1,CategoryName="Roman",CategoryUrl="roman"},
                new Category{CategoryId=2,CategoryName="Hikaye",CategoryUrl="hikaye"}
            );
            builder.Entity<BookCategory>().HasData(
                new BookCategory{BookId=1,CategoryId=1},
                new BookCategory{BookId=1,CategoryId=2},
                new BookCategory{BookId=2,CategoryId=1},
                new BookCategory{BookId=2,CategoryId=2}
            );
        }
    }
}