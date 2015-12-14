using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
        public ActionResult Index(int? tripId)
        {
            var trip = db.Trip.Find(System.Convert.ToInt32(tripId));
            StepIndexViewModel result = new StepIndexViewModel();

            List<Step> Steps = trip.Step.Where(s => s.Attraction != null).ToList();
            List<Attraction> Attractions = attractionM.getAttractions(trip.City, Steps.Count() + 15);

            foreach (Step s in Steps)
            {
                int toRemove = -1;
                toRemove = Attractions.FindIndex(a => a.web_id == s.Attraction1.web_id);
                if (toRemove > -1)
                {
                    Attractions.RemoveAt(toRemove);
                }
            }

            result.Attractions = Attractions;
            result.tripId = trip.Id;
            result.Steps = trip.Step.ToList();
            result.Steps.Reverse();
            ViewBag.selectedAttraction = new SelectList(result.Attractions, "web_id", "Display");
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(StepIndexViewModel result)
        {
            Attraction a;
            List<Attraction> attractions = db.Attraction.Where(att => att.web_id == result.selectedAttraction).ToList();
            if (attractions.Count < 0)
                a = attractions[0];
            else
                a = attractionM.getAttraction(result.selectedAttraction);
            Step s = new Step(a);
            s.TripId = result.tripId;

            db.Attraction.Add(a);
            db.Step.Add(s);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
               
            }

            return RedirectToAction("Index", new { tripId = result.tripId});
        }

        // GET: Steps/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Step step = db.Step.Where(s => s.Id == id).ToList()[0];
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
                Step s = new Step(a);
                DateTime d = new DateTime(step.Date.Year, step.Date.Month, step.Date.Day, step.Time.Hour, step.Time.Minute, step.Time.Second);
                
                s.TripId = step.tripId;
                s.Time = d;
                
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

        // GET: Steps/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Step> step = db.Step.Where(s=>s.Id==id).ToList();
            if (step.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(step[0]);
        }

        // POST: Steps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Step step = db.Step.Where(s => s.Id == id).ToList()[0];
            int tripId = step.TripId;
            db.Step.Remove(step);
            db.SaveChanges();
            return RedirectToAction("Index", new {tripId = tripId});
        }

        public JsonResult getAttraction(string web_id)
        {
            return Json(attractionM.getAttraction(web_id),JsonRequestBehavior.AllowGet);
        }

        public ActionResult TripIndex(int tripId)
        {
            Trip trip = db.Trip.Find(tripId);
            return RedirectToAction("Index", "Trip", new { id = trip.UserId });
        }

        public ActionResult Day(DateTime? date, int? tripId)
        {
            return RedirectToAction("Day", "Trip", new { date = date, tripId = tripId });
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
