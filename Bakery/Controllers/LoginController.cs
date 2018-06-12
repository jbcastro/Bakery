using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;

namespace Bakery.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName, Password, PersonKey")]LoginClass loginClass)
        {
            BakeryEntities db = new BakeryEntities();
            loginClass.PersonKey = 0;

            int result = db.usp_Login(loginClass.UserName, loginClass.Password);
            Message msg = new Message();
            if (result != -1)
            {
                var ukey = (from r in db.People
                            where r.PersonEmail.Equals(loginClass.UserName)
                            select r.PersonKey).FirstOrDefault();
                loginClass.PersonKey = (int)ukey;
                Session["NewPersonKey"] = loginClass.PersonKey;
                msg.MessageText = "Welcome " + loginClass.UserName;
            }
            else
            {
                msg.MessageText = "Invalid Login";
            }
            return View("Result", loginClass);
        }
        public ActionResult Result(LoginClass loginClass)
        {
            return View(loginClass);
        }
    }
}