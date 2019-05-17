using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW190512.Models;

namespace HW190512.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        private CustomerDBEntities db = new CustomerDBEntities();
        客戶資料Repository repoCustomer;
        客戶聯絡人Repository repoContact;
        public 客戶聯絡人Controller()
        {
            repoCustomer = RepositoryHelper.Get客戶資料Repository();
            repoContact = RepositoryHelper.Get客戶聯絡人Repository(repoCustomer.UnitOfWork);
        }
        // GET: 客戶聯絡人
        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.職稱 = sortOrder=="職稱_DESC" ? "職稱" : "";
            ViewBag.姓名 = sortOrder == "姓名_DESC" ? "姓名" : "";
            ViewBag.Email = sortOrder == "Email_DESC" ? "Email " : "";
            ViewBag.手機 = sortOrder == "手機_DESC" ? "手機" : "";
            ViewBag.電話 = sortOrder == "電話_DESC" ? "電話" : "";
            ViewBag.客戶名稱 = sortOrder == "客戶名稱_DESC" ? "客戶名稱" : "";

            var 客戶聯絡人 = repoContact.All();
            if (!String.IsNullOrEmpty(searchString))
            {
                客戶聯絡人 = repoContact.Get關鍵字(searchString);
            }
            switch (sortOrder)
            {
                case "職稱":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.職稱);
                    break;
                case "姓名":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.姓名);
                    break;
                case "Email":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.Email);
                    break;
                case "手機":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.手機);
                    break;
                case "電話":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.電話);
                    break;
                case "客戶名稱":
                    客戶聯絡人 = 客戶聯絡人.OrderBy(s => s.客戶資料.客戶名稱);
                    break;
                case "職稱_DESC":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.職稱);
                    break;
                case "姓名_DESC":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.姓名);
                    break;
                case "Email_DESC":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.Email);
                    break;
                case "手機_DESC":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.手機);
                    break;
                case "電話_DESC":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.電話);
                    break;
                case "客戶名稱_DESC":
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.客戶資料.客戶名稱);
                    break;
                default:
                    客戶聯絡人 = 客戶聯絡人.OrderByDescending(s => s.客戶資料.客戶名稱);
                    break;
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repoContact.Add(客戶聯絡人);
                repoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repoContact.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
                repoContact.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repoContact.Find(id);
            repoContact.Delete(客戶聯絡人);
            repoContact.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
