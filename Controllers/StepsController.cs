using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TravelTime.Models;

namespace TravelTime.Controllers
{
    public class StepsController : Controller
    {
        private DB_9DF51E_traveltimeEntities db = new DB_9DF51E_traveltimeEntities();
        private AttractionManager attractionM = new AttractionManager();

        // GET: Steps
        public ActionResult Index()
        {
            var step = db.Step.Include(s => s.Attraction1).Include(s => s.Trip);
            return View(step.ToList());
        }

        // GET: Steps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Step step = db.Step.Find(id);
            if (step == null)
            {
                return HttpNotFound();
            }
            return View(step);
        }

        // GET: Steps/Create
        public ActionResult Create(int? tripId, DateTime? date)
        {
            Trip t = db.Trip.Find(tripId);
            AddStepViewModel asv = new AddStepViewModel(attractionM.getAttractions(t.City, 20));
            asv.tripId = t.Id;
            asv.Date = System.Convert.ToDateTime(date);

            ViewBag.AttractionId = new SelectList(asv.Attractions, "web_id", "Display", asv.AttractionId);

            return View(asv);
        }

        // POST: Steps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Time,TripId,AttractionId,Date")] AddStepViewModel step)
        {
            if (ModelState.IsValid)
            {
                Attraction a;
                List<Attraction> attractions = db.Attraction.Where(att => att.web_id == step.AttractionId).ToList();
                if (attractions.Count < 0)
                    a = attractions[0];
                else
                    a = attractionM.getAttraction(step.AttractionId);
                Step s = new Step();
                DateTime d = new DateTime(step.Date.Year, step.Date.Month, step.Date.Day, step.Time.Hour, step.Time.Minute, step.Time.Second);
                s.Attraction1 = a;
                s.Name = a.Name;
                s.TripId = step.tripId;
                s.Time = d;
                s.Latitude = a.Latitude;
                s.Longitude = a.Longitude;
                db.Attraction.Add(a);
                db.Step.Add(s);
                db.SaveChanges();
                return RedirectToAction("Day","Trip", new { tripId = step.tripId, Date = step.Date }); 
            }

            Trip t = db.Trip.Find(step.tripId);
            AddStepViewModel asv = new AddStepViewModel(attractionM.getAttractions(t.City, 20));

            ViewBag.AttractionId = new SelectList(asv.Attractions, "web_id", "Display", asv.AttractionId);

            return View();
        }

        // GET: Steps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Step step = db.Step.Find(id);
            if (step == null)
            {
                return HttpNotFound();
            }
            ViewBag.Attraction = new SelectList(db.Attraction, "Id", "web_id", step.Attraction);
            ViewBag.TripId = new SelectList(db.Trip, "Id", "Name", step.TripId);
            return View(step);
        }

        // POST: Steps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Time,TripId,AttractionId")] AddStepViewModel step)
        {
            /*if (ModelState.IsValid)
            {
                db.Entry(step).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Attraction = new SelectList(db.Attraction, "Id", "web_id", step.Attraction);
            ViewBag.TripId = new SelectList(db.Trip, "Id", "Name", step.TripId);*/
            return View();
        }

        // GET: Steps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Step step = db.Step.Find(id);
            if (step == null)
            {
                return HttpNotFound();
            }
            return View(step);
        }

        // POST: Steps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Step step = db.Step.Find(id);
            db.Step.Remove(step);
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
