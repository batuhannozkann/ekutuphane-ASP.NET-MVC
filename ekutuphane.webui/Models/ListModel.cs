using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.entity;

namespace ekutuphane.webui.Models
{
    public class ListModel
    {
        public List<Book> Books { get; set; }
        public List<Category> Categories { get; set; }
    }
}