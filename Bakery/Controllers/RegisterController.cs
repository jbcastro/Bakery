using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;

namespace Bakery.Controllers
{
    public class RegisterController : Controller
    {
        BakeryEntities db = new BakeryEntities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "LastName, FirstName, Email, Phone, PlainPassword")]NewPerson r)
        {
            
            Message m = new Message();
            int results = db.usp_newPerson(r.LastName, r.FirstName, r.Email,r.Phone, r.PlainPassword);
            if (results != -1)

            {

                m.MessageText = "Welcome, " + r.FirstName;

            }
            else
            {
                m.MessageText = "Something went horribly, horribly wrong";
            }


            return View("Result", m);
        }

        public ActionResult Result(Message m)
        {
            return View(m);
        }
    }
}