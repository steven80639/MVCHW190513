using HW190512.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW190512.Controllers
{
    public class HomeController : Controller
    {
        private CustomerDBEntities db = new CustomerDBEntities();
        public ActionResult Index()
        {
            var 客戶資訊 = db.客戶數量明細.ToList();
            return View(客戶資訊);
        }

        
    }
}