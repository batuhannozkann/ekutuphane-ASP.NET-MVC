using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.entity;
using PagedList;

namespace ekutuphane.webui.Models
{
    public class BookViewModel
    {
        public List<Book> Books { get; set; }
        public IPagedList<Book> PagedBooks {get;set;}
        public int CurrentPage {get;set;}
    }
}