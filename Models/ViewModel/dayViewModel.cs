using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelTime.Models
{
    public class dayViewModel
    {
        Day day;
        List<Attraction> attractions;

        public dayViewModel(Day d, List<Attraction> a)
        {
            Day = d;
            Attractions =a;
        }

        public Day Day
        {
            get
            {
                return day;
            }

            set
            {
                day = value;
            }
        }

        public List<Attraction> Attractions
        {
            get
            {
                return attractions;
            }

            set
            {
                attractions = value;
            }
        }
    }
}