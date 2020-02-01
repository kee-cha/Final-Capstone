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
    public class InjuriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Injuries
        public IQueryable<Injury> GetInjuries()
        {
            return db.Injuries;
        }

        // GET: api/Injuries/5
        [ResponseType(typeof(Injury))]
        public IHttpActionResult GetInjury(int id)
        {
            Injury injury = db.Injuries.Find(id);
            if (injury == null)
            {
                return NotFound();
            }

            return Ok(injury);
        }

        // PUT: api/Injuries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutInjury(int id, Injury injury)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != injury.Id)
            {
                return BadRequest();
            }

            db.Entry(injury).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InjuryExists(id))
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

        // POST: api/Injuries
        [ResponseType(typeof(Injury))]
        public IHttpActionResult PostInjury(Injury injury)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Injuries.Add(injury);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = injury.Id }, injury);
        }

        // DELETE: api/Injuries/5
        [ResponseType(typeof(Injury))]
        public IHttpActionResult DeleteInjury(int id)
        {
            Injury injury = db.Injuries.Find(id);
            if (injury == null)
            {
                return NotFound();
            }

            db.Injuries.Remove(injury);
            db.SaveChanges();

            return Ok(injury);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InjuryExists(int id)
        {
            return db.Injuries.Count(e => e.Id == id) > 0;
        }
    }
}