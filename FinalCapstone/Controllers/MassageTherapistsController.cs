using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalCapstone.Models;

namespace FinalCapstone.Controllers
{
    public class MassageTherapistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MassageTherapists
        public ActionResult Index()
        {
            var massageTherapists = db.MassageTherapists.Include(m => m.ApplicationUser);
            return View(massageTherapists.ToList());
        }

        // GET: MassageTherapists/Details/5
        public ActionResult Details(int? id)
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

        // GET: MassageTherapists/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: MassageTherapists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Street,City,State,Zip,Latitude,Longitude,Gender,Specialty,Rating,ApplicationId")] MassageTherapist massageTherapist)
        {
            if (ModelState.IsValid)
            {
                db.MassageTherapists.Add(massageTherapist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationId = new SelectList(db.ApplicationUsers, "Id", "Email", massageTherapist.ApplicationId);
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
            ViewBag.ApplicationId = new SelectList(db.ApplicationUsers, "Id", "Email", massageTherapist.ApplicationId);
            return View(massageTherapist);
        }

        // POST: MassageTherapists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Street,City,State,Zip,Latitude,Longitude,Gender,Specialty,Rating,ApplicationId")] MassageTherapist massageTherapist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(massageTherapist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.ApplicationUsers, "Id", "Email", massageTherapist.ApplicationId);
            return View(massageTherapist);
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
