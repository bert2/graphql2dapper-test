namespace Example.SqlQuery
{
    using System;

    using Dapper.GraphQL;

    using Model;

    public class PersonMapper : IEntityMapper<Person>
    {
        public Func<Person, Person> ResolveEntity { get; set; }

        public Person Map(EntityMapContext context)
        {
            Person person = null;

            foreach (var obj in context.Items) {
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
