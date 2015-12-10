using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TravelTime.Models;

namespace TravelTime
{
    public class ItineraryManager
    {
        private DB_9DF51E_traveltimeEntities db = new DB_9DF51E_traveltimeEntities();
        AttractionManager AttM = new AttractionManager();

        public void run(int tripId)
        {
            clearTime(tripId);

            Trip trip = db.Trip.Find(tripId);
            List<Step> steps = trip.Step.ToList();

            List<Step> nightSpot = getNightSpot(steps);

            int nbDays = ((DateTime)trip.End - (DateTime)trip.Beginning).Days;

            if (steps.Count() != 0)
            {
                int nbSteps = steps.Count();
                int stepByDay = 1;
                Step currentStep = steps[0];
                DateTime currentH = (DateTime)trip.Beginning;
                currentH = ChangeTime(currentH, 10, 0, 0, 0);
                currentStep.Time = currentH;
                db.Entry(currentStep).State = EntityState.Modified;
                steps.Remove(currentStep);

                while (steps.Count() > 0)
                {
                    currentStep = getNearestStep(currentStep, steps);
                    steps.Remove(currentStep);

                    if (stepByDay > (nbSteps / nbDays))
                    {
                        currentH = currentH.AddDays(1);
                        currentH = ChangeTime(currentH, 10, 0, 0, 0);
                        stepByDay = 1;
                    }
                    else
                    {
                        currentH = currentH.AddHours(8 / (nbSteps / nbDays));
                        stepByDay++;
                    }
                    currentStep.Time = currentH;
                    db.Entry(currentStep).State = EntityState.Modified;
                }
            }
            if(nightSpot.Count()>0)
            {
                Step currentStep = nightSpot[0];
                DateTime currentH = (DateTime)trip.Beginning;
                currentH = ChangeTime(currentH, 21, 0, 0, 0);
                currentStep.Time = currentH;
                db.Entry(currentStep).State = EntityState.Modified;
                nightSpot.Remove(currentStep);
                int stepByDay = 1;
                int nbSteps = nightSpot.Count();

                while (nightSpot.Count() > 0)
                {
                    currentStep = getNearestStep(currentStep, nightSpot);
                    nightSpot.Remove(currentStep);

                    if (stepByDay > (nbSteps / nbDays))
                    {
                        currentH = currentH.AddDays(1);
                        currentH = ChangeTime(currentH, 21, 0, 0, 0);
                        stepByDay = 1;
                    }
                    else
                    {
                        currentH = currentH.AddHours(2 / (nbSteps / nbDays));
                        stepByDay++;
                    }
                    currentStep.Time = currentH;
                    db.Entry(currentStep).State = EntityState.Modified;
                }
                db.SaveChanges();
            }

        }


        private Step getNearestStep(Step step, List<Step> steps)
        {
            Step result = steps[0];
            double distance = Math.Pow(step.Latitude.Value - result.Latitude.Value, 2) + Math.Pow(step.Longitude.Value - result.Longitude.Value,2);

            foreach(Step s in steps)
            {
                double d = Math.Pow(step.Latitude.Value - s.Latitude.Value, 2) + Math.Pow(step.Longitude.Value - s.Longitude.Value, 2);
                if (d < distance)
                {
                    distance = d;
                    result = s;
                }
            }
            return result;
        }

        private void clearTime(int tripId)
        {
            List<Step> steps = db.Step.Where(s => s.TripId == tripId).ToList();

            foreach(Step s in steps)
            {
                s.Time = null;
                db.Entry(s).State = EntityState.Modified;
            }
            db.SaveChanges();
        }

        private DateTime ChangeTime(DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }

        private List<Step> getNightSpot(List<Step> steps)
        {
            List<Step> result = new List<Step>();
            dynamic categories = AttM.getCategories();
                
            foreach (Step s in steps)
            {
                foreach(dynamic c in categories)
                {
                    if(c.id == "4d4b7105d754a06376d81259"){
                        dynamic d = searchIn(c, s.Attraction1.Categorie);
                        if (d != null)
                        {
                            result.Add(s);
                        }
                    }
                }
            }
            foreach(Step s in result)
            {
                steps.Remove(s);
            }
            return result;
        }

        private dynamic searchIn(dynamic node, string cat)
        {
            dynamic result=null;
            foreach (dynamic n in node.categories)
            {
                if (n.name == cat)
                {
                    result = n;
                    break;
                }
                else
                {
                    if (n.categories.Count > 0)
                        result = searchIn(n, cat);
                }
            }
            return result;
        }

    }
}