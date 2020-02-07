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
        private ApplicationDbContext db;
        private string userId;
        public ClientPrefsController()
        {
            db = new ApplicationDbContext();
            userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        // GET: ClientPrefs
        public ActionResult Index()
        {
            var clientPrefs = db.ClientPrefs.Include(c => c.Client);
            return View(clientPrefs.ToList());
        }

        // GET: ClientPrefs/Details/5
        public ActionResult Details(int? id)
        {

            var thisClient = db.Clients.Include(c => c.ApplicationUser).Where(c => c.ApplicationId == userId).SingleOrDefault();
            
            if (thisClient == null)
            {
                thisClient = db.Clients.Include(c => c.ApplicationUser).Where(c => c.Id == id).Single();
            } 
            var clientPref = db.ClientPrefs.Where(p => p.ClientId == thisClient.Id).SingleOrDefault();
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
            var client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
            if (ModelState.IsValid)
            {
                clientPref.ClientId = client.Id;
                db.ClientPrefs.Add(clientPref);
                db.SaveChanges();
                return RedirectToAction("LogOut", "Account");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", clientPref.ClientId);
            return View("Details", clientPref);
        }

        // GET: ClientPrefs/Edit/5
        public ActionResult Edit(int? id)
        {
            ClientPrefViewModel viewModel = new ClientPrefViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            viewModel.ClientPref = db.ClientPrefs.Find(id);
            viewModel.Client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
            if (viewModel.ClientPref == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName",viewModel.ClientPref.ClientId);
            return View(viewModel);
        }

        // POST: ClientPrefs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientPrefViewModel viewModel)
        {
            viewModel.Client.ApplicationId = userId;
            Client client = viewModel.Client;

            ClientPref clientPref = viewModel.ClientPref;

            if (ModelState.IsValid)
            {                
                db.Entry(clientPref).State = EntityState.Modified;
                db.SaveChanges();
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", "Clients", viewModel);
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", viewModel.ClientPref.ClientId);
            return RedirectToAction("Details", "Clients",viewModel);
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
