namespace Example.SqlQuery
{
    using System;

    using Dapper.GraphQL;

    using GraphQL.Language.AST;

    using Model;

    public class CityQuery : IQueryBuilder<City>
    {
        public SqlQueryContext Build(SqlQueryContext query, IHaveSelectionSet context, string alias)
        {
            query.Select($"{alias}.Id");
            query.SplitOn<City>("Id");

            foreach (var field in context.GetSelectedFields())
            {
                switch (field.Key.ToLower())
                {
                    case "name": 
                        query.Select($"{alias}.Name"); 
                        break;
                    case "zipcode": 
                        query.Select($"{alias}.ZipCode"); 
                        break;
                    case "country":
                        var countryAlias = $"{alias}Country";
                        query.InnerJoin($"Countries {countryAlias} on {alias}.CountryId = {countryAlias}.Id");
                        query = new CountryQuery().Build(query, field.Value, countryAlias);
                        break;
                }
            }

            return query;
        }
    }
}