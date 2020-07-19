using K.Wms.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace K.Wms.Data.Config {
    public abstract class AppEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class {
        public void Configure(EntityTypeBuilder<T> builder) {
            ConfigureEntity (builder);

            builder.AddChange ( );
            builder.AddSoftDelete ( );
        }

        protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
    }
}
