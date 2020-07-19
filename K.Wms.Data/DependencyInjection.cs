using K.Wms.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace K.Wms.Data {
    public static class DependencyInjection {
        private static ILoggerFactory CreateLogger() {
            var loggerFactory = LoggerFactory.Create (builder => {
                builder.AddFilter ("Microsoft", LogLevel.Warning)
                    .AddFilter ("System", LogLevel.Warning)
                    .AddFilter (DbLoggerCategory.Name, LogLevel.Information)
                    .AddConsole ( );
            }
            );

            return loggerFactory;
        }

        public static IServiceCollection UseAppDatabase(this IServiceCollection serviceCollection,
            IConfiguration configuration) {
            Setup<WmsAppContext> (serviceCollection, configuration);
            return serviceCollection;
        }

        private static void Setup<T>(IServiceCollection services, IConfiguration configuration) where T : DbContext {
            services.AddDbContext<T> (options => {
                options.UseNpgsql (configuration["ConnectionStrings:AppConnection"],
                    b => b.MigrationsAssembly (typeof (T).Assembly.FullName));

                options.UseSnakeCaseNamingConvention ( );
#if DEBUG
                options.UseLoggerFactory (CreateLogger ( ));
#endif
            });
        }
    }
}
