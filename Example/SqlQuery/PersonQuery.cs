namespace Example.SqlQuery
{
    using System;

    using Dapper.GraphQL;

    using GraphQL.Language.AST;

    using Model;

    public class PersonQuery : IQueryBuilder<Person>
    {
        public SqlQueryContext Build(SqlQueryContext query, IHaveSelectionSet context, string alias)
        {
            query.Select($"{alias}.Id");
            query.SplitOn<Person>("Id");

            foreach (var field in context.GetSelectedFields())
            {
                switch (field.Key.ToLower())
                {
                    case "firstname":
                        query.Select($"{alias}.FirstName");
                        break;
                    case "lastname":
                        query.Select($"{alias}.LastName");
                        break;
                    case "address":
                        query.InnerJoin($"Address on {alias}.AddressId = Address.Id");
                        query = new AddressQuery().Build(query, field.Value, "Address");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Unable to map field '{field.Key}' to table '{alias}'.");
                }
            }

            return query;
        }
    }
}