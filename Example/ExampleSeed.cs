namespace Example
{
    using Models;

    public static class ExampleSeed
    {
        private static readonly Country[] Countries = {
            new Country {Name = "Fooland", IsoCode = "FO"},
            new Country {Name = "Barland", IsoCode = "BA"},
            new Country {Name = "Bazzland", IsoCode = "BZ"}
        };

        private static readonly City[] Cities = {
            new City {Name = "Footown", ZipCode = 12345, Country = Countries[0]},
            new City {Name = "Barcity", ZipCode = 09876, Country = Countries[0]},
            new City {Name = "Bazzville", ZipCode = 54321, Country = Countries[1]},
            new City {Name = "Quxpolis", ZipCode = 67890, Country = Countries[1]}
        };

        private static readonly Person[] Persons = {
            new Person {FirstName = "Foo", LastName = "Barnsted", Address = new Address {Street = "12 Foo Ln", City = Cities[0]}},
            new Person {FirstName = "Bazz", LastName = "Quxdecker", Address = new Address {Street = "463 Qux Blvd", City = Cities[2]}}
        };

        public static void Seed(this ExampleContext db)
        {
            foreach (var c in Countries) db.Add(c);
            foreach (var c in Cities) db.Add(c);
            foreach (var p in Persons) db.Add(p);
            db.SaveChanges();
        }
    }
}
