
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
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db;
        private string userId;
        public ReviewsController()
        {
            db  = new ApplicationDbContext();
            userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }
           

        // GET: Reviews
        public ActionResult Index(int? id, bool? isTrue)
        {
            ViewBag.IsTrue = isTrue;
            ViewBag.MtId = id;
            var reviews = db.Reviews.Include(r => r.Client).Include(r => r.MassageTherapist).Include(r=>r.Client.ApplicationUser).Where(r=>r.TherapistId == id).Select(r=>r);

            return View(reviews.ToList());
        }
        [HttpPost]
        public ActionResult Index(int? id, int? therapistId, Review review)
        {
            if (ModelState.IsValid)
            {
                var client = db.Clients.Include(c => c.ApplicationUser).Where(c => c.ApplicationId == userId).SingleOrDefault();
                var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.Id == id).SingleOrDefault();
                if (therapist == null)
                {
                    therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.Id == therapistId).SingleOrDefault();

                    var comment = db.Reviews.Include(c => c.Client.ApplicationUser).Where(c => c.Id == id && therapist.Id == therapistId).SingleOrDefault();
                    comment.Comment = review.Comment;
                    db.SaveChanges();                    
                }
                else
                {
                    review.TherapistId = therapist.Id;
                    review.ClientId = client.Id;
                    db.Reviews.Add(review);
                    db.SaveChanges();
                    
                }

                return RedirectToAction("Index", new { id = review.TherapistId });
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", review.ClientId);
            ViewBag.TherapistId = new SelectList(db.MassageTherapists, "Id", "FirstName", review.TherapistId);
            return View(review);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName");
            ViewBag.TherapistId = new SelectList(db.MassageTherapists, "Id", "FirstName");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id, bool isTrue)
        {
            isTrue = true;
            ViewBag.IsTrue = isTrue;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", review.ClientId);
            ViewBag.TherapistId = new SelectList(db.MassageTherapists, "Id", "FirstName", review.TherapistId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Review review)
        {
            if (ModelState.IsValid)
            { 
                var comment = db.Reviews.Include(c => c.Client.ApplicationUser).Where(c => c.Id == id).SingleOrDefault();
                comment.Comment = review.Comment;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = review.TherapistId });
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", review.ClientId);
            ViewBag.TherapistId = new SelectList(db.MassageTherapists, "Id", "FirstName", review.TherapistId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Reviews.Remove(review);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = review.TherapistId });
            }
            return View(review);
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
