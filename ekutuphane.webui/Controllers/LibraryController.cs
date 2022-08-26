using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.business.Abstract;
using ekutuphane.webui.Extensions;
using ekutuphane.webui.Models;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace ekutuphane.webui.Controllers
{
    public class LibraryController:Controller
    { 
        private IUnitOfWork _unitOfWork;
        public LibraryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        public IActionResult Index()
        {
            var books=_unitOfWork.Books.GetAll();
            return View(new ListModel(){
                Books=books,
            });
        }
        public IActionResult Detail(int? id)
        {
            var book=_unitOfWork.Books.GetbookdetailWithById((int)id);

            return View(new BookModel{
                BookName=book.BookName,
                AuthorName=book.AuthorName,
                ImageUrl=book.ImageUrl,
                Description=book.Description,
                BookPage=book.BookPage,
                BookCategories=book.BookCategories.Select(c=>c.Category).ToList(),
                PdfUrl=book.PdfUrl
            });
        }
        public IActionResult BookList(string kategori,int page=1)
        {
            var books=_unitOfWork.Books.GetbookByCategories(kategori);
            ViewBag.RouteCat=RouteData.Values["kategori"];
            return View(new BookViewModel{
                Books=books,
                PagedBooks=books.ToPagedList(page,5),
                CurrentPage=page
            });
        }
        public IActionResult RecommendedBooks(int page=1)
        {
            var books =_unitOfWork.Books.RecommendedBook();
            return View(new BookViewModel{
                Books=books,
                PagedBooks=books.ToPagedList(page,5),
                CurrentPage=page
            });
        }
        public IActionResult SearchBook(string search,int page=1)
        {
            var books=_unitOfWork.Books.SearchBooks(search);
            return View(new BookViewModel{
                Books=books,
                PagedBooks=books.ToPagedList(page,5),
                CurrentPage=page
            });
        }
        public IActionResult ViewPdf(string BookName)
        {
            ViewBag.bookName=BookName;
            return View();
        }
    }
}