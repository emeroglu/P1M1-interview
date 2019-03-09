using P1M1.Abstract;
using P1M1.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1M1.Objects
{
    public class Address : BaseObject
    {
        public string Street { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        private static List<Address> list = new List<Address>();

        public Address(Address address)
        {
            GenerateHashCode();

            Id = address.Id;
            Street = address.Street;
            District = address.District;
            Country = address.Country;
            PostalCode = address.PostalCode;
        }

        public Address(string street, string district, string country, string postalCode)
        {
            Address address = list.FirstOrDefault(a => a.Street == street && a.District == district && a.Country == country && a.PostalCode == postalCode);

            if (address == null)
            {
                GenerateHashCode();

                Id = Guid.NewGuid().ToString();
                Street = street;
                District = district;
                Country = country;
                PostalCode = postalCode;

                list.Add(this);
            }
            else
            {
                HashCode = address.HashCode;

                Id = address.Id;
                Street = address.Street;
                District = address.District;
                Country = address.Country;
                PostalCode = address.PostalCode;
            }
        }

        public static Address Find(string id)
        {
            Address address = DB.Find(id) as Address;

            if (address == null)
                return null;
            else
                return new Address(address);
        }

        public override bool Equals(object obj)
        {
            Address address = obj as Address;

            if (address == null)
                return false;
            else
            {
                return
                    Id == address.Id &&
                    Street == address.Street &&
                    District == address.District &&
                    Country == address.Country &&
                    PostalCode == address.PostalCode;
            }
        }
    }
}
