using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekutuphane.entity
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string ImageUrl { get; set; }
        public bool Recommended {get;set;}
        public string PdfUrl {get; set;}
        public int BookPage { get; set; }
        public List<BookCategory> BookCategories { get; set; }
    }
}