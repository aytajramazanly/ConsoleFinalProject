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
        public string Name  { get; }
        public string SurName { get; }
        public int Age { get; }
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
    }
}
