
using System;

namespace InsideAirbnbCasus.Models
{
    public class TimeLine
    {
        public TimeLine(string neighbourhood, DateTime date)
        {
            this.neighbourhood = neighbourhood;
            this.date = date;
        }

        public string neighbourhood { get; set; }
        public DateTime date { get; set; } 
    }
}
