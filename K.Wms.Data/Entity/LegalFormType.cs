using System.Collections.Generic;

namespace K.Wms.Data.Entity {
    public enum LegalTypeCountry {
        Poland = 0,
        German = 1,
        UnitedKingdom = 2
    }

    public class LegalFormType : BaseEntity<int> {
        public LegalTypeCountry Country { get; set; }

        public IList<LegalFormTypeName> LegalFormTypeNames { get; set; }

        public IList<LegalForm> LegalForms { get; set; }
    }
}
