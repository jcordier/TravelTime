using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelTime.Models
{
    public class TripIndexViewModel
    {
        public List<Trip> Trips { get; set; }
        public User User { get; set; }
    }
}