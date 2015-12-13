using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelTime.Models
{
    public class Point
    {
        private double? latitude;
        private double? longitude;

        public Point(double? latitude, double? longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }


}