using ConsoleProject.Models;
using ConsoleProject.Utils;
using System;
using System.Collections.Generic;
using static ConsoleProject.Models.Enums;

namespace ConsoleProject
{
    class Program
    {
      
        static void Main(string[] args)
        {
            List<Pharmacy> pharmacies = new List<Pharmacy>();
            List<User> users = new List<User>();

            while (true)
            {
                Console.Clear();
            StartMenu:
                Helper.Print(ConsoleColor.Cyan, "1- Login as Admin");
                Helper.Print(ConsoleColor.Cyan, "2-Login as User");
                Helper.Print(ConsoleColor.Cyan, "3-Out");
                string loginAs = Console.ReadLine();
                if (loginAs == "1")
                {
                    Console.Clear();
                    Helper.Print(ConsoleColor.Green, "          ADMIN PANEL");
                    Helper.Print(ConsoleColor.Yellow, "Input Username: ");
                    string adiminUserName = Console.ReadLine();
                    Helper.Print(ConsoleColor.Yellow, "Input Password: ");
                    string adminPassword = Console.ReadLine();
                    if (Helper.LoginAsadmin(adiminUserName, adminPassword))
                    {
                        Console.Clear();
                        while (true)
                        {
                            Console.Clear();
                            foreach (var item in Helper.GetStartMenu())
                            {
                                Console.WriteLine(item);
                            }
                            if (int.TryParse(Console.ReadLine(), out int menu) && menu >= 1 && menu <= 3)
                            {
                                if (menu == 3)
                                {
                                    break;
                                }
                                PharmacyStartMenu startMenu = (PharmacyStartMenu)menu;
                                switch (startMenu)
                                {
                                    case PharmacyStartMenu.Brances:
                                        Console.Clear();
                                        if (pharmacies.Count == 0)
                                        {
                                            Helper.DactyloWriting(ConsoleColor.Cyan, "No Pharmacy Branches found. Add One ");
                                            goto case PharmacyStartMenu.AddBranch;
                                        }

                                        var existPharmacy = Helper.SelectPharmacy(pharmacies);
                                    PharmacyMenu:
                                        Console.Clear();
                                        foreach (var item in Helper.GetPharmacyMenu())
                                        {
                                            Console.WriteLine(item);
                                        }

                                        if (int.TryParse(Console.ReadLine(), out menu) && menu >= 1 && menu <= 7)
                                        {
                                            PharmacyMenu mainMenu = (PharmacyMenu)menu;
                                            switch (mainMenu)
                                            {
                                                case PharmacyMenu.AddDrug:
                                                    Console.Clear();

                                                    string drugTypeName;
                                                    if (Helper.PressKey("Input new drug Type press space | choose exist drug Type press any")==ConsoleKey.Spacebar)
                                                    {
                                                        Helper.Print(ConsoleColor.Yellow, "Input drug Type: ");
                                                        drugTypeName = Console.ReadLine();
                                                        if (existPharmacy.IsDrugTypeExist(drugTypeName))
                                                        {
                                                            Helper.DactyloWriting(ConsoleColor.Cyan, "This drug Type already Exist");
                                                            goto case PharmacyMenu.AddDrug;
                                                        }
                                                        DrugType type = new DrugType(drugTypeName);
                                                        existPharmacy.AddDrugType(type);
                                                        goto inputDrugName;
                                                    }
                                                    else
                                                    {
                                                        inputDrugType:
                                                        Console.Clear();
                                                        Helper.Print(ConsoleColor.Yellow, "Choose Drug Type Name");
                                                        if (existPharmacy.DrugTypesCount() != 0)
                                                        {
                                                            foreach (var item in existPharmacy.ShowDrugTypes())
                                                            {
                                                                Console.WriteLine(item);
                                                            }
                                                            drugTypeName = Console.ReadLine();
                                                        }
                                                        else
                                                        {
                                                            Helper.DactyloWriting(ConsoleColor.Red, "There is no exist Drug type");
                                                            goto case PharmacyMenu.AddDrug;
                                                        }
                                                        if (existPharmacy.IsDrugTypeExist(drugTypeName)==false)
                                                        {
                                                            Helper.DactyloWriting(ConsoleColor.Red, "Please input drug type NAME correctly");
                                                            goto inputDrugType;
                                                        }
                                                    }
                                                inputDrugName:
                                                    Helper.Print(ConsoleColor.Yellow, "Input drug name: ");
                                                    string name = Console.ReadLine();

                                                    if (existPharmacy.IsDrugExist(name))
                                                    {
                                                        Helper.Print(ConsoleColor.Red, "Drug with this name already exist!");
                                                        Helper.Print(ConsoleColor.DarkYellow, "Update exist drug, press space | for menu press any key");
                                                        ConsoleKeyInfo keyInfo;
                                                        keyInfo = Console.ReadKey();
                                                        if (keyInfo.Key == ConsoleKey.Escape)
                                                        {
                                                            goto case (PharmacyMenu)5;
                                                        }
                                                        goto PharmacyMenu;
                                                    }

                                                    Helper.Print(ConsoleColor.Yellow, "Input drug price: ");
                                                inputPrice:
                                                    if (!double.TryParse(Console.ReadLine(), out double price))
                                                    {
                                                        Helper.Print(ConsoleColor.Red, "Input Price as a Digit: ");
                                                        goto inputPrice;
                                                    }

                                                    Helper.Print(ConsoleColor.Yellow, "Input drug count: ");
                                                inputCount1:
                                                    if (!int.TryParse(Console.ReadLine(), out int count))
                                                    {
                                                        Helper.Print(ConsoleColor.Red, "Input Count as a Digit: ");
                                                        goto inputCount1;
                                                    }
                                                    Drug drug = new Drug(existPharmacy.GetDrugType(x => x.TypeName.ToLower() == drugTypeName.Trim().ToLower()), name, price, count);
                                                    existPharmacy.AddDrugs(drug);
                                                    Helper.DactyloWriting(ConsoleColor.Green, "Drug add Successfull!");
                                                    goto PharmacyMenu;

                                                case PharmacyMenu.InfoDrug:
                                                    if (existPharmacy.DrugsCount() == 0)
                                                    {
                                                        Helper.Print(ConsoleColor.Red, "Here is empty!");
                                                        if (Helper.PressKey("For Add Drug press space | for menu press any") == ConsoleKey.Spacebar)
                                                        {
                                                            Console.Clear();
                                                            goto case PharmacyMenu.AddDrug;
                                                        }
                                                        goto PharmacyMenu;
                                                    }

                                                    Helper.Print(ConsoleColor.Yellow, "Enter drug name for information: ");
                                                    name = Console.ReadLine();
                                                    List<Drug> drugsSearchResult = new List<Drug>(); 
                                                    foreach (var item in existPharmacy.InfoDrug(name))
                                                    {
                                                        drugsSearchResult.Add(item);
                                                    }
                                                    
                                                    if (drugsSearchResult.Count==0)
                                                    {
                                                        Helper.DactyloWriting(ConsoleColor.Red, "There is no drug with this or similar name");
                                                        goto PharmacyMenu;
                                                    }
                                                    foreach (var item in drugsSearchResult)
                                                    {
                                                        Console.WriteLine(item);
                                                    }
                                                    if (Helper.PressKey("press space for menu") == ConsoleKey.Spacebar)
                                                    {
                                                        goto PharmacyMenu;
                                                    }
                                                    System.Threading.Thread.Sleep(6000);
                                                    goto PharmacyMenu;

                                                case PharmacyMenu.ShowDrugItems:
                                                    if (existPharmacy.DrugsCount() == 0)
                                                    {
                                                        Helper.Print(ConsoleColor.Red, "Here is empty!");
                                                        if (Helper.PressKey("For Add Drug press space | for menu press any") == ConsoleKey.Spacebar)
                                                        {
                                                            Console.Clear();
                                                            goto case PharmacyMenu.AddDrug;
                                                        }
                                                        goto PharmacyMenu;
                                                    }
                                                    foreach (var item in existPharmacy.ShowDrugItems())
                                                    {
                                                        Console.WriteLine(item);
                                                    }
                                                    if (Helper.PressKey("press space for menu") == ConsoleKey.Spacebar)
                                                    {
                                                        goto PharmacyMenu;
                                                    }
                                                    System.Threading.Thread.Sleep(6000);
                                                    goto PharmacyMenu;
                                                case PharmacyMenu.UpdateDrug:
                                                    if (existPharmacy.DrugsCount() == 0)
                                                    {
                                                        Helper.Print(ConsoleColor.Red, "Here is empty!");
                                                        if (Helper.PressKey("For Add Drug press space | for menu press any") == ConsoleKey.Spacebar)
                                                        {
                                                            Console.Clear();
                                                            goto case PharmacyMenu.AddDrug;
                                                        }
                                                        goto PharmacyMenu;
                                                    }

                                                    Helper.Print(ConsoleColor.Cyan, "Input Name of Drug to update: ");
                                                    name = Console.ReadLine();
                                                    if (!existPharmacy.IsDrugExist(name))
                                                    {
                                                        Helper.DactyloWriting(ConsoleColor.Red, "Drug with this name doesn't exist!");
                                                        goto PharmacyMenu;
                                                    }
                                                    Update:
                                                    Helper.Print(ConsoleColor.Cyan, "1-Update Count | 2-Update Price");
                                                    string answer = Console.ReadLine();
                                                    if (answer == "1")
                                                    {
                                                        Helper.Print(ConsoleColor.Yellow, "How many of this medicine have come? => ");
                                                    UpdateCount:
                                                        if (!int.TryParse(Console.ReadLine(), out count))
                                                        {
                                                            Helper.Print(ConsoleColor.Red, "Please Input as a Digit: ");
                                                            goto UpdateCount;
                                                        }
                                                        existPharmacy.UpdateDrugCount(name, new Drug(count));
                                                    }
                                                    else if (answer == "2")
                                                    {
                                                        Helper.Print(ConsoleColor.Yellow, "Input Drug new Price: ");
                                                    UpdateCount:
                                                        if (!double.TryParse(Console.ReadLine(), out price))
                                                        {
                                                            Helper.Print(ConsoleColor.Red, "Please Input as a Digit: ");
                                                            goto UpdateCount;
                                                        }
                                                        existPharmacy.UpdateDrugCount(name, new Drug(price));
                                                    }
                                                    else
                                                    {
                                                        Console.Clear();
                                                        Helper.Print(ConsoleColor.Red, "Input from given numbers");
                                                        goto Update;
                                                    }
                                                    Helper.DactyloWriting(ConsoleColor.Green, "Drug info updated successfully");
                                                    goto PharmacyMenu;
                                                case PharmacyMenu.TotalInCome:
                                                    Helper.DactyloWriting(ConsoleColor.Blue, $" Total Income: {existPharmacy.TotalInCome}");
                                                    if (Helper.PressKey("press space for menu") == ConsoleKey.Spacebar)
                                                    {
                                                        goto PharmacyMenu;
                                                    }
                                                    System.Threading.Thread.Sleep(6000);
                                                    goto PharmacyMenu;
                                                case PharmacyMenu.ShowDrugTypes:
                                                    Console.Clear();
                                                    if (existPharmacy.DrugTypesCount() == 0)
                                                    {
                                                        Helper.Print(ConsoleColor.Red, "Here is empty!");
                                                        System.Threading.Thread.Sleep(2000);
                                                        goto PharmacyMenu;
                                                    }
                                                    foreach (var item in existPharmacy.ShowDrugTypes())
                                                    {
                                                        Console.WriteLine(item);
                                                    }
                                                    if (Helper.PressKey("press space for menu") == ConsoleKey.Spacebar)
                                                    {
                                                        goto PharmacyMenu;
                                                    }
                                                    System.Threading.Thread.Sleep(6000);
                                                    goto PharmacyMenu;
                                                    
                                                case PharmacyMenu.GoStartMenu:
                                                    Console.Clear();
                                                    break;
                                                default:
                                                    goto PharmacyMenu;
                                            }
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Helper.Print(ConsoleColor.Red, "Please, enter from the given numbers");
                                            Console.WriteLine();
                                            goto PharmacyMenu;
                                        }
                                        break;
                                    case PharmacyStartMenu.AddBranch:
                                        Console.Clear();
                                        pharmacies.Add(Helper.CreatPharmacy());
                                        Helper.DactyloWriting(ConsoleColor.Green, "Branch add is Succesfull!");
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                Helper.Print(ConsoleColor.Red, "Please input from given numbers!");
                                System.Threading.Thread.Sleep(1500);
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Helper.DactyloWriting(ConsoleColor.Red, "Incorrect Username or Password!");
                            goto StartMenu;
                    }
                }
                else if (loginAs=="2")
                {
                    Console.Clear();
                    Helper.Print(ConsoleColor.DarkGreen, "Don't have an Account?");
                    if (Helper.PressKey("For sign in press Space | for login as current member press any key") ==ConsoleKey.Spacebar)
                    {
                        Console.Clear();
                        Helper.Print(ConsoleColor.Yellow, "    Sign in");
                        Helper.Print(ConsoleColor.DarkGreen, "Input Name");
                        string name = Console.ReadLine();
                        Helper.Print(ConsoleColor.DarkGreen, "Input Surname");
                        string surname = Console.ReadLine();
                        Helper.Print(ConsoleColor.DarkGreen, "Input Age");
                        InputAge:
                        if (!int.TryParse(Console.ReadLine(),out int age))
                        {
                            Helper.DactyloWriting(ConsoleColor.Red, "Please input age as a digit");
                            goto InputAge;
                        }
                        User user = new User(Helper.InitialUpper(ref name),Helper.InitialUpper(ref surname),age);
                        Helper.Print(ConsoleColor.DarkGreen, "Input Username");
                        InputUsername:
                        string username = Console.ReadLine();
                        if (users.Find(x => x.UserName.ToLower() == username.Trim().ToLower()) != null)
                        {
                            Helper.Print(ConsoleColor.Red, "This username already exists! Try a different username");
                            goto InputUsername;
                        }
                        user.UserName = username;
                        if (user.UserName == null)
                        {
                            Helper.DactyloWriting(ConsoleColor.Red,"Username must be at least 8 symbol: ");
                            goto InputUsername;
                        }
                        Helper.Print(ConsoleColor.DarkGreen, "Input Password");
                        InputPassword:
                        string password = Console.ReadLine();
                        user.Password = password;
                        if (user.Password == null)
                        {
                            Helper.DactyloWriting(ConsoleColor.Red,"Password must be at least 8 symbol and must contain at least one lower | upper letter and number ");
                            goto InputPassword;
                        }
                        users.Add(user);
                        Helper.DactyloWriting(ConsoleColor.Green, "You have successfully signed up");
                    }
                    else
                    {
                        Console.Clear();
                        if (users.Count==0)
                        {
                            Console.Clear();
                            Helper.Print(ConsoleColor.Red, "No current member!");
                            goto StartMenu;
                        }
                    existUserLogin:
                        Helper.Print(ConsoleColor.Yellow, "    LOGIN");
                        Helper.Print(ConsoleColor.DarkGreen, "Input Username");
                        string username = Console.ReadLine();
                        Helper.Print(ConsoleColor.DarkGreen, "input password");
                        string password = Console.ReadLine();

                        if (users.Find(x=>x.UserName.ToLower()==username.Trim().ToLower())==null || users.Find(x => x.UserName.ToLower() == username.Trim().ToLower()).Password!=password)
                        {
                            Helper.DactyloWriting(ConsoleColor.Red, "Incorrect Username or Password! Please try again");
                            goto existUserLogin;
                        }
                        User existUser = users.Find(x => x.UserName.ToLower() == username.Trim().ToLower());
                    UserMenu:
                        Console.Clear();
                        Helper.Print(ConsoleColor.Magenta, $"{existUser}");
                        foreach (var item in Helper.GetUserMenu())
                        {
                            Console.WriteLine(item);
                        }
                        if (int.TryParse(Console.ReadLine(),out int userMenu) && userMenu>=1 &&userMenu<=7)
                        {
                            if (userMenu==7)
                            {
                                break;
                            }
                            UserMenu menu = (UserMenu)userMenu;
                            switch (menu)
                            {
                                case UserMenu.AddCash:
                                    Helper.Print(ConsoleColor.Yellow, "Enter the amount of money to be added");
                                    InputAddMoney:
                                    if (!double.TryParse(Console.ReadLine(),out double money))
                                    {
                                        Helper.DactyloWriting(ConsoleColor.Red, "Please input amount of money as a digit!");
                                        goto InputAddMoney;
                                    }
                                    existUser.IncreaseBalance( money);
                                    Helper.DactyloWriting(ConsoleColor.Green, "Adding Cash is successfull");
                                    goto UserMenu;
                                case UserMenu.BuyDrug:
                                    if (pharmacies.Count==0)
                                    {
                                        Helper.DactyloWriting(ConsoleColor.Red, "There is no exist Pharmacy here!");
                                        System.Threading.Thread.Sleep(2000);
                                        goto UserMenu;
                                    }
                                    var existPharmacy = Helper.SelectPharmacy(pharmacies);
                                    Helper.Print(ConsoleColor.Yellow, "Input Drug name for Buy");
                                    string drugName = Console.ReadLine();
                                    Drug drugToBuy = existPharmacy.FindDrug(x => x.Name.ToLower() == drugName.Trim().ToLower());
                                    if (drugToBuy==null)
                                    {
                                        Helper.Print(ConsoleColor.Red, "There is no any drug with this name!");
                                        goto UserMenu;
                                    }
                                    Helper.Print(ConsoleColor.Yellow, "Input Drug count");
                                    if (!int.TryParse(Console.ReadLine(),out int count))
                                    {
                                        Helper.Print(ConsoleColor.Red, "Please input as a digit");
                                    }
                                    BuyDrugException buyDrugException= existUser.BuyDrug(drugToBuy, count, existUser.GetBalance(), existPharmacy);
                                    if (buyDrugException==BuyDrugException.InsufficientCustomerBalance)
                                    {
                                        Helper.DactyloWriting(ConsoleColor.Red, "Your balance not enough for buy");
                                        goto UserMenu;
                                    }
                                    else if (buyDrugException == BuyDrugException.InsufficientDrugCount)
                                    {
                                        Helper.DactyloWriting(ConsoleColor.Cyan, $"Just have {drugToBuy.Count} of {drugToBuy.Name} drug. Do you want to buy {drugToBuy.Count} {drugToBuy.Name}? ");
                                        Helper.Print(ConsoleColor.DarkYellow, "For buy, press space | for menu press any key");
                                        ConsoleKeyInfo keyInfo;
                                        keyInfo = Console.ReadKey();
                                        if (keyInfo.Key == ConsoleKey.Spacebar)
                                        {
                                            int tempDrugCount = drugToBuy.Count;
                                            existUser.BuyDrug(drugToBuy, drugToBuy.Count, existUser.GetBalance(), existPharmacy);
                                            Helper.SuccessfullPurchase(existUser, drugToBuy, existPharmacy, tempDrugCount);
                                        }
                                    }
                                    else
                                    {
                                        Helper.SuccessfullPurchase(existUser, drugToBuy, existPharmacy, count);
                                    }
                                    goto UserMenu;
                                case UserMenu.CheckBalance:
                                    Helper.DactyloWriting(ConsoleColor.Green, $"Your Balance: {existUser.GetBalance()} AZN");
                                    if (Helper.PressKey("press space for menu") == ConsoleKey.Spacebar)
                                    {
                                        goto UserMenu;
                                    }
                                    System.Threading.Thread.Sleep(10000);
                                    goto UserMenu;
                                case UserMenu.PurchaseHistory:
                                    if (existUser.ReceiptsCount()==0)
                                    {
                                        Helper.DactyloWriting(ConsoleColor.Red, "Your Purchase History is Empty!");
                                    }
                                    foreach (Receipt receipt in existUser.GetPurchaseHistory())
                                    {
                                        Helper.Print(ConsoleColor.DarkMagenta, $"{receipt}");
                                    }
                                    if (Helper.PressKey("press space for menu")==ConsoleKey.Spacebar)
                                    {
                                        goto UserMenu;
                                    }
                                    System.Threading.Thread.Sleep(10000);
                                    goto UserMenu;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Helper.Print(ConsoleColor.Red, "Please input from given numbers");
                            goto UserMenu;
                        }
                    }
                   
                }
                else if (loginAs == "3")
                {
                    break;
                }
                else
                {
                    Helper.DactyloWriting(ConsoleColor.Red, "Please input from given numbers");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
