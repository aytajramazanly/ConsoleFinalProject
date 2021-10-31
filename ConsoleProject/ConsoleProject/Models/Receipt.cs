using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.Models
{
    class Receipt
    {
        public User User { get; }
        public DateTime ReceipDate { get; }
        public string DrugName { get; }
        public int DrugCount { get; }
        public double TotalPrice { get; }
        public Guid Guid { get; }

        public Receipt(User user, string drugName, double totalPrice, int drugCount)
        {
            User = user;
            ReceipDate = DateTime.Now;
            DrugName = drugName;
            DrugCount = drugCount;
            TotalPrice = totalPrice;
            Guid = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"{DrugName} | QYT: {DrugCount} | Total Price: {TotalPrice} | Receipt Date: {ReceipDate} | ID=> {Guid}";
        }
    }
}
