//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TravelTime.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Step
    {
        public Step()
        {

        }

        public Step(Attraction a)
        {
            Name = a.Name;
            Latitude = a.Latitude;
            Longitude = a.Longitude;
            Attraction1 = a;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public int TripId { get; set; }
        public Nullable<int> Attraction { get; set; }
        public string Comment { get; set; }
        public string Media { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
    
        public virtual Attraction Attraction1 { get; set; }
        public virtual Trip Trip { get; set; }
    }
}