namespace Example
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Model;

    public static class ExampleSeed
    {
        public static readonly Country[] Countries = {
            new Country {Name = "Fooland", IsoCode = "FO"},
            new Country {Name = "Barland", IsoCode = "BA"},
            new Country {Name = "Bazzland", IsoCode = "BZ"}
        };

        public static readonly City[] Cities = {
            new City {Name = "Footown", ZipCode = 12345, Country = Countries[0]},
            new City {Name = "Barcity", ZipCode = 09876, Country = Countries[0]},
            new City {Name = "Bazzville", ZipCode = 54321, Country = Countries[1]},
            new City {Name = "Quxpolis", ZipCode = 67890, Country = Countries[1]}
        };

        public static readonly Person[] Persons = {
            new Person {FirstName = "Foo", LastName = "Barnsted", Address = new Address {Street = "12 Foo Ln", City = Cities[0]}},
            new Person {FirstName = "Bazz", LastName = "Quxdecker", Address = new Address {Street = "463 Qux Blvd", City = Cities[2]}}
        };

        public static void Seed(this ExampleContext db)
        {
            db.Countries.SeedWith(Countries);
            db.Cities.SeedWith(Cities);
            db.Persons.SeedWith(Persons);
            db.SaveChanges();
        }

        public static void SeedWith<T>(this DbSet<T> dbSet, IEnumerable<T> entities)
            where T : class
        {
            if (dbSet.Any())
                return;

            foreach (var entitity in entities) {
                dbSet.Add(entitity);
            }
        }
    }
}
