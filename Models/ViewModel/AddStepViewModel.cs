using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelTime.Models
{
    public class AddStepViewModel
    {
        public AddStepViewModel()
        {

        }

        public AddStepViewModel(DateTime Time,int TripId,string AttractionId)
        {
            this.Time = Time;
            this.tripId = TripId;
            this.AttractionId = AttractionId;
        }

        public List<Attraction> Attractions { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public int Duration { get; set; }
        public string AttractionId { get; set; }
        public int tripId { get; set; }
        public DateTime Date { get; set; }

        public AddStepViewModel(List<Attraction> attractions)
        {
            Attractions = attractions;
        }
    }
}