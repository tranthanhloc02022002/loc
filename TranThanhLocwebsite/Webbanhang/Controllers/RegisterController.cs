using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webbanhang.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult User()
        {
            return View();
        }
    }
}