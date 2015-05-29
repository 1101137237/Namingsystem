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
    public class LendingRecordsController : Controller
    {
        private Entities db = new Entities();

        // GET: LendingRecords
        public ActionResult Index()
        {
            var lendingRecord = db.LendingRecord.Include(l => l.LendingCode).Include(l => l.LibraryCard);
            return View(lendingRecord.ToList());
        }

        // GET: LendingRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LendingRecord lendingRecord = db.LendingRecord.Find(id);
            if (lendingRecord == null)
            {
                return HttpNotFound();
            }
            return View(lendingRecord);
        }

        // GET: LendingRecords/Create
        public ActionResult Create()
        {
            ViewBag.LendingCodeID = new SelectList(db.LendingCode, "LendingCodeID", "Status");
            ViewBag.LibraryCardID = new SelectList(db.LibraryCard, "LibraryCardID", "LibraryCardID");
            return View();
        }

        // POST: LendingRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LendingRecordID,LendingTime,ReturnTime,ExpectedReturnTime,NumberOfTime,LibraryCardID,LendingCodeID")] LendingRecord lendingRecord)
        {
            if (ModelState.IsValid)
            {
                db.LendingRecord.Add(lendingRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LendingCodeID = new SelectList(db.LendingCode, "LendingCodeID", "Status", lendingRecord.LendingCodeID);
            ViewBag.LibraryCardID = new SelectList(db.LibraryCard, "LibraryCardID", "LibraryCardID", lendingRecord.LibraryCardID);
            return View(lendingRecord);
        }

        // GET: LendingRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LendingRecord lendingRecord = db.LendingRecord.Find(id);
            if (lendingRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.LendingCodeID = new SelectList(db.LendingCode, "LendingCodeID", "Status", lendingRecord.LendingCodeID);
            ViewBag.LibraryCardID = new SelectList(db.LibraryCard, "LibraryCardID", "LibraryCardID", lendingRecord.LibraryCardID);
            return View(lendingRecord);
        }

        // POST: LendingRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LendingRecordID,LendingTime,ReturnTime,ExpectedReturnTime,NumberOfTime,LibraryCardID,LendingCodeID")] LendingRecord lendingRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lendingRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LendingCodeID = new SelectList(db.LendingCode, "LendingCodeID", "Status", lendingRecord.LendingCodeID);
            ViewBag.LibraryCardID = new SelectList(db.LibraryCard, "LibraryCardID", "LibraryCardID", lendingRecord.LibraryCardID);
            return View(lendingRecord);
        }

        // GET: LendingRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LendingRecord lendingRecord = db.LendingRecord.Find(id);
            if (lendingRecord == null)
            {
                return HttpNotFound();
            }
            return View(lendingRecord);
        }

        // POST: LendingRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LendingRecord lendingRecord = db.LendingRecord.Find(id);
            db.LendingRecord.Remove(lendingRecord);
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
