using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject
{
    class DrugType
    {
        public int Id { get; }

        private static int _counter = 0;
        public string TypeName { get; }
        public DrugType(string typeName)
        {
            TypeName = typeName;
            _counter++;
            Id = _counter;
        }
        public override string ToString()
        {
            return $"{Id} - {TypeName}";
        }
    }
}
