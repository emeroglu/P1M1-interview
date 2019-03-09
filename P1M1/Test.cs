using System;
using NUnit.Framework;
using System.Collections.Generic;
using P1M1.Objects;
using P1M1.Agents;

namespace P1M1
{    
    public class Test
    {
        [TestCase]
        public void DeveloperChallange()
        {
            DB.Path = @"{your-path}\db.txt";            

            var address = new Address("4875 Sun Tail", "Queen Creek", "TX", "38452");
            var person = new Person("Bill", "Smith", address);
            var company = new Company("P1M1 Software And Consulting", address);

            Assert.IsNullOrEmpty(person.Id);
            person.Save();
            Assert.IsNotNullOrEmpty(person.Id);

            Assert.IsNullOrEmpty(company.Id);
            company.Save();
            Assert.IsNotNullOrEmpty(company.Id);

            Person savedPerson = Person.Find(person.Id);
            Assert.IsNotNull(savedPerson);
            Assert.AreSame(person.Address, address);
            Assert.AreEqual(savedPerson.Address, address);
            Assert.AreEqual(person.Id, savedPerson.Id);
            Assert.AreEqual(person.FirstName, savedPerson.FirstName);
            Assert.AreEqual(person.LastName, savedPerson.LastName);
            Assert.AreEqual(person, savedPerson);
            Assert.AreNotSame(person, savedPerson);
            Assert.AreNotSame(person.Address, savedPerson.Address);

            Company savedCompany = Company.Find(company.Id);
            Assert.IsNotNull(savedCompany);
            Assert.AreSame(company.Address, address);
            Assert.AreEqual(savedCompany.Address, address);
            Assert.AreEqual(company.Id, savedCompany.Id);
            Assert.AreEqual(company.Name, savedCompany.Name);
            Assert.AreEqual(company, savedCompany);
            Assert.AreNotSame(company, savedCompany);
            Assert.AreNotSame(company.Address, savedCompany.Address);

            var dictionary = new Dictionary<object, object> { [address] = address, [person] = person, [company] = company };
            Assert.IsTrue(dictionary.ContainsKey(new Address("4875 Sun Tail", "Queen Creek", "TX", "38452")));
            Assert.IsTrue(dictionary.ContainsKey(new Person("Bill", "Smith", address)));
            Assert.IsTrue(dictionary.ContainsKey(new Company("P1M1 Software And Consulting", address)));
            Assert.IsFalse(dictionary.ContainsKey(new Address("54553 Apache Trail", "Queen Creek", "TX", "38452")));
            Assert.IsFalse(dictionary.ContainsKey(new Person("Jim", "Smith", address)));
            Assert.IsFalse(dictionary.ContainsKey(new Company("P1M1", address)));

            var deletedPersonId = person.Id;
            person.Delete();
            Assert.IsNullOrEmpty(person.Id);
            Assert.IsNull(Person.Find(deletedPersonId));

            var deletedCompanyId = company.Id;
            company.Delete();
            Assert.IsNullOrEmpty(company.Id);
            Assert.IsNull(Person.Find(deletedCompanyId));
        }
    }
}
