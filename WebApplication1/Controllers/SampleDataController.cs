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
                vaderSHMI = new WeatherDetail[vaderYR.Length];
                for(int i = 0; i < vaderYR.Length; i++)
                {
                    vaderSHMI[i] = new WeatherDetail
                    {
                        temperature = "",
                        time = DateTime.MinValue,
                    };
                }
            }
            
            

            vaderYR = vaderYR.Skip(1).ToArray();
            Days[] das = splitDays(vaderYR,vaderSHMI);

            string[] test = new string[5];

     
            Weather vader = new Weather
            {
                place = loc.place+", "+ loc.country,
                coord = new Coordinates
                {
                    lat = loc.lat,
                    lon = loc.lon
                }
                ,
                detailSMHI = das,
                detailYR = vaderYR,
                test = test
            };

            return vader;
        }

        private Days[] splitDays(WeatherDetail[] input, WeatherDetail[] input2)
        {
            Days[] days = new Days[3];           
            Test[] tempDetail = new Test[24];
            int j = 0;
            int f = 0;
            DateTime temp = input[0].time.Date;


            for (int i=0; i < input.Length; i++)
            {
                if (input[i].time.Date!= temp || i==input.Length-1)
                {
                    tempDetail = tempDetail.Where(c => c != null).ToArray();
                    days[j] = new Days
                    {
                        day = input[i-1].time.Date.DayOfWeek.ToString()+"  " + input[i - 1].time.Date.Day +"/"+ input[i - 1].time.Date.Month,
                        detail = tempDetail,
                        
                    };

                    if(i != input.Length-1)
                    {
                        j = j + 1;
                    }
                    
                    f = 0;
                    tempDetail = new Test[24];             
                    temp =input[i].time.Date;
                   
                }

            

                tempDetail[f] = new Test
                {
                    hour = input[i].hour,
                    temperatureYR = input[i].temperature,
                    temperatureSMHI = input2[i].temperature,
                };

                f = f + 1;
            }

            return days;

        }

        private string changeDec(string input)
        {
            Decimal tempLon = Decimal.Parse(input, CultureInfo.InvariantCulture);

            return tempLon.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture);
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

