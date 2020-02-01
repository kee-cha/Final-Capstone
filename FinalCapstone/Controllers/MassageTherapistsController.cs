using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalCapstone.Models;
using Microsoft.AspNet.Identity;

namespace FinalCapstone.Controllers
{
    public class MassageTherapistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MassageTherapists
        public ActionResult Index(int? id)
        {
           
            //var massageTherapists = db.MassageTherapists.Include(m => m.ApplicationUser);
            var client = db.Clients.Where(c => c.Id == id).SingleOrDefault();
            var therapist = Filter(client.Id);
            return View(therapist);
        }

        public List<MassageTherapist> Filter(int id)
        {
            var pref = db.ClientPrefs.Where(p => p.ClientId == id).SingleOrDefault();
            var therapist = db.MassageTherapists.Where(m => m.Gender == pref.TherapistGender && m.Specialty == pref.TherapistSpecialty).ToList();
            return therapist;
        }

        public List<MassageTherapist> FilterByLocation(List<MassageTherapist> therapist, int id)
        {
            var client = db.Clients.Where(c => c.Id == id).SingleOrDefault();
            therapist = db.MassageTherapists.Where(m => m.Zip == client.Zip).ToList();
            return therapist;            
        }
        // GET: MassageTherapists/Details/5
        public ActionResult Details()
        {
            var id = User.Identity.GetUserId();
            var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.ApplicationId == id).SingleOrDefault();            
                       
            if (therapist.ApplicationId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (therapist == null)
            {
                return HttpNotFound();
            }
            return View(therapist);
        }

        // GET: MassageTherapists/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: MassageTherapists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MassageTherapist massageTherapist)
        {
            if (ModelState.IsValid)
            {
                massageTherapist.ApplicationId = User.Identity.GetUserId();
                db.MassageTherapists.Add(massageTherapist);
                db.SaveChanges();
                return RedirectToAction("LogOut","Account");
            }

            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", massageTherapist.ApplicationId);
            return View(massageTherapist);
        }

        // GET: MassageTherapists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MassageTherapist massageTherapist = db.MassageTherapists.Find(id);
            if (massageTherapist == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", massageTherapist.ApplicationId);
            return View(massageTherapist);
        }

        // POST: MassageTherapists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( MassageTherapist massageTherapist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(massageTherapist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", massageTherapist.ApplicationId);
            return View(massageTherapist);
        }

        public ActionResult SetSchedule()
        {
            var userId = User.Identity.GetUserId();
            var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.ApplicationId == userId).SingleOrDefault();
            return View("Schedule", therapist);
        }
        [HttpPost]
        public ActionResult SetSchedule(int? id, MassageTherapist update)
        {
            var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.Id == id).SingleOrDefault();

            therapist.Schedule1 = update.Schedule1;
            therapist.Schedule2 = update.Schedule2;
            therapist.Schedule3 = update.Schedule3;
            therapist.Schedule4 = update.Schedule4;
            db.SaveChanges();
            return View("Details", therapist);

        }

        // GET: MassageTherapists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MassageTherapist massageTherapist = db.MassageTherapists.Find(id);
            if (massageTherapist == null)
            {
                return HttpNotFound();
            }
            return View(massageTherapist);
        }

        // POST: MassageTherapists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MassageTherapist massageTherapist = db.MassageTherapists.Find(id);
            db.MassageTherapists.Remove(massageTherapist);
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
