using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.entity;

namespace ekutuphane.webui.Models
{
    public class BookModel
    {
        public int BookId { get; set; }
        [Required(ErrorMessage="Kitap Adı boş bırakılamaz/Book name is required")]
        [StringLength(30)]
        public string BookName { get; set; }
        [Required(ErrorMessage="Açıklama Alanı boş bırakılamaz/Description section is required")]
        public string Description { get; set; }
        [Required(ErrorMessage="Yazar Adı boş bırakılamaz/Author name is required")]
        public string AuthorName { get; set; }
        public string ImageUrl { get; set; }
        
        [Required(ErrorMessage="Sayfa sayısı boş bırakılamaz/Page count is required")]
        public int BookPage { get; set; }
        public bool Recommended { get; set; }
        public string PdfUrl{get;set;}
        public List<Category> BookCategories { get; set; }
    }
}