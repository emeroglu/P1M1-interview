using P1M1.Abstract;
using P1M1.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1M1.Objects
{
    public class Person : BaseObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

        private static List<Person> list = new List<Person>();

        public Person(Person person)
        {
            GenerateHashCode();

            Id = person.Id;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Address = new Address(person.Address);
        }

        public Person(string firstName, string lastName, Address address)
        {
            Person person = list.FirstOrDefault(p => p.FirstName == firstName && p.LastName == lastName && p.Address.Equals(address));

            if (person == null)
            {
                GenerateHashCode();

                Id = null;
                FirstName = firstName;
                LastName = lastName;
                Address = address;

                list.Add(this);
            }
            else
            {
                HashCode = person.HashCode;

                Id = person.Id;
                FirstName = person.FirstName;
                LastName = person.LastName;
                Address = person.Address;
            }
        }

        public static Person Find(string id)
        {
            Person personFound = DB.Find(id) as Person;

            if (personFound == null)
                return null;
            else
                return new Person(personFound);
        }

        public override bool Equals(object obj)
        {
            Person person = obj as Person;

            if (person == null)
                return false;
            else
            {
                return
                    Id == person.Id &&
                    FirstName == person.FirstName &&
                    LastName == person.LastName &&
                    Address.Equals(person.Address);
            }
        }
    }
}
