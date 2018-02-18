namespace Example.Schema
{
    using GraphQL.Types;

    using Model;

    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType()
        {
            Field(x => x.Id);
            Field(x => x.FirstName);
            Field(x => x.LastName);
        }
    }
}
