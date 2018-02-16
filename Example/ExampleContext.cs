namespace Example
{
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class ExampleContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite("Data Source=example.db");
    }
}
