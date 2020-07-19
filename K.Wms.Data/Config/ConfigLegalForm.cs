using K.Wms.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace K.Wms.Data.Config {
    public class ConfigLegalForm : AppEntityTypeConfiguration<LegalForm> {
        protected override void ConfigureEntity(EntityTypeBuilder<LegalForm> builder) {
            builder.HasKey (x => x.Id);
            builder.Property (x => x.City).IsRequired ( ).HasMaxLength (255);
            builder.Property (x => x.Name).IsRequired ( ).HasMaxLength (255);
            builder.HasIndex (x => x.Name).IsUnique ( );

            builder.HasOne (x => x.LegalFormType).WithMany (x => x.LegalForms).HasForeignKey (x => x.LegalFormTypeId)
                .OnDelete (DeleteBehavior.SetNull);
        }
    }
}
