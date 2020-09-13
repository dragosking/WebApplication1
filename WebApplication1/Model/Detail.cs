using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class Detail
    {
        public WeatherDetail SMHI { get; set; }

        public WeatherDetail YR{ get; set; }

        public string HOUR { get; set; }
    }
}
