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
                        var addressAlias = $"{alias}Address";
                        query.InnerJoin($"Addresses {addressAlias} on {alias}.AddressId = {addressAlias}.Id");
                        query = new AddressQuery().Build(query, field.Value, addressAlias);
                        break;
                }
            }

            return query;
        }
    }
}