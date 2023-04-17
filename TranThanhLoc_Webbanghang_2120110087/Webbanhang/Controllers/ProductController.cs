using System.Linq;
using System.Web.Mvc;
using Webbanhang.Context;

namespace Webbanhang.Controllers
{
    public class ProductController : Controller
    {
        locEntities objlocEntities = new locEntities();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objlocEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}