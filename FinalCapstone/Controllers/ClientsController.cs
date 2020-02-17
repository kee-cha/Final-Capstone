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
            var clients = db.ClientTherapists.Include(c => c.Client.ApplicationUser).Include(c => c.Client).Include(c => c.MassageTherapist).Where(c => c.TherapistId == therapist.Id).Select(c => c.Client).ToList();
            List<Client> client = new List<Client>();
            foreach (var item in clients)
            {
                var currentPref = db.ClientPrefs.Include(p => p.Client).Where(p => p.ClientId == item.Id).Single();
                if (currentPref.AppointmentDate == today)
                {
                    client.Add(item);
                }
            }
            return View(client);
        }

        public async Task<ActionResult> GetInjuryInfo()
        {
            InjuryViewModel[] injury = null;
            InjuryViewModel pain = null;
            List<InjuryViewModel> injuries = new List<InjuryViewModel>();
            var thisClient = db.Clients.Include(c => c.ApplicationUser).Where(c => c.ApplicationId == userId).SingleOrDefault();
            var pref = db.ClientPrefs.Include(p => p.Client.ApplicationUser).Where(p => p.ClientId == thisClient.Id).SingleOrDefault();
            HttpClient client = new HttpClient();
            string url = "https://localhost:44333/api/Injuries";
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                injury = JsonConvert.DeserializeObject<InjuryViewModel[]>(jsonResult);
                foreach (var item in injury)
                {
                    if ((pref.HeadPain == true || pref.NeckPain == true) && item.InjuryLocation == "Head")
                    {
                        injuries.Add(item);
                    }
                    else if ((pref.ShoulderPain == true || pref.ArmPain == true || pref.WristHandPain == true) && item.InjuryLocation == "Arm")
                    {
                        injuries.Add(item);
                    }
                    else if ((pref.UpperBackPain == true || pref.LowBackPain == true) && item.InjuryLocation == "Back")
                    {
                        injuries.Add(item);
                    }
                    else if ((pref.HipPain == true) && item.InjuryLocation == "Hip")
                    {
                        injuries.Add(item);
                    }
                    else if ((pref.ThighPain == true || pref.KneeLegPain == true || pref.AnkleFootPain == true) && item.InjuryLocation == "Leg")
                    {
                        injuries.Add(item);
                    }
                }
            }
            return View("Injury", injuries);
        }
        // GET: Clients/Details/5
        public ActionResult Details(int? id, bool? complete, bool? isTrue)
        {
            Client client = null;
            ViewBag.IsTrue = isTrue;
            ViewBag.IsComplete = complete;
            if (id == null)
            {
                var pref = db.ClientPrefs.Include(c => c.Client).Where(c => c.Id == id).FirstOrDefault();
                client = db.Clients.Include(c => c.ApplicationUser).Where(c => c.ApplicationId == userId).SingleOrDefault();
                ViewBag.MyPref = pref;
            }
            else
            {
                var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.ApplicationId == userId).SingleOrDefault();
                client = db.Clients.Include(t => t.ApplicationUser).Where(c => c.Id == id).SingleOrDefault();
                var pref = db.ClientPrefs.Where(p => p.ClientId == client.Id).SingleOrDefault();
                ViewBag.MyPref = pref;
                ViewBag.MTLat = therapist.Latitude;
                ViewBag.MTLng = therapist.Longitude;
            }

            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.Map = "https://maps.googleapis.com/maps/api/js?key=" + GoogleMapKey.myKey + "&callback=initMap";
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
                return RedirectToAction("Create", "ClientPrefs");
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
        public ActionResult Edit(Client client)
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
            var user = db.Users.Where(u => u.Id == userId).SingleOrDefault();
            db.Users.Remove(user);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("LogOut", "Home");
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
