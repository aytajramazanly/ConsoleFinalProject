using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.Models
{
    class Drug
    {
        public string Name { get; }
        public DrugType Type { get; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int Id { get; }

        private static int _counter = 0;

        public Drug(int count)
        {
            Count = count;
        }
        public Drug(double price) 
        {
            Price = price;
        }
        public Drug(DrugType type, string name, double price,int count) : this(count)
        {
            Type = type;
            Name = name;
            Price = price;
            _counter++;
            Id = _counter;
        }
        public override string ToString()
        {
            return $"{Name} | Price: {Price} AZN | Count: {Count} | ID: {Id}";
        }
    }
}
