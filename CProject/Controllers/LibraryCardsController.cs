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
    public class LibraryCardsController : Controller
    {
        private Entities db = new Entities();

        // GET: LibraryCards
        public ActionResult Index()
        {
            var libraryCard = db.LibraryCard.Include(l => l.Reader);
            return View(libraryCard.ToList());
        }

        // GET: LibraryCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryCard libraryCard = db.LibraryCard.Find(id);
            if (libraryCard == null)
            {
                return HttpNotFound();
            }
            return View(libraryCard);
        }

        // GET: LibraryCards/Create
        public ActionResult Create()
        {
            ViewBag.ReaderID = new SelectList(db.Reader, "ReaderID", "ReaderName");
            return View();
        }

        // POST: LibraryCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LibraryCardID,DateLicensing,CancellationDate,ReaderID")] LibraryCard libraryCard)
        {
            if (ModelState.IsValid)
            {
                db.LibraryCard.Add(libraryCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReaderID = new SelectList(db.Reader, "ReaderID", "ReaderName", libraryCard.ReaderID);
            return View(libraryCard);
        }

        // GET: LibraryCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryCard libraryCard = db.LibraryCard.Find(id);
            if (libraryCard == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReaderID = new SelectList(db.Reader, "ReaderID", "ReaderName", libraryCard.ReaderID);
            return View(libraryCard);
        }

        // POST: LibraryCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LibraryCardID,DateLicensing,CancellationDate,ReaderID")] LibraryCard libraryCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libraryCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReaderID = new SelectList(db.Reader, "ReaderID", "ReaderName", libraryCard.ReaderID);
            return View(libraryCard);
        }

        // GET: LibraryCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryCard libraryCard = db.LibraryCard.Find(id);
            if (libraryCard == null)
            {
                return HttpNotFound();
            }
            return View(libraryCard);
        }

        // POST: LibraryCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LibraryCard libraryCard = db.LibraryCard.Find(id);
            db.LibraryCard.Remove(libraryCard);
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
