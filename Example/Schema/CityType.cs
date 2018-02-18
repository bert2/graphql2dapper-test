namespace Example.Schema
{
    using GraphQL.Types;

    using Model;

    public class CityType : ObjectGraphType<City>
    {
        public CityType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.ZipCode);
            Field(x => x.Country, type: typeof(CountryType));
        }
    }
}
