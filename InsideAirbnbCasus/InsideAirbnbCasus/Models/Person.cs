using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsideAirbnbCasus.Models
{
    public class Person
    {
        public Person()
        {
        }

        public Person(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

        public string name { get; set; }
        public string type { get; set; }
    }
}
