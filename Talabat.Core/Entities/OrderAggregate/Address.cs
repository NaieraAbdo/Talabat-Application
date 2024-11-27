using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class Address
    {
        public Address()
        {
            
        }
        public Address(string fName, string lname, string city, string country, string street)
        {
            FName = fName;
            Lname = lname;
            City = city;
            Country = country;
            Street = street;
        }

        public string FName { get; set; }
        public string Lname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }


    }
}
