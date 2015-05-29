using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CProject.Models;

namespace CProject.Controllers
{
    public class LendingCodesController : Controller
    {
        private Entities db = new Entities();

        // GET: LendingCodes
        public ActionResult Index()
        {
            var lendingCode = db.LendingCode.Include(l => l.LibraryBook).Include(l => l.Library);
            return View(lendingCode.ToList());
        }

        // GET: LendingCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LendingCode lendingCode = db.LendingCode.Find(id);
            if (lendingCode == null)
            {
                return HttpNotFound();
            }
            return View(lendingCode);
        }

        // GET: LendingCodes/Create
        public ActionResult Create()
        {
            ViewBag.LibraryBookID = new SelectList(db.LibraryBook, "LibraryBookID", "BookName");
            ViewBag.LibraryID = new SelectList(db.Library, "LibraryID", "LibraryName");
            return View();
        }

        // POST: LendingCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LendingCodeID,LibraryID,LibraryBookID,Status")] LendingCode lendingCode)
        {
            if (ModelState.IsValid)
            {
                db.LendingCode.Add(lendingCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LibraryBookID = new SelectList(db.LibraryBook, "LibraryBookID", "BookName", lendingCode.LibraryBookID);
            ViewBag.LibraryID = new SelectList(db.Library, "LibraryID", "LibraryName", lendingCode.LibraryID);
            return View(lendingCode);
        }

        // GET: LendingCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LendingCode lendingCode = db.LendingCode.Find(id);
            if (lendingCode == null)
            {
                return HttpNotFound();
            }
            ViewBag.LibraryBookID = new SelectList(db.LibraryBook, "LibraryBookID", "BookName", lendingCode.LibraryBookID);
            ViewBag.LibraryID = new SelectList(db.Library, "LibraryID", "LibraryName", lendingCode.LibraryID);
            return View(lendingCode);
        }

        // POST: LendingCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LendingCodeID,LibraryID,LibraryBookID,Status")] LendingCode lendingCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lendingCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LibraryBookID = new SelectList(db.LibraryBook, "LibraryBookID", "BookName", lendingCode.LibraryBookID);
            ViewBag.LibraryID = new SelectList(db.Library, "LibraryID", "LibraryName", lendingCode.LibraryID);
            return View(lendingCode);
        }

        // GET: LendingCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LendingCode lendingCode = db.LendingCode.Find(id);
            if (lendingCode == null)
            {
                return HttpNotFound();
            }
            return View(lendingCode);
        }

        // POST: LendingCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LendingCode lendingCode = db.LendingCode.Find(id);
            db.LendingCode.Remove(lendingCode);
            db.SaveChanges();
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
