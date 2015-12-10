using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelTime.Models
{
    public class StepIndexViewModel
    {
        public List<Step> Steps { get; set; }
        public List<Attraction> Attractions { get; set; }
        public string selectedAttraction { get; set; }
        public int tripId { get; set; }
    }
}