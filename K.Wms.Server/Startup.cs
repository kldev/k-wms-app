using K.Wms.Data;
using K.Wms.Sever.BackgroundService;
using K.Wms.Sever.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace K.Wms.Sever {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.UseAppDatabase (Configuration);
            services.AddGraphServer ( );

            services.AddHostedService<DbMigrateService> ( );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ( )) {
                app.UseDeveloperExceptionPage ( );
            }
            app.UseRouting ( );

            app.UseAppGraph ( );

            app.UseEndpoints (endpoints => {
                endpoints.MapGet ("/", async context => {
                    await context.Response.WriteAsync ("Server on address /graphql");
                });
            });
        }
    }
}
