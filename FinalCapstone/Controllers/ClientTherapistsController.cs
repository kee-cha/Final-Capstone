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
    public class ClientTherapistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClientTherapists
        public ActionResult Index()
        {
            var clientTherapists = db.ClientTherapists.Include(c => c.Client).Include(c => c.MassageTherapist);
            return View(clientTherapists.ToList());
        }

        // GET: ClientTherapists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientTherapist clientTherapist = db.ClientTherapists.Find(id);
            if (clientTherapist == null)
            {
                return HttpNotFound();
            }
            return View(clientTherapist);
        }

        // GET: ClientTherapists/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName");
            ViewBag.TherapistId = new SelectList(db.MassageTherapists, "Id", "FirstName");
            return View();
        }

        // POST: ClientTherapists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientId,TherapistId")] ClientTherapist clientTherapist)
        {
            if (ModelState.IsValid)
            {
                db.ClientTherapists.Add(clientTherapist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", clientTherapist.ClientId);
            ViewBag.TherapistId = new SelectList(db.MassageTherapists, "Id", "FirstName", clientTherapist.TherapistId);
            return View(clientTherapist);
        }

        // GET: ClientTherapists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientTherapist clientTherapist = db.ClientTherapists.Find(id);
            if (clientTherapist == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", clientTherapist.ClientId);
            ViewBag.TherapistId = new SelectList(db.MassageTherapists, "Id", "FirstName", clientTherapist.TherapistId);
            return View(clientTherapist);
        }

        // POST: ClientTherapists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientId,TherapistId")] ClientTherapist clientTherapist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientTherapist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", clientTherapist.ClientId);
            ViewBag.TherapistId = new SelectList(db.MassageTherapists, "Id", "FirstName", clientTherapist.TherapistId);
            return View(clientTherapist);
        }

        // GET: ClientTherapists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientTherapist clientTherapist = db.ClientTherapists.Find(id);
            if (clientTherapist == null)
            {
                return HttpNotFound();
            }
            return View(clientTherapist);
        }

        // POST: ClientTherapists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientTherapist clientTherapist = db.ClientTherapists.Find(id);
            db.ClientTherapists.Remove(clientTherapist);
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
