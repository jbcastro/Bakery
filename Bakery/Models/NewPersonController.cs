using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;

namespace Bakery.Controllers
{
    public class NewPersonController : Controller
    {
        // GET: NewPerson
        public ActionResult Index()
        {
            return View();
        }
    }
}