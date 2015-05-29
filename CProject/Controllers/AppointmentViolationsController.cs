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
    public class AppointmentViolationsController : Controller
    {
        private Entities db = new Entities();

        // GET: AppointmentViolations
        public ActionResult Index()
        {
            var appointmentViolation = db.AppointmentViolation.Include(a => a.ReservationRecord);
            return View(appointmentViolation.ToList());
        }

        // GET: AppointmentViolations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentViolation appointmentViolation = db.AppointmentViolation.Find(id);
            if (appointmentViolation == null)
            {
                return HttpNotFound();
            }
            return View(appointmentViolation);
        }

        // GET: AppointmentViolations/Create
        public ActionResult Create()
        {
            ViewBag.ReservationRecordID = new SelectList(db.ReservationRecord, "ReservationRecordID", "ReservationRecordID");
            return View();
        }

        // POST: AppointmentViolations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentViolationID,NoTakeNumber,SuspendedDate,ReservationRecordID")] AppointmentViolation appointmentViolation)
        {
            if (ModelState.IsValid)
            {
                db.AppointmentViolation.Add(appointmentViolation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReservationRecordID = new SelectList(db.ReservationRecord, "ReservationRecordID", "ReservationRecordID", appointmentViolation.ReservationRecordID);
            return View(appointmentViolation);
        }

        // GET: AppointmentViolations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentViolation appointmentViolation = db.AppointmentViolation.Find(id);
            if (appointmentViolation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservationRecordID = new SelectList(db.ReservationRecord, "ReservationRecordID", "ReservationRecordID", appointmentViolation.ReservationRecordID);
            return View(appointmentViolation);
        }

        // POST: AppointmentViolations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentViolationID,NoTakeNumber,SuspendedDate,ReservationRecordID")] AppointmentViolation appointmentViolation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointmentViolation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservationRecordID = new SelectList(db.ReservationRecord, "ReservationRecordID", "ReservationRecordID", appointmentViolation.ReservationRecordID);
            return View(appointmentViolation);
        }

        // GET: AppointmentViolations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppointmentViolation appointmentViolation = db.AppointmentViolation.Find(id);
            if (appointmentViolation == null)
            {
                return HttpNotFound();
            }
            return View(appointmentViolation);
        }

        // POST: AppointmentViolations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppointmentViolation appointmentViolation = db.AppointmentViolation.Find(id);
            db.AppointmentViolation.Remove(appointmentViolation);
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
