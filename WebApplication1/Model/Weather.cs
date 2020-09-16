using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class Weather
    {
        public Coordinates coord { get; set; }
        public string place { get; set; }
        public WeatherDetail[] detailSMHI { get; set; }
        public WeatherDetail[] detailYR { get; set; }

        public Days[] WeatherByDay { get; set; }

        public string[] test { get; set; }


    }
}
