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
    public class ReservationRecordsController : Controller
    {
        private Entities db = new Entities();

        // GET: ReservationRecords
        public ActionResult Index()
        {
            var reservationRecord = db.ReservationRecord.Include(r => r.LendingCode).Include(r => r.Reader);
            return View(reservationRecord.ToList());
        }

        // GET: ReservationRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationRecord reservationRecord = db.ReservationRecord.Find(id);
            if (reservationRecord == null)
            {
                return HttpNotFound();
            }
            return View(reservationRecord);
        }

        // GET: ReservationRecords/Create
        public ActionResult Create()
        {
            ViewBag.LendingCodeID = new SelectList(db.LendingCode, "LendingCodeID", "Status");
            ViewBag.ReaderID = new SelectList(db.Reader, "ReaderID", "ReaderName");
            return View();
        }

        // POST: ReservationRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationRecordID,ReservationTime,TakeTime,LendingCodeID,ReaderID")] ReservationRecord reservationRecord)
        {
            if (ModelState.IsValid)
            {
                db.ReservationRecord.Add(reservationRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LendingCodeID = new SelectList(db.LendingCode, "LendingCodeID", "Status", reservationRecord.LendingCodeID);
            ViewBag.ReaderID = new SelectList(db.Reader, "ReaderID", "ReaderName", reservationRecord.ReaderID);
            return View(reservationRecord);
        }

        // GET: ReservationRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationRecord reservationRecord = db.ReservationRecord.Find(id);
            if (reservationRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.LendingCodeID = new SelectList(db.LendingCode, "LendingCodeID", "Status", reservationRecord.LendingCodeID);
            ViewBag.ReaderID = new SelectList(db.Reader, "ReaderID", "ReaderName", reservationRecord.ReaderID);
            return View(reservationRecord);
        }

        // POST: ReservationRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationRecordID,ReservationTime,TakeTime,LendingCodeID,ReaderID")] ReservationRecord reservationRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservationRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LendingCodeID = new SelectList(db.LendingCode, "LendingCodeID", "Status", reservationRecord.LendingCodeID);
            ViewBag.ReaderID = new SelectList(db.Reader, "ReaderID", "ReaderName", reservationRecord.ReaderID);
            return View(reservationRecord);
        }

        // GET: ReservationRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationRecord reservationRecord = db.ReservationRecord.Find(id);
            if (reservationRecord == null)
            {
                return HttpNotFound();
            }
            return View(reservationRecord);
        }

        // POST: ReservationRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReservationRecord reservationRecord = db.ReservationRecord.Find(id);
            db.ReservationRecord.Remove(reservationRecord);
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
