using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class Location
    {

        public string geonameid { get; set; }
        public string place { get; set; }
        public string country { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
    }
}
