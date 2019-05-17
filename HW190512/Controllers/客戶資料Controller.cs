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
    public class 客戶資料Controller : Controller
    {
        private CustomerDBEntities db = new CustomerDBEntities();
        客戶資料Repository repoCustomer;
        客戶銀行資訊Repository repoBank;
        客戶聯絡人Repository repoContact;
        public 客戶資料Controller()
        {
            repoCustomer = RepositoryHelper.Get客戶資料Repository();
            repoBank = RepositoryHelper.Get客戶銀行資訊Repository(repoCustomer.UnitOfWork);
            repoContact = RepositoryHelper.Get客戶聯絡人Repository(repoCustomer.UnitOfWork);
        }
        // GET: 客戶資料
        public ActionResult Index(string sortOrder,string searchString)
        {
            ViewBag.客戶名稱 = String.IsNullOrEmpty(sortOrder) ? "客戶名稱" : "";
            ViewBag.統一編號 = String.IsNullOrEmpty(sortOrder) ? "統一編號" : "";
            ViewBag.電話 = String.IsNullOrEmpty(sortOrder) ? "電話" : "";
            ViewBag.傳真 = String.IsNullOrEmpty(sortOrder) ? "傳真" : "";
            ViewBag.地址 = String.IsNullOrEmpty(sortOrder) ? "地址" : "";
            ViewBag.Email = String.IsNullOrEmpty(sortOrder) ? "Email" : "";

            var 客戶資料 = repoCustomer.All();
            if (!String.IsNullOrEmpty(searchString))
            {
                客戶資料 = repoCustomer.Get關鍵字(searchString);
            }
            switch (sortOrder)
            {
                case "客戶名稱":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.客戶名稱);
                    break;
                case "統一編號":
                    客戶資料 = 客戶資料.OrderBy(s => s.統一編號);
                    break;
                case "電話":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.電話);
                    break;
                case "傳真":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.傳真);
                    break;
                case "地址":
                    客戶資料 = 客戶資料.OrderBy(s => s.地址);
                    break;
                case "Email":
                    客戶資料 = 客戶資料.OrderByDescending(s => s.Email);
                    break;
                default:
                    客戶資料 = 客戶資料.OrderBy(s => s.客戶名稱);
                    break;
            }
            return View(客戶資料.ToList());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repoCustomer.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repoCustomer.Add(客戶資料);
                repoCustomer.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repoCustomer.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repoCustomer.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                repoCustomer.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repoCustomer.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repoCustomer.Find(id);

            if (客戶資料 != null)
            {
                repoCustomer.Delete(id);
            }

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
