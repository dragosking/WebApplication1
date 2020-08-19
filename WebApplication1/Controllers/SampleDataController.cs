using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            string[] temp = search(pl.a);

            if (temp == null)
            {
                return null;
            }
            return Enumerable.Range(1, temp.Length-1).Select(index => new PL
            {
                a = temp[index]
            }) ;

           //return temp;
        }

        private string[] search(string input) { 
            string[] outputs=new string[20];
            int j = 0;

            if (input == "")
            {
                return null;
            }

            foreach( var item in Summaries)
            {
                if (item.Contains(input)){
                 
                    outputs[j] = item;
                    j++;
                }
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

