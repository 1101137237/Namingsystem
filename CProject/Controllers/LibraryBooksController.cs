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
    public class LibraryBooksController : Controller
    {
        private Entities db = new Entities();

        // GET: LibraryBooks
        public ActionResult Index()
        {
            var libraryBook = db.LibraryBook.Include(l => l.BookType).Include(l => l.Classification);
            return View(libraryBook.ToList());
        }

        // GET: LibraryBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryBook libraryBook = db.LibraryBook.Find(id);
            if (libraryBook == null)
            {
                return HttpNotFound();
            }
            return View(libraryBook);
        }

        // GET: LibraryBooks/Create
        public ActionResult Create()
        {
            ViewBag.TypeID = new SelectList(db.BookType, "TypeID", "TypeName");
            ViewBag.ClassificationID = new SelectList(db.Classification, "ClassificationID", "ClassificationName");
            return View();
        }

        // POST: LibraryBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LibraryBookID,BookName,BookAuthor,BookPress,ISBN,Page,ClassificationID,TypeID")] LibraryBook libraryBook)
        {
            if (ModelState.IsValid)
            {
                db.LibraryBook.Add(libraryBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeID = new SelectList(db.BookType, "TypeID", "TypeName", libraryBook.TypeID);
            ViewBag.ClassificationID = new SelectList(db.Classification, "ClassificationID", "ClassificationName", libraryBook.ClassificationID);
            return View(libraryBook);
        }

        // GET: LibraryBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryBook libraryBook = db.LibraryBook.Find(id);
            if (libraryBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeID = new SelectList(db.BookType, "TypeID", "TypeName", libraryBook.TypeID);
            ViewBag.ClassificationID = new SelectList(db.Classification, "ClassificationID", "ClassificationName", libraryBook.ClassificationID);
            return View(libraryBook);
        }

        // POST: LibraryBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LibraryBookID,BookName,BookAuthor,BookPress,ISBN,Page,ClassificationID,TypeID")] LibraryBook libraryBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libraryBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeID = new SelectList(db.BookType, "TypeID", "TypeName", libraryBook.TypeID);
            ViewBag.ClassificationID = new SelectList(db.Classification, "ClassificationID", "ClassificationName", libraryBook.ClassificationID);
            return View(libraryBook);
        }

        // GET: LibraryBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LibraryBook libraryBook = db.LibraryBook.Find(id);
            if (libraryBook == null)
            {
                return HttpNotFound();
            }
            return View(libraryBook);
        }

        // POST: LibraryBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LibraryBook libraryBook = db.LibraryBook.Find(id);
            db.LibraryBook.Remove(libraryBook);
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
