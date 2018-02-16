namespace Example.Cli
{
    using System;

    using Microsoft.EntityFrameworkCore;

    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new ExampleContext()) {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Seed();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
