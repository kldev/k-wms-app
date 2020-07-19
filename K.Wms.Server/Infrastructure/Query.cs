using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using K.Wms.Data.Context;
using K.Wms.Data.Entity;

namespace K.Wms.Sever.Infrastructure {
    public class Query {

        [UseSorting]
        [UseSelection]
        [UseFiltering]
        public IQueryable<Corporation> GetCorporation([Service] WmsAppContext context) => context.Corporations;

        [UseSorting]
        [UseSelection]
        [UseFiltering]
        public IQueryable<LegalForm> GetLegalForm([Service] WmsAppContext context) => context.LegalForms;

        [UseSorting]
        [UseSelection]
        [UseFiltering]
        public IQueryable<LegalFormType> GetLegalFormType([Service] WmsAppContext context) => context.LegalFormTypes;

        [UseSorting]
        [UseSelection]
        [UseFiltering]
        public IQueryable<LegalFormTypeName> GetLegalFormTypeNames([Service] WmsAppContext context) => context.LegalFormTypeNames;
    }
}
