namespace Example.SqlQuery
{
    using System;

    using Dapper.GraphQL;

    using GraphQL.Language.AST;

    using Model;

    public class AddressQuery : IQueryBuilder<Address>
    {
        public SqlQueryContext Build(SqlQueryContext query, IHaveSelectionSet context, string alias)
        {
            query.Select($"{alias}.Id");
            query.SplitOn<Address>("Id");

            foreach (var field in context.GetSelectedFields())
            {
                switch (field.Key.ToLower())
                {
                    case "street":
                        query.Select($"{alias}.Street");
                        break;
                    case "city":
                        query.InnerJoin($"City on {alias}.CityId = City.Id");
                        query = new CityQuery().Build(query, field.Value, "City");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Unable to map field '{field.Key}' to table '{alias}'.");
                }
            }

            return query;
        }
    }
}