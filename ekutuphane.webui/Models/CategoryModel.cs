using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.entity;
using PagedList;

namespace ekutuphane.webui.Models
{
    public class CategoryModel
    {
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryUrl { get; set; }
    public List<Book> BookCategory { get; set; }
    public IPagedList<Book> BookList{get;set;}
    }
}