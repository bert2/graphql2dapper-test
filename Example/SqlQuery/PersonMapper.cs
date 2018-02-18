namespace Example.SqlQuery
{
    using System;
    using System.Collections.Generic;

    using Dapper.GraphQL;

    using Model;

    public class PersonMapper : IEntityMapper<Person>
    {
        public Func<Person, Person> ResolveEntity { get; set; }

        public Person Map(IEnumerable<object> objs)
        {
            Person person = null;

            foreach (var obj in objs) {
                switch (obj) {
                    case Person p:
                        person = p;
                        break;
                    case Address address:
                        person.Address = address;
                        break;
                    case City city:
                        person.Address.City = city;
                        break;
                    case Country country:
                        person.Address.City.Country = country;
                        break;
                }
            }

            return person;
        }
    }
}
