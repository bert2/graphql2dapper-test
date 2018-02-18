namespace Example.Schema
{
    using System.Linq;

    using GraphQL.Types;

    public class QueryType : ObjectGraphType
    {
        public QueryType()
        {
            Field<ListGraphType<PersonType>>(
                "persons", 
                resolve: _ => ExampleSeed.Persons);

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
