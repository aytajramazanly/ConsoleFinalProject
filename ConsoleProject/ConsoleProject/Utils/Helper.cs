using ConsoleProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleProject.Models.Enums;

namespace ConsoleProject.Utils
{
    class Helper
    {
        public static void Print(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void DactyloWriting(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            foreach (char item in text)
            {
                Console.Write(item);
                System.Threading.Thread.Sleep(35);
            }
            Console.WriteLine();
            System.Threading.Thread.Sleep(1000);
            Console.ResetColor();
        }

        public static ConsoleKey PressKey(string text)
        {
            Helper.Print(ConsoleColor.DarkYellow, text);
            ConsoleKeyInfo keyInfo;
            keyInfo = Console.ReadKey();
            return keyInfo.Key;
        }

        public static Pharmacy SelectPharmacy(List<Pharmacy> pharmacies)
        {
            Helper.Print(ConsoleColor.Cyan, "Select Branch by ID");
            Console.WriteLine();
            foreach (var item in pharmacies)
            {
                Console.WriteLine(item);
            }

        inputPharmacyId:
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Helper.Print(ConsoleColor.Red, "Please input Id ad a Digit: ");
                goto inputPharmacyId;
            }
            var existPharmacy = pharmacies.Find(x => x.Id ==id);
            if (existPharmacy == null)
            {
                Helper.Print(ConsoleColor.Red, "Select from exist Branches: ");
                goto inputPharmacyId;
            }
            return existPharmacy;
        }
        public static Pharmacy CreatPharmacy()
        {
            Helper.Print(ConsoleColor.Yellow, "Input Pharmacy Branch Name: ");
            string pharmacyName = Console.ReadLine();
            Pharmacy pharmacy = new Pharmacy(pharmacyName);
            return pharmacy;
        }
        public static bool LoginAsadmin(string username, string password)
        {
            string username1 = "admin";
            string password1 = "admin123";
            bool check = username.ToLower().Equals(username1.ToLower()) && password.ToLower().Equals(password1.ToLower());
            return check;
        }
        public static IEnumerable<string> GetStartMenu()
        {
            foreach (var item in Enum.GetValues(typeof(PharmacyStartMenu)))
            {
                yield return $"{(int)item}. {StringValueOfEnum((Enum)item)}";
            }
        }
        public static IEnumerable<string> GetUserMenu()
        {
            foreach (var item in Enum.GetValues(typeof(UserMenu)))
            {
                yield return $"{(int)item}. {StringValueOfEnum((Enum)item)}";
            }
        }
        public static IEnumerable<string> GetPharmacyMenu()
        {
            foreach (var item in Enum.GetValues(typeof(PharmacyMenu)))
            {
                yield return $"{(int)item}. {StringValueOfEnum((Enum)item)}";
            }
        }
        public static void SuccessfullPurchase(User user, Drug drug,Pharmacy pharmacy, int count)
        {
           
            Receipt receipt = new Receipt(user, drug.Name, drug.Price * count, count,drug.Price);
            Helper.DactyloWriting(ConsoleColor.Green, "Purchase is successfull!");
            Helper.DactyloWriting(ConsoleColor.DarkMagenta, $"Your Receipt: {receipt}");
            user.AddCheck(receipt);
        }
        public static string InitialUpper(ref string text)
        {
            text = text.Trim();
            StringBuilder str = new StringBuilder();
            str.Append(Char.ToUpper(text[0]) + text.Substring(1));
            return str.ToString();
        }
    }
}
