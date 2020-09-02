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
        public IEnumerable<Location> test([FromBody]PL pl)
        {
            if (pl == null )
            { 
                return null;
            }

            return searchLocation(pl.a);
        }

        [HttpPost("select")]
        public void select([FromBody] Location pl)
        {
            searchCoordinates(pl);
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

        private void searchCoordinates(Location loc)
        {
            ParseJson parse=new ParseJson();
            if (validCoordinates(loc))
            {
                Location locTemp=changeNoDecimals(loc);
                string coordSMHI ="lon/"+ locTemp.lon + "/lat/" + locTemp.lat + "/data.json";
                string coordYR = "compact?lat="+ locTemp.lat + "&lon=" + locTemp.lon;
                WeatherDetail[] vaderSHMI = parse.ParseUrlSMHI("https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/", coordSMHI);
                RootobjectYR vader= parse.ParseUrlYRAsync("https://api.met.no/weatherapi/locationforecast/2.0/", coordYR);
                string denniS = "dsd";
            }
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

