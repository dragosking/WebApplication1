using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.API;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        private int count;

        public SampleDataController()
        {
            count = 0;
        }


        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }


        [HttpPost("test")]
        public IEnumerable<PL> test([FromBody]PL pl)
        {
            if (pl == null)
            {
                count++;
                String d = count.ToString();
                return null;
            }

            var rng = new Random();
            string[] temp = searchLocation(pl.a);

            if (temp == null)
            {
                return null;
            }
            return Enumerable.Range(0, temp.Length).Select(index => new PL
            {
                a = temp[index]
            }) ;

           //return temp;
        }

        private string[] searchLocation(string input) { 
            string[] outputs=new string[20];
            int j = 0;

            ParseJsonPlace parse = new ParseJsonPlace();
            var loc=parse.ReadUrlAsync("https://www.smhi.se/wpt-a/backend_solr/autocomplete/search/", input);

            if (input == "" || input == null)
            {
                return null;
            }

            foreach (var item in loc)
            {
                outputs[j] = item.place;
                j++;    
            }

            outputs = outputs.Where(c => c != null).ToArray();
            return outputs;
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }

        public class PL
        {
            public string a { get; set; }
        }

    }
}

