using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bakery.Models;

namespace Bakery.Controllers
{
    public class SalesController : Controller
    {
        BakeryEntities db = new BakeryEntities();
        // GET: Sales
        public ActionResult Index()
        {
            if(Session["NewPersonKey"] == null)
            {
                Message msg = new Message();
                msg.MessageText = "You must be logged in to shop";
                return RedirectToAction("Result", msg);
            }
            ViewBag.ProductList = new SelectList(db.Products, "ProductKey", "ProductName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index ([Bind(Include ="ProductKey, SaleDetailQuantity"
            )]SaleDetail r)
        {
            try
            {
                Sale s = new Sale();
                s.SaleDate = DateTime.Now;
                s.CustomerKey = (int)Session["NewPersonKey"];
                s.EmployeeKey = 1;
                db.Sales.Add(s);
                r.Sale = s;
                db.SaleDetails.Add(r);
                db.SaveChanges();
                Message m = new Message();
                m.MessageText = "Thank you for your business";
                return RedirectToAction("Result", m);

            }
            catch (Exception e)
            {
                Message m = new Message();
                m.MessageText = e.Message;
                return RedirectToAction("Result", m);
            }
        }
        public ActionResult Result(Message msg)
        {
            return View(msg);
        }

    }
}