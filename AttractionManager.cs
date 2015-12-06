using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using TravelTime.Models;

namespace TravelTime
{
    public class AttractionManager
    {
        private string client_secret = "0PV3KLUBI1RBHG3EVDLW5LTMXA53SD2GFPJ4VNTR2UW4RRPY";
        private string client_id = "KLSKVBRRASCKSHA54GH1FFQ0CCJWO4V5ARYDZWMLLJHFOJWT";
        private string version = "20130815";

        //public List<Attraction> getAttraction(string city, int nb)
        public List<Attraction> getAttractions(string city, int nb)
        {
            List<Attraction> result = new List<Attraction>();

            string near = city;
            var url = "https://api.foursquare.com/v2/venues/explore?"
                + "client_secret=" + client_secret
                + "&client_id=" + client_id
                + "&v=" + version
                + "&near=" + near;
            //params= {new "client_id"= client_id, "client_secret": client_secret, "near": near, "v": version};
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Clean up the streams and the response.
            dynamic json = JsonConvert.DeserializeObject(responseFromServer);
            
            foreach (dynamic venue in json.response.groups[0].items)
            {
                try
                {
                    Attraction a = new Attraction();
                    a.Name = venue.venue.name;
                    a.Categorie = venue.venue.categories[0].name;
                    a.Address = venue.venue.location.address;
                    a.City = venue.venue.location.city;
                    a.Contact = (venue.venue.contact.phone != null) ? venue.venue.contact.phone : "";
                    a.Description = (venue.tips[0].text != null) ? venue.tips[0].text : "";
                    a.Opening = (venue.venue.hours != null & venue.venue.hours.status != null) ? venue.venue.contact.status : "";
                    a.Latitude = venue.venue.location.lat;
                    a.Longitude = venue.venue.location.lng;
                    a.web_id = venue.venue.id;
                    a.Display = a.Name + " - " + a.Categorie;
                    result.Add(a);
                }
                catch (Exception e)
                {

                }
            }
            reader.Close();
            response.Close();
            return result;
        }

        public Attraction getAttraction(string webId)
        {
            Attraction a = new Attraction();
            
            var url = "https://api.foursquare.com/v2/venues/"+webId + "?"
                + "client_secret=" + client_secret
                + "&client_id=" + client_id
                + "&v=" + version;  
            //params= {new "client_id"= client_id, "client_secret": client_secret, "near": near, "v": version};
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            // Display the status.
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Clean up the streams and the response.
            dynamic json = JsonConvert.DeserializeObject(responseFromServer);
            try
            {
                a.Name = json.response.venue.name;
                a.Categorie = json.response.venue.categories[0].name;
                a.Address = json.response.venue.location.address;
                a.City = json.response.venue.location.city;
                a.Contact = (json.response.venue.contact.phone != null) ? json.response.venue.contact.phone : "";
                a.Description = (json.response.venue.tips.groups[0].items[0].text != null) ? json.response.venue.tips.groups[0].items[0].text : "";
                a.Opening = (json.response.venue.hours != null & json.response.venue.hours.status != null) ? json.response.venue.contact.status : "";
                a.Latitude = json.response.venue.location.lat;
                a.Longitude = json.response.venue.location.lng;
                a.web_id = json.response.venue.id;
                a.Display = a.Name + " - " + a.Categorie;
            }
            catch (Exception e)
            {

            }
            reader.Close();
            response.Close();
            return a;

        }
    }
}