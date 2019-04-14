namespace Example.Schema
{
    using System.Linq;

    using Dapper.GraphQL;

    using GraphQL.Types;

    using Microsoft.Data.Sqlite;

    using SqlQuery;

    public class QueryType : ObjectGraphType
    {
        public QueryType()
        {
            Field<ListGraphType<PersonType>>(
                "persons", 
                resolve: context => {
                    const string alias = "person";
                    var sqlQuery = SqlBuilder.From($"Persons {alias}");
                    
                    sqlQuery = new PersonQuery().Build(sqlQuery, context.FieldAst, alias);
                    using (var db = new SqliteConnection("Data Source=example.db")) {
                        return sqlQuery.Execute(db, context.FieldAst, new PersonMapper());
                    }
                });

            Field<PersonType>(
                "person", 
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => {
                    var id = context.GetArgument<long>("id");
                    return ExampleSeed.Persons.SingleOrDefault(p => p.Id == id);
                 });
        }
    }
}
