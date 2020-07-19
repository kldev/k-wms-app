using System;
using K.Wms.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace K.Wms.Data.Config {
    public class ConfigLegalFormType : AppEntityTypeConfiguration<LegalFormType> {
        protected override void ConfigureEntity(EntityTypeBuilder<LegalFormType> builder) {
            builder.HasKey (x => new {x.Country, x.Id});

            builder.HasMany (x => x.LegalFormTypeNames).WithOne (x => x.LegalFormType)
                .HasForeignKey (x => x.LegalFormTypeId).OnDelete (DeleteBehavior.SetNull);

            builder.Property (x => x.Country).HasMaxLength (255).HasConversion (x => x.ToString ( ),
                x => (LegalTypeCountry)Enum.Parse (typeof(LegalTypeCountry), x));
        }
    }
}
