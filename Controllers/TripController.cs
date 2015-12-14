using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TravelTime.Models;

namespace TravelTime.Controllers
{
    public class TripController : Controller
    {
        private DB_9DF51E_traveltimeEntities db = new DB_9DF51E_traveltimeEntities();
        private AttractionManager attractionM = new AttractionManager();

        // GET: Trip
        public ActionResult Index(int? id)
        {
            try
            {
                TripIndexViewModel result = new TripIndexViewModel();
                result.User = db.User.Find(System.Convert.ToInt64(id));
                result.Trips = result.User.Trip.ToList();
                return View(result);
            }
            catch(Exception e)
            {
                return RedirectToAction("Index", "Users");
            }
            
        }

        public ActionResult Run(int? tripId)
        {

            ItineraryManager ItM = new ItineraryManager();

            Thread myThread;
            myThread = new Thread(()=>ItM.run(tripId.Value));
            myThread.Start();
            return RedirectToAction("DaysIndex", new { tripId = tripId });
        }

        public ActionResult DaysIndex(int? tripId)
        {

            List<Day> days = new List<Day>();
            if (tripId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(tripId);
            if (trip == null)
            {
                return HttpNotFound();
            }

            int dure = ((DateTime)trip.End - (DateTime)trip.Beginning).Days;

            for (int d = 0; d< dure; d++)
            {
                days.Add(new Day(((DateTime)trip.Beginning).AddDays(System.Convert.ToDouble(d)),System.Convert.ToInt32(tripId)));
            }

            var step = db.Step.Where(s => s.TripId == tripId);
            /*foreach(Step s in step)
            {
                foreach(Day d in days)
                {
                    if(((DateTime)s.Time).Day == (d.Date).Day)
                    {

                    }
                }
            }*/
            return View(days.ToList());
        }

        public ActionResult Day(DateTime? date, int? tripId)
        {
            if (tripId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(tripId);
            if (trip == null)
            {
                return HttpNotFound();
            }
            var step = db.Step
                .Where(s => s.TripId == tripId)
                .Where(s => ((DateTime)s.Time).Day == ((DateTime)date).Day)
                .Where(s => ((DateTime)s.Time).Month == ((DateTime)date).Month)
                .Where(s => ((DateTime)s.Time).Year == ((DateTime)date).Year)
                .Include(s => s.Attraction1);

            Day day = new Day();
            day.TripId = System.Convert.ToInt32(tripId);
            day.Steps = step.OrderBy(o => o.Time).ToList();
            day.Date = System.Convert.ToDateTime(date);
            
            return View(day);
        }


        // GET: Trip/Points/5
        public ActionResult Points(DateTime? date, int? tripId)
        {

            if (tripId == null)
            {
                return new JsonResult { Data = new { valid = false } };
            }
            Trip trip = db.Trip.Find(tripId);
            if (trip == null)
            {
                return new JsonResult { Data = new { valid = false } };
            }

            var step = db.Step
                .Where(s => s.TripId == tripId)
                .Where(s => ((DateTime)s.Time).Day == ((DateTime)date).Day)
                .Where(s => ((DateTime)s.Time).Month == ((DateTime)date).Month)
                .Where(s => ((DateTime)s.Time).Year == ((DateTime)date).Year)
                .Include(s => s.Attraction1);

            List<Double?[]> points = new List<Double?[]>();
            List<String> titles = new List<String>();
            

            foreach (Step s in step.OrderBy(o => o.Time))
            {
                double?[] point = { s.Id, s.Latitude, s.Longitude };
                points.Add(point);
                titles.Add(s.Name);
            }

            return Json(JsonConvert.SerializeObject(new { pts = points,
            tts = titles,
            tid = tripId,
            dt = date}), JsonRequestBehavior.AllowGet);
        }

        // GET: Trip/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trip/Create
        public ActionResult Create(int userId)
        {
            Trip t = new Trip();
            t.UserId = userId;
            return View(t);
        }

        // POST: Trip/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Trip.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.User, "Id", "Firstname", trip.UserId);
            return View(trip);
        }

        // GET: Trip/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.User, "Id", "Firstname", trip.UserId);
            return View(trip);
        }

        // POST: Trip/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,City,Beginning,End,UserId")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.User, "Id", "Firstname", trip.UserId);
            return View(trip);
        }

        // GET: Trip/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trip.Find(id);
            db.Trip.Remove(trip);
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

        public ActionResult CreateStep(int? tripId, DateTime? date)
        {
            return RedirectToAction("Create", "Steps", new { tripId = tripId, date = date });
        }

        public ActionResult Plan(int? tripId)
        {
            return RedirectToAction("Index", "Steps", new { tripId = tripId});
        }
    }
}
