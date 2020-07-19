using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using K.Wms.Data.Context;
using K.Wms.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace K.Wms.Sever.BackgroundService {
    public class DbMigrateService : IHostedService {
        private readonly ILogger<DbMigrateService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _iServiceScopeFactory;

        public DbMigrateService(ILogger<DbMigrateService> logger,
            IConfiguration configuration, IServiceScopeFactory iServiceScopeFactory) {
            _logger = logger;
            _configuration = configuration;
            _iServiceScopeFactory = iServiceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            _logger.LogInformation (
                "DbMigrateService Hosted Service is starting.");

            var polandFormTypes = new LegalFormType ( ) {
                Country = LegalTypeCountry.Poland,
                LegalFormTypeNames = new List<LegalFormTypeName> ( ) {
                    new LegalFormTypeName ( ) {Name = "Spółka z.o.o", Description = string.Empty},
                    new LegalFormTypeName ( ) {Name = "Spółka komandytowa", Description = string.Empty},
                    new LegalFormTypeName ( ) {Name = "Spółka Akcyjna", Description = string.Empty},
                    new LegalFormTypeName ( ) {Name = "Spółka Jawna", Description = string.Empty},
                    new LegalFormTypeName ( ) {Name = "Spółka Partnerska", Description = string.Empty},
                }
            };
            var germanFormTypes = new LegalFormType ( ) {
                Country = LegalTypeCountry.German,
                LegalFormTypeNames = new List<LegalFormTypeName> ( ) {
                    new LegalFormTypeName ( ) {Name = "Spółka akcyjna (AG)", Description = string.Empty},
                    new LegalFormTypeName ( ) {Name = "Spółka komandytowo-akcyjna (KGaA)", Description = string.Empty},
                    new LegalFormTypeName ( ) {Name = "Spółka komandytowa (KG)", Description = string.Empty},
                    new LegalFormTypeName ( ) {Name = "Spółka cywilna (GbR)", Description = string.Empty},
                    new LegalFormTypeName ( ) {Name = "Spółka jawna (OHG)", Description = string.Empty},
                    new LegalFormTypeName ( ) {Name = "Spółka partnerska (Partnerschaft)", Description = string.Empty},
                }
            };

            using (var scope = _iServiceScopeFactory.CreateScope ( )) {

                var context = scope.ServiceProvider.GetService<WmsAppContext> ( );

                await context.Database.EnsureDeletedAsync (cancellationToken);

                await context.Database.MigrateAsync (cancellationToken);

                await context.SaveChangesAsync (cancellationToken);
                if (await context.LegalFormTypes.CountAsync (x => x.Country == LegalTypeCountry.Poland,
                    cancellationToken) == 0) {
                    await context.LegalFormTypes.AddAsync (polandFormTypes, cancellationToken);
                    await context.SaveChangesAsync (cancellationToken);
                }

                if (await context.LegalFormTypes.CountAsync (x => x.Country == LegalTypeCountry.German,
                    cancellationToken) == 0) {
                    await context.LegalFormTypes.AddAsync (germanFormTypes, cancellationToken);
                    await context.SaveChangesAsync (cancellationToken);
                }

                if (await context.Corporations.CountAsync (cancellationToken) == 0) {
                    var formTypeGerman = await context.LegalFormTypes.Where (x => x.Country == LegalTypeCountry.German)
                        .FirstOrDefaultAsync (cancellationToken);

                    var formTypePoland = await context.LegalFormTypes.Where (x => x.Country == LegalTypeCountry.Poland)
                        .FirstOrDefaultAsync (cancellationToken);

                    _logger.LogInformation ($"formTypeGerman: {formTypeGerman.Country}");
                    _logger.LogInformation ($"formTypePoland: {formTypePoland.Country}");

                    var germanCorp = new Corporation ( ) {
                        Id = Guid.NewGuid ( ),
                        Name = "German 1",
                        LegalForms = new List<LegalForm> ( ) {
                            new LegalForm ( ) {
                                Name = "G1",
                                City = "City 1", LegalFormType = formTypeGerman, LegalFormTypeId = formTypeGerman.Id,
                                VatId = "G1-VAT 1"
                            },
                            new LegalForm ( ) {
                                Name = "G2",
                                City = "City 2", LegalFormType = formTypeGerman, LegalFormTypeId = formTypeGerman.Id,
                                VatId = "G2-VAT 1"
                            },
                        }
                    };

                    var germanAndPolishCorp = new Corporation ( ) {
                        Id = Guid.NewGuid ( ),
                        Name = "German/Polish 2",
                        LegalForms = new List<LegalForm> ( ) {
                            new LegalForm ( ) {
                                Name = "GP 1",
                                City = "City 1", LegalFormType = formTypeGerman, LegalFormTypeId = formTypeGerman.Id,
                                VatId = "VAT 1"
                            },
                            new LegalForm ( ) {
                                Name = "GP 2",
                                City = "City Poland 1",
                                LegalFormType = formTypePoland,
                                LegalFormTypeId = formTypePoland.Id,
                                VatId = "VAT 2"
                            },
                            new LegalForm ( ) {
                                City = "City Poland 2",
                                Name = "GP 3",
                                LegalFormType = formTypePoland,
                                LegalFormTypeId = formTypePoland.Id,
                                VatId = "VAT 3"
                            },
                            new LegalForm ( ) {
                                Name = "GP 4",
                                City = "City Poland 3",
                                LegalFormType = formTypePoland,
                                LegalFormTypeId = formTypeGerman.Id,
                                VatId = "VAT 4"
                            }
                        }
                    };

                    await context.Corporations.AddRangeAsync (new[] {germanCorp, germanAndPolishCorp},
                        cancellationToken);
                    await context.SaveChangesAsync (cancellationToken);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken) {
            _logger.LogInformation (
                "DbMigrateService Hosted Service is stopping.");

            await Task.CompletedTask;
        }
    }
}
