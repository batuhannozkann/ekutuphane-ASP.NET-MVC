using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ekutuphane.webui.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private IUnitOfWork _unitOfWork;
        public CategoriesViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IViewComponentResult Invoke()
        {
            
            var categories = _unitOfWork.Categories.GetAll();
            if(RouteData.Values["kategori"]!=null)
            {
                ViewBag.SelectedCategory=RouteData?.Values["kategori"];
            }
            return View(categories);
        }
    }
}