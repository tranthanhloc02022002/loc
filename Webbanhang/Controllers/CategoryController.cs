using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbanhang.Context;

namespace Webbanhang.Controllers
{
    public class CategoryController : Controller
    {
        locEntities objlocEntities = new locEntities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objlocEntities.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var lstProduct = objlocEntities.Products.Where(n =>n.CategoryId == Id).ToList();
            return View(lstProduct);
        }
    }
}