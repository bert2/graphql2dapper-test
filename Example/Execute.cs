namespace Example
{
    using System;

    using GraphQL;
    using GraphQL.Http;
    using GraphQL.Types;

    using Schema;

    public static class Execute
    {
        public static string Query(string query)
        {
            var result = new DocumentExecuter().ExecuteAsync(opts => {
                opts.Query = query;
                opts.Schema = new GraphQL.Types.Schema
                {
                    ResolveType = t => (IGraphType)Activator.CreateInstance(t),
                    Query = new QueryType()
                };
            }).Result;

            return new DocumentWriter(true).Write(result);
        }
    }
}
