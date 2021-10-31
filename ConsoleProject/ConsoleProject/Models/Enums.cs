using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.Models
{
    class Enums
    {
        public enum UserMenu
        {
            [Description("Add Cash")]
            AddCash=1,
            [Description("Buy Drug")]
            BuyDrug,
            [Description("Check your balance")]
            CheckBalance,
            [Description("Show Purchase History")]
            PurchaseHistory,
            [Description("Log out")]
            LogOut
        }

        public enum PharmacyStartMenu
        {
            [Description("Brances")]
            Brances=1,
            [Description("Add Branch")]
            AddBranch,
            [Description("Log Out")]
            LogOut
        }
        public enum PharmacyMenu
        {
            [Description("Add Drug")]
            AddDrug=1,
            [Description("Info Drug")]
            InfoDrug,
            [Description("Show Drug Items")]
            ShowDrugItems,
            [Description("Update Drug count")]
            UpdateDrug,
            [Description("Show Total Income")]
            TotalInCome,
            [Description("Go Main Menu")]
            GoStartMenu
        }
        public enum BuyDrugException
        {
           
            [Description("Not enough drugs for sale!")]
            InsufficientDrugCount,
            [Description("Insufficient customer balance!")]
            InsufficientCustomerBalance,
            [Description("Sale successfully completed")]
            SuccessfulPurchase
        }

        public static string StringValueOfEnum(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
