using System;

namespace K.Wms.Data.Entity {
    public class LegalForm : BaseEntity<Guid> {
        public string Name { get; set; }
        public string City { get; set; } = string.Empty;
        public string VatId { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;

        public int LegalFormTypeId { get; set; }
        public LegalFormType LegalFormType { get; set; }

        public Guid CorporationId { get; set; }
        public Corporation Corporation { get; set; }
    }
}
