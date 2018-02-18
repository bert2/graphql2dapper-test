namespace Example.Cli
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    public static class Program
    {
        private const string UsageMessage = 
            "USAGE: dotnet run <get|query> <verb args>\n\n" 
            + "\tget <table name>\n" 
            + "\t\tPrints the contents of <table name>.\n\n" 
            + "\tquery <graphql query>\n" 
            + "\t\tExecutes <graphql query> and prints the result.\n";

        public static void Main(string[] args)
        {
            InitDb();

            try {
                var result = Run(args);
                Console.WriteLine(result);
                Environment.Exit(0);
            }
            catch (ArgumentException e) {
                Console.WriteLine($"ERROR: {e.Message}\n");
                Console.WriteLine(UsageMessage);
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
                db.Database.EnsureCreated();
                db.Seed();
            }
        }

        private static string Run(IReadOnlyList<string> args)
        {
            if (args.Count < 1) throw new ArgumentException("Missing verb.");

            switch (args[0].ToLower()) {
                case "get":   return ExecuteGet(args);
                case "query": return ExecuteQuery(args);
                default:      throw new ArgumentException($"Unknown verb '{args[0]}'.");
            }
        }

        private static string ExecuteGet(IReadOnlyList<string> args)
        {
            if (args.Count < 2) throw new ArgumentException("Missing table name.");

            using (var db = new ExampleContext()) {
                var entities = GetTableContents(args[1], db);
                return JsonConvert.SerializeObject(entities, Formatting.Indented);
            }
        }

        private static IReadOnlyList<object> GetTableContents(string table, ExampleContext db)
        {
            switch (table.ToLower()) {
                case "address": return db.Addresses.Include(x => x.City).ToArray();
                case "city":    return db.Cities.Include(x => x.Country).ToArray();
                case "country": return db.Countries.ToArray();
                case "person":  return db.Persons.Include(x => x.Address).ThenInclude(x => x.City).ThenInclude(x => x.Country).ToArray();
                default: throw new ArgumentException($"Unknown table '{table}'.");
            }
        }

        private static string ExecuteQuery(IReadOnlyList<string> args)
        {
            if (args.Count < 2) throw new ArgumentException("Missing GraphQL query.");

            return Execute.Query(args[1]);
        }
    }
}
