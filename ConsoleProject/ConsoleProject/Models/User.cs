using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleProject.Models.Enums;

namespace ConsoleProject.Models
{
    class User
    {
        public string Name  { get; }
        public string SurName { get; }
        public int Age { get; set; }
        private string _userName;

        public string UserName
        {
            get
            {
                if (_userName == null)
                {
                    return null;
                }
                return _userName;
            }
            set
            {
                if (value.Length >= 8)
                {
                    _userName = value;
                    return;
                }
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                if (_password == null)
                {
                    return null;
                }
                return _password;
            }

            set
            {
                if (value.Length >= 8 && CheckPassword(value))
                {
                    _password = value;
                    return;
                }
            }
        }
        private double Balance { get; set; }
        public int Id { get; }

        private static int _counter = 0;
        private List<Receipt> checks;


        public User(string name,string surname,int age)
        {
            Name = name;
            SurName = surname;
            Age = age;
            _counter++;
            Id = _counter;
            checks = new List<Receipt>();
        }
        public override string ToString()
        {
            return $"{Name} {SurName}";
        }
        public static bool CheckPassword(string value)
        {
            bool num = false;
            bool lower = false;
            bool upper = false;
            foreach (char item in value)
            {
                if (char.IsDigit(item) && num == false)
                {
                    num = true;
                }
                else if (char.IsLower(item) && lower == false)
                {
                    lower = true;

                }
                else if (char.IsUpper(item) && upper == false)
                {
                    upper = true;

                }
                if (num && lower && upper)
                {

                    return true;
                }
            }
            return false;
        }
        public void IncreaseBalance(double money)
        {
            Balance += money;
        }
        public double GetBalance()
        {
            return Balance;
        }
         public BuyDrugException BuyDrug(Drug drug, int count, double money,Pharmacy pharmacy)
         {
            if (drug.Count<count)
            {
                return BuyDrugException.InsufficientDrugCount;
            }
            else if (money<drug.Price*count)
            {
                return BuyDrugException.InsufficientCustomerBalance;
            }
            drug.Count -= count;
            pharmacy.TotalInCome+= drug.Price * count;
            Balance -=drug.Price * count;
            return BuyDrugException.SuccessfulPurchase;
         }
        public void AddCheck(Receipt receipt)
        {
            checks.Add(receipt);
        }
        public IEnumerable<Receipt> GetPurchaseHistory()
        {
            if (checks.Count == 0)
            {
                yield break;
            }
            foreach (Receipt receipt in checks)
            {
                yield return receipt;
            }
        }
        public int ReceiptsCount()
        {
            return checks.Count;
        }
    }
}
