using K.Wms.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace K.Wms.Data.Context {
    public class WmsAppContext : CoreDbContext {
        public WmsAppContext() {
        }

        public WmsAppContext(DbContextOptions<WmsAppContext> options) : base (options) { }

        public DbSet<Corporation> Corporations { get; set; }
        public DbSet<LegalForm> LegalForms { get; set; }
        public DbSet<LegalFormType> LegalFormTypes { get; set; }
        public DbSet<LegalFormTypeName> LegalFormTypeNames { get; set; }
    }
}
