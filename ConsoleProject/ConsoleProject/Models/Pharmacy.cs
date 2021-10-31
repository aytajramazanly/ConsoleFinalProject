using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.Models
{
   partial class Pharmacy
    {
        public string Name { get; }
        public int Id { get; }

        private static int _counter = 0;

        private List<Drug> drugs;
        public  double TotalInCome { get; set; }
        public Pharmacy(string name)
        {
            Name = name;
            _counter++;
            Id = _counter;
            drugs = new List<Drug>();
        }
    }
}
