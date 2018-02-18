namespace Example.Schema
{
    using GraphQL.Types;

    using Model;

    public class CountryType : ObjectGraphType<Country>
    {
        public CountryType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.IsoCode);
        }
    }
}
