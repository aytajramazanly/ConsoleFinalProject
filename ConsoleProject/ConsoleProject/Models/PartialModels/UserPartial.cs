using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleProject.Models.Enums;

namespace ConsoleProject.Models
{
    partial class User
    {
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
        public BuyDrugException BuyDrug(Drug drug, int count, double money, Pharmacy pharmacy)
        {
            if (drug.Count < count)
            {
                return BuyDrugException.InsufficientDrugCount;
            }
            else if (money < drug.Price * count)
            {
                return BuyDrugException.InsufficientCustomerBalance;
            }
            drug.Count -= count;
            pharmacy.TotalInCome += drug.Price * count;
            Balance -= drug.Price * count;
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
