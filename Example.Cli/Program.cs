namespace Example.Cli
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    public static class Program
    {
        public static void Main(string[] args)
        {
            InitDb();

            try {
                Run(args);
                Environment.Exit(0);
            }
            catch (ArgumentException e) {
                Console.WriteLine($"ERROR: {e.Message}\n");
                Console.WriteLine(GetUsageMessage());
                Environment.Exit(1);
            }
            catch (Exception e) {
                Console.WriteLine(e);
                Environment.Exit(1);
            }
        }

        private static void InitDb()
        {
            using (var db = new ExampleContext()) {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.Seed();
            }
        }

        private static void Run(IReadOnlyList<string> args)
        {
            if (args.Count < 1) throw new ArgumentException("Missing verb.");

            switch (args[0].ToLower()) {
                case "get":
                    Get(args);
                    break;
                default:
                    throw new ArgumentException($"Unknown verb '{args[0]}'.");
            }
        }

        private static void Get(IReadOnlyList<string> args)
        {
            if (args.Count < 2) throw new ArgumentException("Missing table name.");

            PrintTableContents(args[1]);
        }

        private static void PrintTableContents(string table)
        {
            using (var db = new ExampleContext()) {
                var entities = GetTableContents(table, db);
                var result = JsonConvert.SerializeObject(entities, Formatting.Indented);
                Console.WriteLine(result);
            }
        }

        private static IReadOnlyList<object> GetTableContents(string table, ExampleContext db)
        {
            switch (table.ToLower()) {
                case "address": return db.Addresses.Include(x => x.City).ToArray();
                case "city": return db.Cities.Include(x => x.Country).ToArray();
                case "country": return db.Countries.ToArray();
                case "person": return db.Persons.Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country).ToArray();
                default: throw new ArgumentException($"Unknown table '{table}'.");
            }
        }

        private static string GetUsageMessage()
            => "USAGE: dotnet run <get|query> <verb args>\n\n"
               + "\tget <table name>\n"
               + "\t\tPrints the contents of <table name>.\n\n"
               + "\tquery <graphql query>\n"
               + "\t\tExecutes <graphql query> and prints the result.\n";
    }
}
