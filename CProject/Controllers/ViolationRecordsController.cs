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
    public class ViolationRecordsController : Controller
    {
        private Entities db = new Entities();

        // GET: ViolationRecords
        public ActionResult Index()
        {
            var violationRecord = db.ViolationRecord.Include(v => v.LendingRecord);
            return View(violationRecord.ToList());
        }

        // GET: ViolationRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViolationRecord violationRecord = db.ViolationRecord.Find(id);
            if (violationRecord == null)
            {
                return HttpNotFound();
            }
            return View(violationRecord);
        }

        // GET: ViolationRecords/Create
        public ActionResult Create()
        {
            ViewBag.LendingRecordID = new SelectList(db.LendingRecord, "LendingRecordID", "LendingRecordID");
            return View();
        }

        // POST: ViolationRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ViolationID,ViolationDay,Violationdate,LendingRecordID")] ViolationRecord violationRecord)
        {
            if (ModelState.IsValid)
            {
                db.ViolationRecord.Add(violationRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LendingRecordID = new SelectList(db.LendingRecord, "LendingRecordID", "LendingRecordID", violationRecord.LendingRecordID);
            return View(violationRecord);
        }

        // GET: ViolationRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViolationRecord violationRecord = db.ViolationRecord.Find(id);
            if (violationRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.LendingRecordID = new SelectList(db.LendingRecord, "LendingRecordID", "LendingRecordID", violationRecord.LendingRecordID);
            return View(violationRecord);
        }

        // POST: ViolationRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ViolationID,ViolationDay,Violationdate,LendingRecordID")] ViolationRecord violationRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(violationRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LendingRecordID = new SelectList(db.LendingRecord, "LendingRecordID", "LendingRecordID", violationRecord.LendingRecordID);
            return View(violationRecord);
        }

        // GET: ViolationRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViolationRecord violationRecord = db.ViolationRecord.Find(id);
            if (violationRecord == null)
            {
                return HttpNotFound();
            }
            return View(violationRecord);
        }

        // POST: ViolationRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViolationRecord violationRecord = db.ViolationRecord.Find(id);
            db.ViolationRecord.Remove(violationRecord);
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
