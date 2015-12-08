using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelTime.Models
{
    public class Day
    {
        private DateTime date;
        private List<Step> steps;
        private int tripId;


        public Day()
        {

        }

        public Day(DateTime date, int id)
        {
            this.Date = date;
            this.TripId = id;
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        public List<Step> Steps
        {
            get
            {
                return steps;
            }

            set
            {
                steps = value;
            }
        }

        public int TripId
        {
            get
            {
                return tripId;
            }

            set
            {
                tripId = value;
            }
        }
    }
}