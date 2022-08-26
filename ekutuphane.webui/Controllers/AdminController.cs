using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.business.Abstract;
using ekutuphane.entity;
using ekutuphane.webui.Extensions;
using ekutuphane.webui.Identity;
using ekutuphane.webui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList;

namespace ekutuphane.webui.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController:Controller
    {
        private IUnitOfWork _unitOfWork;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public AdminController(IUnitOfWork unitOfWork,RoleManager<IdentityRole> roleManager,UserManager<User> userManager)
        {
            _unitOfWork=unitOfWork;
            _roleManager=roleManager;
            _userManager=userManager;
        }
        [HttpGet]
        public IActionResult AddBook()
        
        {
            ViewBag.Categories=_unitOfWork.Categories.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBookAsync(BookModel model,int[] categoryId,IFormFile file,IFormFile Pdf)
        {
            ViewBag.Categories=_unitOfWork.Categories.GetAll();
            if(!ModelState.IsValid)
            {
                }
                var book=new Book{
                BookName=model.BookName,
                AuthorName=model.AuthorName,
                BookPage=model.BookPage,
                Description=model.Description,
                ImageUrl=model.ImageUrl,
                BookCategories=categoryId.Select(c=>new BookCategory{
                    BookId=model.BookId,
                    CategoryId=c
                }).ToList()
                };
                if(file!=null)
                {
               var extension=Path.GetExtension(file.FileName);
                var randomFileName=string.Format($"{DateTime.Now.Ticks}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images",randomFileName);
                using(var stream = new FileStream(path,FileMode.Create))
                {
                await file.CopyToAsync(stream);
                book.ImageUrl=randomFileName;
                }
                if(Pdf!=null)
                {
                    if(Path.GetExtension(Pdf.FileName)==".pdf")
                    {
                        var FileName=string.Format($"{book.BookName}{DateTime.Now.Ticks}.pdf");
                        var Pdfpath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Pdfs",FileName);
                        using(var stream= new FileStream(Pdfpath,FileMode.Create))
                        {
                            await Pdf.CopyToAsync(stream);
                            book.PdfUrl=FileName;}
                    }
                }
            _unitOfWork.Books.Create(book);
            TempData.Put("message",new AlertMessage{
                Title="Kitap Eklendi",
                Message=$"{book.BookName} Adlı kitap eklendi.",
                AlertType="success"
            });
            return RedirectToAction("BookList");
            }
            return View();
        }
        public IActionResult BookList()
        {
            var books=_unitOfWork.Books.GetAll();
            return View(new BookViewModel{
                Books=books
            });
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var book=_unitOfWork.Books.GetById(id);
            _unitOfWork.Books.Remove(book);
            return RedirectToAction("BookList");
        }
        [HttpGet]
        public IActionResult EditBook(int? id)
        {
            var book=_unitOfWork.Books.GetbookdetailWithById((int)id);
            ViewBag.Categories=_unitOfWork.Categories.GetAll();
            return View(new BookModel{
                BookId=book.BookId,
                BookName=book.BookName,
                AuthorName=book.AuthorName,
                BookPage=book.BookPage,
                Description=book.Description,
                ImageUrl=book.ImageUrl,
                BookCategories=book.BookCategories.Select(c=>c.Category).ToList(),
                PdfUrl=book.PdfUrl,
                Recommended=book.Recommended
            });
        }
        [HttpPost]
        public async Task<IActionResult> EditBook(BookModel book,IFormFile file,int[] categoryId,IFormFile pdf)
        {
            if(ModelState.IsValid)
            {
            var bookdetail=_unitOfWork.Books.GetbookdetailWithById(book.BookId);
            bookdetail.BookName=book.BookName;
            bookdetail.AuthorName=book.AuthorName;
            bookdetail.BookPage=book.BookPage;
            bookdetail.Description=book.Description;
            bookdetail.Recommended=book.Recommended;
            bookdetail.BookCategories=categoryId.Select(c=>new BookCategory{
                BookId=book.BookId,
                CategoryId=c
            }).ToList();
            bookdetail.ImageUrl=book.ImageUrl;
            if(file!=null)
            {
                var extension=Path.GetExtension(file.FileName);
                var RandomName=string.Format($"{DateTime.Now.Ticks}{book.BookName}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images",RandomName);
                using(var stream = new FileStream(path,FileMode.Create))
                {
                await file.CopyToAsync(stream);
                }
                if(book.ImageUrl!=null)
                {
                    var PathForDeleteImage = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images",book.ImageUrl);
                    System.IO.File.Delete(PathForDeleteImage);
                }
                bookdetail.ImageUrl=RandomName;
            }
            if(pdf!=null)
            {
                var PdfName=string.Format($"{DateTime.Now.Ticks}{book.BookName}.pdf");
                var PathToPdf=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Pdfs",PdfName);
                using(var stream = new FileStream(PathToPdf,FileMode.Create))
                {
                    await pdf.CopyToAsync(stream);
                }
                    if(book.PdfUrl!=null)
                        {
                            var PathForDeletePdf=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Pdfs",book.PdfUrl);
                            System.IO.File.Delete(PathForDeletePdf);
                        }
                bookdetail.PdfUrl=PdfName;
                }
                _unitOfWork.Books.Update(bookdetail,categoryId);
                return RedirectToAction("BookList");
            }
            ViewBag.Categories=_unitOfWork.Categories.GetAll();
            return View(book);
        }
        public IActionResult CategoryList()
        {
            var categories = _unitOfWork.Categories.GetAll();
                return View(new ListModel{Categories=categories});
        }
        public IActionResult DeleteCategory(int? id)
        {
            var category = _unitOfWork.Categories.GetById((int)id);
            _unitOfWork.Categories.Remove(category);
                return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
                return View();
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryModel categoryModel)
        {
            var category = new Category(){CategoryName=categoryModel.CategoryName,
            CategoryUrl=categoryModel.CategoryUrl
            };
            _unitOfWork.Categories.Create(category);
                return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public IActionResult EditCategory(string categoryname,int page=1)
        {
            var category=_unitOfWork.Categories.Getcategorybyname(categoryname);
            return View(new CategoryModel{
                CategoryId=category.CategoryId,
                CategoryName=category.CategoryName,
                CategoryUrl=category.CategoryUrl,
                BookCategory=category.BookCategories.ConvertAll(c=>c.Book),
                BookList=category.BookCategories.ConvertAll(c=>c.Book).ToPagedList((int)page,6)
                });
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel categorymodel,int CategoryId,int page=1)
        {
            var category=_unitOfWork.Categories.GetById(CategoryId);
            category.CategoryName=categorymodel.CategoryName;
            category.CategoryUrl=categorymodel.CategoryUrl;
            categorymodel.BookList=categorymodel.BookCategory.ToPagedList(page,6);
            _unitOfWork.Categories.Update(category);
            return View(categorymodel);
        }
        public IActionResult DeleteBookinCategory(int BookId,int CategoryId,string categoryname)
        {
            _unitOfWork.Books.DeleteBookinCategory(BookId,CategoryId);
            return Redirect($"/Category/{categoryname}");
        }
        public IActionResult RoleList()
        {
            var roles=_roleManager.Roles;
            return View(roles);
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRoleAsync(RoleModel model)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole{
                Name=model.Name
            });
            if(result.Succeeded)
            {
             return RedirectToAction("RoleList");
            }
            return View(model);
        }
        public async Task<IActionResult> RoleEditAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<User>();
            var nonmembers=new List<User>();
            foreach(var user in _userManager.Users.ToList<User>())
            {
                    var list = await _userManager.IsInRoleAsync(user,role.Name)?members:nonmembers;
                    list.Add(user);
            }
            return View(new RoleDetails{
                Role=role,
                Members=members,
                NonMembers=nonmembers
            });
        }
        [HttpPost]
        public async Task<IActionResult> RoleEditAsync(RoleEditModel model)
        {
            if(ModelState.IsValid)
            {
                foreach(var userId in model.IdsToAdd?? new string[]{})
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if(user!=null)
                    {
                        var result  = await _userManager.AddToRoleAsync(user,model.RoleName);
                        if(!result.Succeeded)
                        {
                            foreach(var error in result.Errors)
                            {
                                ModelState.AddModelError("",error.Description);
                            }
                        }
                    }
                }
                foreach(var userId in model.IdsToDelete ?? new string[]{})
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if(user!=null)
                     {
                        var result = await _userManager.RemoveFromRoleAsync(user,model.RoleName);
                        if(!result.Succeeded)
                        {
                            foreach(var error in result.Errors)
                            {
                                ModelState.AddModelError("",error.Description);
                            }
                        }
                     }
                }
            }
                return Redirect("/admin/RoleEdit/"+model.RoleId);
        }
        public async Task<IActionResult> DeleteRoleAsync(string name)
        {
            var role = await _roleManager.FindByNameAsync(name);
            await _roleManager.DeleteAsync(role);
            TempData.Put("message" ,new AlertMessage{
                Message=$"{name} adlı rol silindi.",
                Title="Rol Silindi.",
                AlertType="warning"
            });
            return Redirect("/admin/RoleList");
        }
        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }
        public async Task<IActionResult> UserEditAsync(string UserId)
        {
                var roles = _roleManager.Roles.Select(i=>i.Name);
                ViewBag.Roles=roles;
            if(!string.IsNullOrEmpty(UserId))
            {
                var user = await _userManager.FindByIdAsync(UserId);
                if(user!=null)
                {
                    return View(new UserDetailModel{
                        UserId=user.Id,
                        FirstName=user.FirstName,
                        LastName=user.LastName,
                        UserName=user.UserName,
                        Email=user.Email,
                        EmailConfirmed=user.EmailConfirmed,
                        SelectedRoles= await _userManager.GetRolesAsync(user)
                    });
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserEditAsync(UserDetailModel model,string[] selectedRoles)
        {
            ViewBag.Roles=_roleManager.Roles.Select(i=>i.Name);
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if(user!=null)
                {
                    user.FirstName=model.FirstName;
                    user.LastName=model.LastName;
                    user.Email=model.Email;
                    user.UserName=model.UserName;
                    user.EmailConfirmed=model.EmailConfirmed;
                    var result = await _userManager.UpdateAsync(user);
                    if(result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles=selectedRoles?? new string[]{};
                        await _userManager.AddToRolesAsync(user,selectedRoles.Except(userRoles).ToArray());
                        await _userManager.RemoveFromRolesAsync(user,userRoles.Except(selectedRoles).ToArray());
                        return Redirect("/Admin/UserList");
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return Redirect("/Admin/UserList");
        }
    }
}