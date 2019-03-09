using P1M1.Abstract;
using P1M1.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1M1.Objects
{
    public class Company : BaseObject
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        private static List<Company> list = new List<Company>();

        public Company(Company company)
        {
            GenerateHashCode();

            Id = company.Id;
            Name = company.Name;
            Address = new Address(company.Address);
        }

        public Company(string name, Address address)
        {
            Company company = list.FirstOrDefault(c => c.Name == name && c.Address.Equals(address));

            if (company == null)
            {
                GenerateHashCode();

                Id = null;
                Name = name;
                Address = address;

                list.Add(this);
            }
            else
            {
                HashCode = company.HashCode;

                Id = company.Id;
                Name = company.Name;
                Address = company.Address;
            }
        }

        public static Company Find(string id)
        {
            Company company = DB.Find(id) as Company;

            if (company == null)
                return null;
            else
                return new Company(company);
        }

        public override bool Equals(object obj)
        {
            Company company = obj as Company;

            if (company == null)
                return false;
            else
            {
                return
                    Id == company.Id &&
                    Name == company.Name &&
                    Address.Equals(company.Address);
            }
        }
    }
}
