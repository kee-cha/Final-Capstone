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
    public class MassageTherapistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MassageTherapists
        public ActionResult Index(bool? interest, bool? location)
        {
            var userId = User.Identity.GetUserId();
            var therapist = db.MassageTherapists.Include(m => m.ApplicationUser).ToList();
            var client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
            if (location == true)
            {
                therapist = FilterByLocation(therapist, client.Id);
            }

            if (interest == true)
            {
                therapist = Filter(therapist, client.Id);
            }

            return View(therapist);
        }

        public List<MassageTherapist> Filter(List<MassageTherapist> therapists, int id)
        {
            List<MassageTherapist> therapist = null;
            var pref = db.ClientPrefs.Where(p => p.ClientId == id).SingleOrDefault();
            //if (therapists == null)
            //{
            //     therapist = therapists.Where(m => m.Gender == pref.TherapistGender && m.Specialty == pref.TherapistSpecialty).ToList();
            //    return therapist;
            //}
            if (true)
            {

            }
            therapist = therapists.Where(m => m.Gender == pref.TherapistGender && m.Specialty == pref.TherapistSpecialty).ToList();
            return therapist;
        }

        public List<MassageTherapist> FilterByLocation(List<MassageTherapist> therapist, int id)
        {
            var client = db.Clients.Where(c => c.Id == id).SingleOrDefault();
            if (therapist == null)
            {
                var therapists = db.MassageTherapists.Where(t => t.Zip == client.Zip).ToList();
                return therapists;
            }
            therapist = db.MassageTherapists.Where(m => m.Zip == client.Zip).ToList();
            return therapist;
        }
        // GET: MassageTherapists/Details/5
        public ActionResult Details(int? id, MTAppointViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            MassageTherapist therapist = null;

            if (id != null)
            {
                therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.Id == id).SingleOrDefault();
                var client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
                ViewBag.ClientLat = client.Latitude;
                ViewBag.ClientLng = client.Longitude;
            }
            else
            {
                therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.ApplicationId == userId).SingleOrDefault();
            }
            if (therapist == null)
            {
                return HttpNotFound();
            }

            ViewBag.Map = "https://maps.googleapis.com/maps/api/js?key=" + GoogleMapKey.myKey + "&callback=initMap";
            return View(therapist);
        }
        [HttpPost]
        public ActionResult Details(int? id, MTAppointViewModel model, string name)
        {            
            var userId = User.Identity.GetUserId();
            var client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
            model.Client = client;
            var pref = db.ClientPrefs.Where(p => p.ClientId == client.Id).SingleOrDefault();
            model.MassageTherapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.Id == id).SingleOrDefault();
            pref.AppointmentDate = model.ClientPref.AppointmentDate;
            db.SaveChanges();
            return RedirectToAction("MakeAppointment", new { mtId = model.MassageTherapist.Id, cpId = model.ClientPref.Id });
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
        public async Task<ActionResult> Create(MassageTherapist massageTherapist)
        {
            var session = Convert.ToInt32(massageTherapist.SessionPerDay);
            if (ModelState.IsValid)
            {
                massageTherapist.ApplicationId = User.Identity.GetUserId();
                await GetCoord(massageTherapist);
                db.MassageTherapists.Add(massageTherapist);
                db.SaveChanges();
                return RedirectToAction("LogOut", "Account");
            }

            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", massageTherapist.ApplicationId);
            return View(massageTherapist);
        }

        public async Task<ActionResult> GetCoord(MassageTherapist therapist)
        {
            string location = therapist.Street + "+" + therapist.City + "+" + therapist.State + "+" + therapist.Zip;
            HttpClient client = new HttpClient();
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + location + "&key=" + GoogleMapKey.myKey;
            HttpResponseMessage response = await client.GetAsync(url);
            string result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                GeoModel GeoResult = JsonConvert.DeserializeObject<GeoModel>(result);
                therapist.Latitude = GeoResult.results[0].geometry.location.lat;
                therapist.Longitude = GeoResult.results[0].geometry.location.lng;
                return View(therapist);
            }

            return View(therapist);
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
        public ActionResult Edit(MassageTherapist massageTherapist)
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

        public ActionResult SetSchedule(int? id)
        {
            MTAppointViewModel viewModel = new MTAppointViewModel();
            viewModel.MassageTherapist = db.MassageTherapists.Where(t => t.Id == id).SingleOrDefault();
            viewModel.DayPref = new SelectList(new List<string> { "Morning", "Afternoon", "Evening" });
            viewModel.SetTime = new SelectList(new List<string>() { "7:00 AM", "8:00 AM", "9:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "1:00 PM", "2:00 PM", "3:00 PM", "4:00 PM", "5:00 PM", "6:00 PM", "7:00 PM", "8:00 PM", "9:00 PM", "10:00 PM" });
            return View("Schedule", viewModel);
        }
        [HttpPost]
        public ActionResult SetSchedule(int? id, MTAppointViewModel update)
        {
            var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.Id == id).SingleOrDefault();

            therapist.Schedule1 = update.MassageTherapist.Schedule1;
            therapist.Schedule2 = update.MassageTherapist.Schedule2;
            therapist.Schedule3 = update.MassageTherapist.Schedule3;
            therapist.Schedule4 = update.MassageTherapist.Schedule4;
            db.SaveChanges();
            return View("Details", therapist);

        }
        public ActionResult MakeAppointment(int? mtId, int? cpId)
        {
            MTAppointViewModel model = new MTAppointViewModel();
            var userId = User.Identity.GetUserId();
            model.Client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
            model.ClientPref = db.ClientPrefs.Where(p => p.Id==cpId).SingleOrDefault();
            model.MassageTherapist = db.MassageTherapists.Where(t => t.Id == mtId).SingleOrDefault();
            
            return View("Schedule", model);
        }

        [HttpPost]
        public ActionResult MakeAppointment(MTAppointViewModel viewModel)
        {
            var userId = User.Identity.GetUserId();
            var client = db.Clients.Include(c => c.ApplicationUser).Where(c => c.ApplicationId == userId).SingleOrDefault();
            var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.Id == viewModel.MassageTherapist.Id).SingleOrDefault();
            var clientPref = db.ClientPrefs.Include(c => c.Client).Where(c => c.ClientId == client.Id).SingleOrDefault();
            if (viewModel.MassageTherapist.IsOpen1 == true)
            {
                if (clientPref.AppointmentTime == null)
                {
                    therapist.IsOpen1 = true;
                    clientPref.AppointmentTime = therapist.Schedule1;

                    therapist.AppointmentDate = clientPref.AppointmentDate;
                }
            }
            if (viewModel.MassageTherapist.IsOpen2 == true)
            {
                if (clientPref.AppointmentTime == null)
                {
                    therapist.IsOpen2 = true;
                    clientPref.AppointmentTime = therapist.Schedule2;
                    therapist.AppointmentDate = clientPref.AppointmentDate;
                }
            }
            if (viewModel.MassageTherapist.IsOpen3 == true)
            {
                if (clientPref.AppointmentTime == null)
                {
                    therapist.IsOpen3 = true;
                    clientPref.AppointmentTime = therapist.Schedule3;
                    therapist.AppointmentDate = clientPref.AppointmentDate;
                }
            }
            if (viewModel.MassageTherapist.IsOpen4 == true)
            {
                if (clientPref.AppointmentTime == null)
                {
                    therapist.IsOpen3 = true;
                    clientPref.AppointmentTime = therapist.Schedule4;
                    therapist.AppointmentDate = clientPref.AppointmentDate;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Details", "ClientPrefs");
        }
        public ActionResult SetDate(int? id)
        {
            MTAppointViewModel viewModel = new MTAppointViewModel();
            viewModel.MassageTherapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.Id == id).SingleOrDefault();
            var userId = User.Identity.GetUserId();
            var client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
            var pref = db.ClientPrefs.Where(p => p.ClientId == client.Id).SingleOrDefault();
            viewModel.ClientPref = pref;
            return View(viewModel);
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
