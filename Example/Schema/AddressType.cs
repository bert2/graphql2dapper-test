namespace Example.Schema
{
    using GraphQL.Types;

    using Model;
    public class AddressType : ObjectGraphType<Address>
    {
        public AddressType()
        {
            Field(x => x.Id);
            Field(x => x.Street);
            Field(x => x.City, type: typeof(CityType));
        }
    }
}
