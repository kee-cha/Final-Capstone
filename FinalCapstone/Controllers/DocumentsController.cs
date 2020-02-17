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
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db;
        private string userId;
        public DocumentsController()
        {
            db = new ApplicationDbContext();
            userId = System.Web.HttpContext.Current.User.Identity.GetUserId(); 
        }
        // GET: Documents
        public ActionResult Index(int? id, bool? complete )
        {
            ViewBag.ClientId = id;          
            ViewBag.IsComplete = complete;
            var documents = db.Documents.Include(d => d.Client).Where(d => d.ClientId == id);
            return View(documents.ToList());
        }
        [HttpPost]
        public ActionResult Index(int? id, int? clientId, Document document)
        {
            if (ModelState.IsValid)
            {
                var therapist = db.Clients.Include(c => c.ApplicationUser).Where(c => c.ApplicationId == userId).SingleOrDefault();
                var client = db.Clients.Include(t => t.ApplicationUser).Where(t => t.Id == id).SingleOrDefault();
                if (therapist == null)
                {
                    client = db.Clients.Include(t => t.ApplicationUser).Where(t => t.Id == clientId).SingleOrDefault();
                    var soap = db.Documents.Include(c => c.Client.ApplicationUser).Where(s => s.Id == id).SingleOrDefault();
                    soap.Subjective = document.Subjective;
                    soap.Objective = document.Objective;
                    soap.Assessment = document.Assessment;
                    soap.Plan = document.Plan;
                    db.SaveChanges();
                }
                else
                {
                    document.ClientId = client.Id;                    
                    db.Documents.Add(document);
                    db.SaveChanges();
                }

                return RedirectToAction("Details", "Clients", new { id = document.ClientId });
            }
            return View(document);
        }
        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create(int? id)
        {            
            ViewBag.ClientId = id;
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? clientId, Document document)
        {
           
            document.ClientId = Convert.ToInt32(clientId);
            if (ModelState.IsValid)
            {
                document.Id = 0;
                db.Documents.Add(document);
                db.SaveChanges();
                return RedirectToAction("Details", "Clients", new { id = document.ClientId, complete = false });
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", document.ClientId);
            return RedirectToAction("Details","Clients", document);
        }

        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", document.ClientId);
            return View("Details", document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Document document)
        {
            if (ModelState.IsValid)
            {
                var soap = db.Documents.Include(d=>d.Client.ApplicationUser).Where(d => d.Id == id).SingleOrDefault();
                soap.Subjective = document.Subjective;
                soap.Objective = document.Objective;
                soap.Assessment = document.Assessment;
                soap.Plan = document.Plan;
                db.SaveChanges();
                return RedirectToAction("Details", "Clients", new { id = document.ClientId, complete = false });
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", document.ClientId);
            return View("Details",document);
        }


        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Details", "Clients", new { id = document.ClientId, complete = false });
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
