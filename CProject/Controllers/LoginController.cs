using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CProject.Models;

namespace CProject.Controllers
{
    public class LoginController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Login
        public IQueryable<Reader> GetReader()
        {
            return db.Reader;
        }

        // GET: api/Login/5
        [ResponseType(typeof(Reader))]
        public IHttpActionResult GetReader(int id, string acc, string pwd)
        {
            //Reader reader = db.Reader.Find(id);
            var result = db.Reader.Where(s => s.ReaderAccount == acc && s.ReaderPassword == pwd);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Login/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReader(int id, Reader reader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reader.ReaderID)
            {
                return BadRequest();
            }

            db.Entry(reader).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReaderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Login
        [ResponseType(typeof(Reader))]
        public IHttpActionResult PostReader(Reader reader)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reader.Add(reader);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reader.ReaderID }, reader);
        }

        // DELETE: api/Login/5
        [ResponseType(typeof(Reader))]
        public IHttpActionResult DeleteReader(int id)
        {
            Reader reader = db.Reader.Find(id);
            if (reader == null)
            {
                return NotFound();
            }

            db.Reader.Remove(reader);
            db.SaveChanges();

            return Ok(reader);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReaderExists(int id)
        {
            return db.Reader.Count(e => e.ReaderID == id) > 0;
        }
    }
}