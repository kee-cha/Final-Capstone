using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
        private ApplicationDbContext db;
        private string userId;
        public MassageTherapistsController()
        {
            db = new ApplicationDbContext();
            userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        // GET: MassageTherapists
        public ActionResult Index(bool? interest, bool? location, bool? Morning, bool? AFternoon, bool? Evening)
        {

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
            if (Morning == true)
            {
                therapist = FilterByDayPref(therapist, "Morning");
            }
            if (AFternoon == true)
            {
                therapist = FilterByDayPref(therapist, "Afternoon");
            }
            if (Evening == true)
            {
                therapist = FilterByDayPref(therapist, "Evening");
            }
            return View(therapist);
        }
        public ActionResult FilterByToday()
        {
            string today = DateTime.Today.ToString();
            var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.ApplicationId == userId).SingleOrDefault();
            var appointments = db.ClientPrefs.Where(p => p.AppointmentDate == today).SingleOrDefault();
            var todayClient = db.Clients.Include(c => c.ApplicationUser).Where(c => c.Id == appointments.ClientId).ToList();
            return View(todayClient);
        }
        public ActionResult MassageAppt()
        {
            var client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
            var therapists = db.ClientTherapists.Include(t => t.Client).Include(t => t.MassageTherapist).Where(t => t.ClientId == client.Id).Select(t => t.MassageTherapist).ToList();
            return View("Index", therapists);
        }
        [HttpPost]
        public ActionResult MassageAppt(string word)
        {
            var client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
            var therapists = db.ClientTherapists.Include(t => t.Client).Include(t => t.MassageTherapist).Where(t => t.ClientId == client.Id).Select(t => t.MassageTherapist).ToList();
            return View("Index", therapists);
        }
        #region Client Filter
        public List<MassageTherapist> Filter(List<MassageTherapist> therapists, int id)
        {
            List<MassageTherapist> therapist = null;
            var pref = db.ClientPrefs.Where(p => p.ClientId == id).SingleOrDefault();
            therapist = therapists.Where(m => m.Gender == pref.TherapistGender && m.Specialty == pref.TherapistSpecialty).ToList();
            return therapist;
        }

        public List<MassageTherapist> FilterByLocation(List<MassageTherapist> therapist, int id)
        {
            var client = db.Clients.Where(c => c.Id == id).SingleOrDefault();
            therapist = therapist.Where(m => m.Zip == client.Zip).ToList();
            return therapist;
        }
        public List<MassageTherapist> FilterByDayPref(List<MassageTherapist> therapists, string time)
        {
            therapists = therapists.Where(t => t.TimeFramePref == time).ToList();
            return therapists;
        }
        #endregion
        // GET: MassageTherapists/Details/5
        public ActionResult Details(int? id, MTAppointViewModel viewModel)
        {
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
            var client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
            model.Client = client;
            var pref = db.ClientPrefs.Where(p => p.ClientId == client.Id).SingleOrDefault();
            if (model == null)
            {
                model.ClientPref = pref;
            }
            model.MassageTherapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.Id == id).SingleOrDefault();
            string date = model.ClientPref.AppointmentDate;
            DateTime dateFormat = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string appointmentDate = dateFormat.ToString("MM/dd/yyyy");
            pref.AppointmentDate = appointmentDate;
            db.SaveChanges();
            return RedirectToAction("MakeAppointment", new { mtId = model.MassageTherapist.Id, cpId = model.ClientPref.Id });
        }
        public ActionResult CompleteAppt(int? id, bool? complete)
        {
            var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.ApplicationId == userId).SingleOrDefault();
            var pref = db.ClientPrefs.Include(p => p.Client).Where(p => p.Id == id).SingleOrDefault();
            var client = db.Clients.Include(c => c.ApplicationUser).Where(c => c.Id == pref.ClientId).SingleOrDefault();
            var clients = db.ClientTherapists.Include(c => c.Client).Include(t => t.MassageTherapist).Where(c => c.ClientId == client.Id && c.TherapistId == therapist.Id).FirstOrDefault();
            if (therapist.Schedule1 == pref.AppointmentTime)
            {
                therapist.IsOpen1 = false;                
            }
            else if (therapist.Schedule2 == pref.AppointmentTime)
            {
                therapist.IsOpen2 = false;
            }
            else if (therapist.Schedule3 == pref.AppointmentTime)
            {
                therapist.IsOpen3 = false;
            }
            else if (therapist.Schedule4 == pref.AppointmentTime)
            {
                therapist.IsOpen4 = false;
            }
            db.ClientTherapists.Remove(clients);
            pref.AppointmentDate = null;
            pref.AppointmentTime = null;
            db.SaveChanges();
            return RedirectToAction("Details", "Clients", new { id, complete });

        }
        // GET: MassageTherapists/Create
        public ActionResult Create()
        {
            MTAppointViewModel viewModel = new MTAppointViewModel();
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email");
            return View(viewModel);
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
                massageTherapist.ApplicationId = userId;
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
        public async Task<ActionResult> Edit(MassageTherapist massageTherapist)
        {
            if (ModelState.IsValid)
            {
                var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.ApplicationId == userId).SingleOrDefault();
                therapist.FirstName = massageTherapist.FirstName;
                therapist.LastName = massageTherapist.LastName;
                if (therapist.Street != massageTherapist.Street || therapist.City != massageTherapist.City || therapist.State != massageTherapist.State || therapist.Zip != massageTherapist.Zip)
                {
                    await GetCoord(massageTherapist);
                }
                therapist.Street = massageTherapist.Street;
                therapist.City = massageTherapist.City;
                therapist.State = massageTherapist.State;
                therapist.Zip = massageTherapist.Zip;
                therapist.TimeFramePref = massageTherapist.TimeFramePref;
                therapist.Specialty = massageTherapist.Specialty;
                therapist.SessionPerDay = massageTherapist.SessionPerDay;
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
            if (viewModel.MassageTherapist.TimeFramePref == "Morning")
            {
                viewModel.SetTime = new SelectList(new List<string>() { "7:00 AM", "8:00 AM", "9:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "1:00 PM" });
            }
            else if (viewModel.MassageTherapist.TimeFramePref == "Afternoon")
            {
                viewModel.SetTime = new SelectList(new List<string>() { "12:00 PM", "1:00 PM", "2:00 PM", "3:00 PM", "4:00 PM", "5:00 PM", "6:00 PM" });
            }
            else if (viewModel.MassageTherapist.TimeFramePref == "Evening")
            {
                viewModel.SetTime = new SelectList(new List<string>() { "4:00 PM", "5:00 PM", "6:00 PM", "7:00 PM", "8:00 PM", "9:00 PM", "10:00 PM" });
            }

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
            model.Client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
            model.ClientPref = db.ClientPrefs.Where(p => p.Id == cpId).SingleOrDefault();
            model.MassageTherapist = db.MassageTherapists.Where(t => t.Id == mtId).SingleOrDefault();

            return View("Schedule", model);
        }

        [HttpPost]
        public ActionResult MakeAppointment(MTAppointViewModel viewModel)
        {
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
            ClientTherapist clientTherapist = new ClientTherapist();
            clientTherapist.ClientId = client.Id;
            clientTherapist.TherapistId = therapist.Id;
            db.ClientTherapists.Add(clientTherapist);
            db.SaveChanges();
            return RedirectToAction("Details", "Clients");
        }
        public ActionResult SetDate(int? id)
        {
            MTAppointViewModel viewModel = new MTAppointViewModel();
            viewModel.MassageTherapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.Id == id).SingleOrDefault();
            var client = db.Clients.Where(c => c.ApplicationId == userId).SingleOrDefault();
            var pref = db.ClientPrefs.Where(p => p.ClientId == client.Id).SingleOrDefault();
            viewModel.ClientPref = pref;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Rating(int? id, string unlike)
        {
            var therapist = db.MassageTherapists.Include(t => t.ApplicationUser).Where(t => t.Id == id).Single();
            switch (unlike)
            {
                case "UnLike":
                    therapist.LikeCounter--;
                    therapist.TotalCounter--;
                    break;
                case "Like":
                    therapist.LikeCounter++;
                    therapist.TotalCounter++;
                    break;
                case "UnDisLike":
                    therapist.TotalCounter--;
                    break;
                case "DisLike":
                    therapist.TotalCounter++;
                    break;
                default:
                    break;
            }
            double avgRating = (therapist.LikeCounter / therapist.TotalCounter) * 10;
            therapist.Rating = Math.Round(avgRating, 1);
            db.SaveChanges();
            return View(therapist);
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
