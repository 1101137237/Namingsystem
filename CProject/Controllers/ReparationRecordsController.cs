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
    public class ReparationRecordsController : Controller
    {
        private Entities db = new Entities();

        // GET: ReparationRecords
        public ActionResult Index()
        {
            var reparationRecord = db.ReparationRecord.Include(r => r.LendingRecord);
            return View(reparationRecord.ToList());
        }

        // GET: ReparationRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReparationRecord reparationRecord = db.ReparationRecord.Find(id);
            if (reparationRecord == null)
            {
                return HttpNotFound();
            }
            return View(reparationRecord);
        }

        // GET: ReparationRecords/Create
        public ActionResult Create()
        {
            ViewBag.LendingRecordID = new SelectList(db.LendingRecord, "LendingRecordID", "LendingRecordID");
            return View();
        }

        // POST: ReparationRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReparationRecordID,ReparationReason,ReparationMoney,PayTime,LendingRecordID")] ReparationRecord reparationRecord)
        {
            if (ModelState.IsValid)
            {
                db.ReparationRecord.Add(reparationRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LendingRecordID = new SelectList(db.LendingRecord, "LendingRecordID", "LendingRecordID", reparationRecord.LendingRecordID);
            return View(reparationRecord);
        }

        // GET: ReparationRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReparationRecord reparationRecord = db.ReparationRecord.Find(id);
            if (reparationRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.LendingRecordID = new SelectList(db.LendingRecord, "LendingRecordID", "LendingRecordID", reparationRecord.LendingRecordID);
            return View(reparationRecord);
        }

        // POST: ReparationRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReparationRecordID,ReparationReason,ReparationMoney,PayTime,LendingRecordID")] ReparationRecord reparationRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reparationRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LendingRecordID = new SelectList(db.LendingRecord, "LendingRecordID", "LendingRecordID", reparationRecord.LendingRecordID);
            return View(reparationRecord);
        }

        // GET: ReparationRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReparationRecord reparationRecord = db.ReparationRecord.Find(id);
            if (reparationRecord == null)
            {
                return HttpNotFound();
            }
            return View(reparationRecord);
        }

        // POST: ReparationRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReparationRecord reparationRecord = db.ReparationRecord.Find(id);
            db.ReparationRecord.Remove(reparationRecord);
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
