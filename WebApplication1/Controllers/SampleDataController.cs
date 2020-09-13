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
        private object detail;

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

            /*string va = test[1].place;
            String dennis = "nollad";
            if(test[1].detailYR[0].time.Date== DateTime.Today)
            {
                dennis = "Today";
            }*/

            int o = 2;
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

            if (vaderSHMI == null)
            {
                vaderSHMI = new WeatherDetail[1];
                vaderSHMI[0] = new WeatherDetail
                {
                    temperature = "no values",
                    time=DateTime.MinValue,

                };
            }

            Days[] das = splitDays(vaderYR);

            string va = "Dd";

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

        private Days[] splitDays(WeatherDetail[] input)
        {
            Days[] days = new Days[3];
            
            WeatherDetail[] tempDetail = new WeatherDetail[input.Length];
            int j = 0;
            int f = 0;
            DateTime temp = input[0].time.Date;


            for (int i=0; i < input.Length; i++)
            {
                

                if (input[i].time.Date!= temp)
                {
                  
                    days[j] = new Days
                    {
                        day = input[i-1].time.Date.DayOfWeek.ToString(),
                        detail = tempDetail,
                        
                    };

                    j = j + 1;
                    f = 0;

                    tempDetail = new WeatherDetail[input.Length];
                    tempDetail[f] = new WeatherDetail
                    {
                        hour = "test",
                        temperature = "test",
                        time = input[i].time,
                    };
                    f = f + 1;
                    temp =input[i].time.Date;
                   
                }
                else
                {
                   
                    tempDetail[f] = new WeatherDetail
                    {
                        hour = "test",
                        temperature = "test",
                        time = input[i].time,
                    };
              
                    f = f + 1;
                }     

            }

            DateTime edff = temp;
            string dfenni = "";
           

            return days;

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

