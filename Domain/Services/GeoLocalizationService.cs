using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Data;
using Entity;

namespace Domain.Services
{
    public static class GeoLocalizationService
    {

        public static Address GetAddressCoordenates(string street, int number, string city, string postalCode, string province, string country)
        {
            Address address = new Address()
                                  {
                                      City = city,
                                      Country = country,
                                      Number = number,
                                      Street = street,
                                      ZipCode = postalCode
                                  };

            string destinationFormat = "{0} {1}, {2}, {3} Province, {4}";
            var destination = string.Format(destinationFormat, street, number.ToString(), postalCode != string.Empty ? postalCode : city, province, country);
            destination = Uri.EscapeDataString(destination);
            string url = @"http://maps.googleapis.com/maps/api/geocode/xml?address="+destination+"&sensor=false";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);

            WebResponse webResponse = httpRequest.GetResponse();
            Stream dataStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            webResponse.Close();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseFromServer);
            XmlNodeList status = xmlDoc.GetElementsByTagName("status");

            if(status.Item(0).InnerText == "OK")
            {
                XmlNodeList locationNode = xmlDoc.GetElementsByTagName("location");

                address.Latitude = float.Parse(locationNode[0].ChildNodes[0].InnerText);
                address.Longitude = float.Parse(locationNode[0].ChildNodes[1].InnerText);
                return address;
            }
            return null;
        }



        public static List<Branch> NearestBranches(double latitude, double longitude, int distanceKm)
        {
            Data.UnitOfWork unit = new UnitOfWork();
            var branches = unit.BranchRepository.GetAll();

            var nearestBranches = from d in branches
                   where DistanceBetween(latitude, longitude, d.Address.Latitude, d.Address.Longitude) < distanceKm
                   select d;
            return nearestBranches.ToList();
        }

        public static double DistanceBetween(double lat1, double long1, double lat2, double long2)
        {
            double deg2radMultiplier = Math.PI / 180;
            lat1 = lat1 * deg2radMultiplier;
            long1 = long1 * deg2radMultiplier;
            lat2 = lat2 * deg2radMultiplier;
            long2 = long2 * deg2radMultiplier;
            double radius = 6378.137; 
            double dlon = long2 - long1;
            double distance = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(dlon)) * radius;
            return distance;
        }

        public static string[] GetDistanceKmHrs(double latitude, double longitude, Address destination)
        {
            string url = @"http://maps.googleapis.com/maps/api/distancematrix/xml?origins={0},{1}&destinations={2},{3}&mode=driving&language=en-US&sensor=false";
            url = string.Format(url, latitude.ToString(), longitude.ToString(), destination.Latitude, destination.Longitude);

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            WebResponse webResponse = httpRequest.GetResponse();
            Stream dataStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            webResponse.Close();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseFromServer);
            XmlNodeList status = xmlDoc.GetElementsByTagName("status");

            if (status.Item(0).InnerText == "OK")
            {
                XmlNodeList distanceNode = xmlDoc.GetElementsByTagName("distance");
                XmlNodeList durationNode = xmlDoc.GetElementsByTagName("duration");

                List<string> result = new List<string>();
                result.Add(distanceNode[0].ChildNodes[1].InnerText);
                result.Add(durationNode[0].ChildNodes[1].InnerText);
                return result.ToArray();
            }
            return null;
        }






















        //public static Address HostIpToPlaceName(string ip)
        //{
        //    if (ip == "127.0.0.1")
        //    {
        //        ip = "71.117.141.83";
        //        //return string.Empty;
        //    }

        //    string url = "http://ipinfodb.com/ip_query.php?ip={0}&timezone=false";
        //    url = String.Format(url, ip);

        //    var result = XDocument.Load(url);

        //var location = (from x in result.Descendants("Response")
        //                select new Address
        //                           {
        //                               City = (string) x.Element("City"),
        //                               RegionName = (string) x.Element("RegionName"),
        //                               Country = (string) x.Element("CountryName"),
        //                               ZipPostalCode = (string) x.Element("CountryName"),
        //                               Position = new LatLong
        //                                              {
        //                                                  Lat = (float) x.Element("Latitude"),
        //                                                  Long = (float) x.Element("Longitude")
        //                                              }
        //                           }).First();

        //    return location;
        //}

    }
}
