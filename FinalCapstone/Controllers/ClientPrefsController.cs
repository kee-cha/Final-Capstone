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
    public class ClientPrefsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClientPrefs
        public ActionResult Index()
        {
            var clientPrefs = db.ClientPrefs.Include(c => c.Client);
            return View(clientPrefs.ToList());
        }

        // GET: ClientPrefs/Details/5
        public ActionResult Details()
        {
            var id = User.Identity.GetUserId();
            var client = db.Clients.Include(c => c.ApplicationUser).Where(c => c.ApplicationId == id).SingleOrDefault();
            var clientPref = db.ClientPrefs.Where(p => p.ClientId == client.Id).SingleOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
 
            if (clientPref == null)
            {
                return HttpNotFound();
            }
            return View(clientPref);
        }

        // GET: ClientPrefs/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName");
            return View();
        }

        // POST: ClientPrefs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientPref clientPref)
        {
            if (ModelState.IsValid)
            {
                db.ClientPrefs.Add(clientPref);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", clientPref.ClientId);
            return View("Details", clientPref);
        }

        // GET: ClientPrefs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientPref clientPref = db.ClientPrefs.Find(id);
            if (clientPref == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", clientPref.ClientId);
            return View(clientPref);
        }

        // POST: ClientPrefs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientPref clientPref)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientPref).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", clientPref.ClientId);
            return View(clientPref);
        }

        // GET: ClientPrefs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientPref clientPref = db.ClientPrefs.Find(id);
            if (clientPref == null)
            {
                return HttpNotFound();
            }
            return View(clientPref);
        }

        // POST: ClientPrefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientPref clientPref = db.ClientPrefs.Find(id);
            db.ClientPrefs.Remove(clientPref);
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
