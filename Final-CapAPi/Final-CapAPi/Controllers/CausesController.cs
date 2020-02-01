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
using Final_CapAPi.Models;

namespace Final_CapAPi.Controllers
{
    public class CausesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Causes
        public IQueryable<Cause> GetCauses()
        {
            return db.Causes;
        }

        // GET: api/Causes/5
        [ResponseType(typeof(Cause))]
        public IHttpActionResult GetCause(int id)
        {
            Cause cause = db.Causes.Find(id);
            if (cause == null)
            {
                return NotFound();
            }

            return Ok(cause);
        }

        // PUT: api/Causes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCause(int id, Cause cause)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cause.Id)
            {
                return BadRequest();
            }

            db.Entry(cause).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CauseExists(id))
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

        // POST: api/Causes
        [ResponseType(typeof(Cause))]
        public IHttpActionResult PostCause(Cause cause)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Causes.Add(cause);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cause.Id }, cause);
        }

        // DELETE: api/Causes/5
        [ResponseType(typeof(Cause))]
        public IHttpActionResult DeleteCause(int id)
        {
            Cause cause = db.Causes.Find(id);
            if (cause == null)
            {
                return NotFound();
            }

            db.Causes.Remove(cause);
            db.SaveChanges();

            return Ok(cause);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CauseExists(int id)
        {
            return db.Causes.Count(e => e.Id == id) > 0;
        }
    }
}