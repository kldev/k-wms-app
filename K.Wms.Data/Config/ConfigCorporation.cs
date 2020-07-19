using K.Wms.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace K.Wms.Data.Config {
    public class ConfigCorporation : AppEntityTypeConfiguration<Corporation> {
        protected override void ConfigureEntity(EntityTypeBuilder<Corporation> builder) {
            builder.HasKey (x => x.Id);

            builder.HasMany (x => x.LegalForms).WithOne (x => x.Corporation)
                .HasForeignKey (x => x.CorporationId).OnDelete (DeleteBehavior.SetNull);
        }
    }
}
