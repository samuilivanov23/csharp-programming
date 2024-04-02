using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upr4
{
    public class Contact
    {
        private static readonly Random random = new Random();

        private string _contactName;
        public string ContactName
        {
            get { return _contactName; }
            set { _contactName = value ?? GenerateRandomName(); }
        }

        public string Email { get; }

        public Contact(string name, string email)
        {
            ContactName = name;
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        private string GenerateRandomName()
        {
            const string digits = "0123456789";
            string randomName = "user";

            for (int i = 0; i < 5; i++)
            {
                randomName += digits[random.Next(digits.Length)];
            }

            return randomName;
        }

        public override string ToString() => $"{ContactName} <{Email}>";
    }
}
