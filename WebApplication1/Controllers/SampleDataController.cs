using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.API;
using WebApplication1.Model;
using static WebApplication1.Model.YRModel;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        private int count;
        private IEnumerable<Location> location;

        public SampleDataController()
        {
            count = 0;
        }



        [HttpPost("test")]
        public Weather[] test([FromBody]PL pl)
        {
            if (pl == null )
            { 
                return null;
            }

            Weather[] test= sanka(pl.a);
            return test;
        }

        public Weather[] sanka(string pl)
        {
            return SearchCoordinates(searchLocation(pl));
        }

        [HttpPost("select")]
        public Weather select([FromBody] Location pl)
        {
           
           Weather test = SearchOneCoordinates(pl);
            string den = "a";

           return test;

        }

        private Location getCoordinates(String place)
        {
           Location loc=null;
           foreach(var item in location)
            {
                if (place.Equals(item.place))
                {
                    loc = item;
                }
            }

            return loc;
        }

      
        public Weather[] SearchCoordinates(IEnumerable<Location> loc)
        {
            
            Weather[] vaderlista = new Weather[loc.Count()];
            int i = 0;

            foreach (var item in loc)
            {
                if (validCoordinates(item))
                {
                    Location locTemp = changeNoDecimals(item);
                    Weather vader = SearchOneCoordinates(locTemp);
                    vaderlista[i] = vader;
                    i++;
                }
            }
            return vaderlista;
           
        }

        public Weather SearchOneCoordinates(Location loc)
        {

            ParseJson parse = new ParseJson();
            string coordSMHI = "lon/" + loc.lon + "/lat/" + loc.lat + "/data.json";
            string coordYR = "compact?lat=" + loc.lat + "&lon=" + loc.lon;
            WeatherDetail[] vaderSHMI = parse.ParseUrlSMHI("https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/", coordSMHI);
            WeatherDetail[] vaderYR = parse.ParseUrlYR("https://api.met.no/weatherapi/locationforecast/2.0/", coordYR);

            vaderYR = vaderYR.Skip(1).ToArray();
            Weather vader = new Weather
            {
                place = loc.place,
                coord = new Coordinates
                {
                    lat = loc.lat,
                    lon = loc.lon
                }
                ,
                detailSMHI = vaderSHMI,
                detailYR = vaderYR
            };

            return vader;
        }

        private Location changeNoDecimals(Location coordinates)
        {
            Decimal tempLon = Decimal.Parse(coordinates.lon, CultureInfo.InvariantCulture);
            Decimal nyLon=Math.Round(tempLon, 2);
            Decimal tempLat = Decimal.Parse(coordinates.lat, CultureInfo.InvariantCulture);
            Decimal nyLat = Math.Round(tempLat, 2);
            coordinates.lon = nyLon.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            coordinates.lat = nyLat.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

            return coordinates;
        }

        private bool validCoordinates(Location coordinates)
        {
            float lon = float.Parse(coordinates.lon, CultureInfo.InvariantCulture);
            float lat = float.Parse(coordinates.lat, CultureInfo.InvariantCulture);

            if ((lon>= -160 && lon <=160)&&(lat>=-80 && lat<=80))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private IEnumerable<Location> searchLocation(string input) { 

            ParseJsonPlace parse = new ParseJsonPlace();
            location=parse.ReadUrlAsync("https://www.smhi.se/wpt-a/backend_solr/autocomplete/search/", input);
            return location;
        }


        public class PL
        {
            public string a { get; set; }

            public Coordinates coord{get;set;}
        }

    }
}

