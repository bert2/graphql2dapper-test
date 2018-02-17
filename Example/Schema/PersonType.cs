namespace Example.Schema
{
    using GraphQL.Types;

    using Model;

    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType()
        {
            Name = "person";
            Field<IntGraphType>("id", resolve: context => context.Source.Id);
            Field<StringGraphType>("firstName", resolve: context => context.Source.FirstName);
            Field<StringGraphType>("lastName", resolve: context => context.Source.LastName);
        }
    }
}
