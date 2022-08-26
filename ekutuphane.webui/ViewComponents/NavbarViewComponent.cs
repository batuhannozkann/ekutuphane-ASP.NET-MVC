using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ekutuphane.webui.ViewComponents
{
    public class NavbarViewComponent:ViewComponent
    
    {
        private IUnitOfWork _unitOfWork;
        public NavbarViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.Categories=_unitOfWork.Categories.GetAll();
            return View();
        }
    }
}