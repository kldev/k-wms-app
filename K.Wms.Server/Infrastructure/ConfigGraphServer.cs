using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace K.Wms.Sever.Infrastructure {
    public static class ConfigGraphServer {

        public static void AddGraphServer(this IServiceCollection services) {

            services.AddDataLoaderRegistry ( ).AddGraphQL (
                SchemaBuilder.New ( )
                    .AddQueryType<Query> ( )
                    .Create ( ),
                new QueryExecutionOptions { ForceSerialExecution = true });
        }

        public static void UseAppGraph(this IApplicationBuilder builder) {
            builder.UseGraphQL ( );
            builder.UsePlayground ( );
        }
    }
}
