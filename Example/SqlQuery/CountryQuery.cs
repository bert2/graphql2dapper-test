namespace Example.SqlQuery
{
    using System;

    using Dapper.GraphQL;

    using GraphQL.Language.AST;

    using Model;

    public class CountryQuery : IQueryBuilder<Country>
    {
        public SqlQueryContext Build(SqlQueryContext query, IHaveSelectionSet context, string alias)
        {
            query.Select($"{alias}.Id");
            query.SplitOn<Country>("Id");

            foreach (var field in context.GetSelectedFields()) {
                switch (field.Key.ToLower()) {
                    case "name":
                        query.Select($"{alias}.Name"); 
                        break;
                    case "isocode": 
                        query.Select($"{alias}.IsoCode"); 
                        break;
                }
            }

            return query;
        }
    }
}
