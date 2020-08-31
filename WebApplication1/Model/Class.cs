
using System;
namespace WebApplication1.Model
{ 
    public class Rootobject
    {
        public DateTime approvedTime { get; set; }
        public Timesery[] timeSeries { get; set; }
    }

    public class Timesery
    {
        public DateTime validTime { get; set; }
        public Parameter[] parameters { get; set; }
    }

    public class Parameter
    {
        public string name { get; set; }
        public float[] values { get; set; }
    }

}
