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
    
    public partial class Attraction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attraction()
        {
            this.Step = new HashSet<Step>();
        }
    
        public int Id { get; set; }
        public string web_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Address { get; set; }
        public Nullable<double> Latitude { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
        public string Opening { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Categorie { get; set; }
        public string Price { get; set; }
        public Nullable<double> Longitude { get; set; }

        public string Display { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Step> Step { get; set; }
    }
}