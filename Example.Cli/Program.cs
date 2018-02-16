namespace Example.Cli
{
    using System;

    using Microsoft.EntityFrameworkCore;

    public static class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new ExampleContext()) {
                db.Database.Migrate();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
