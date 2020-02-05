using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FinalCapstone.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace FinalCapstone.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext db;
        private string userId;
        public ClientsController()
        {
            db = new ApplicationDbContext();
            userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        //GET: Clients
        public ActionResult Index()
        {
            string today = DateTime.Today.ToString("MM/dd/yyyy");
            var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.ApplicationId == userId).SingleOrDefault();
            var clients = db.ClientTherapists.Include(c => c.Client).Include(c => c.MassageTherapist).Where(c => c.TherapistId == therapist.Id).Select(c => c.Client).ToList();
            List<Client> pref = new List<Client>();
            foreach (var item in clients)
            {
                var currentPref = db.ClientPrefs.Include(p => p.Client).Where(p => p.AppointmentDate == today&& p.ClientId == item.Id).Select(p=>p.Client).Single();
                pref.Add(currentPref);
            }
            return View(pref);
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            Client client = null;
            if (id == null)
            {
                client = db.Clients.Include(c => c.ApplicationUser).Where(c => c.ApplicationId == userId).SingleOrDefault();

            }
            else
            {
                client = db.Clients.Find(id);
            }
            
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                client.ApplicationId = userId;
                await GetClientCoord(client);
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Create","ClientPrefs");
            }

            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", client.ApplicationId);
            return View(client);
        }
        public async Task<ActionResult> GetClientCoord(Client thisClient)
        {
            string location = thisClient.Street + "+" + thisClient.City + "+" + thisClient.State + "+" + thisClient.Zip;
            HttpClient client = new HttpClient();
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + location + "&key=" + GoogleMapKey.myKey;
            HttpResponseMessage response = await client.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                GeoModel GeoResult = JsonConvert.DeserializeObject<GeoModel>(result);
                thisClient.Latitude = GeoResult.results[0].geometry.location.lat;
                thisClient.Longitude = GeoResult.results[0].geometry.location.lng;
                return View(thisClient);
            }

            return View(thisClient);
        }


        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", client.ApplicationId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", client.ApplicationId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
