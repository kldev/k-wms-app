using System;
using System.Collections.Generic;

namespace K.Wms.Data.Entity {
    public class Corporation : BaseEntity<Guid> {
        public string Name { get; set; }
        public string LogoUrl { get; set; }

        public IList<LegalForm> LegalForms { get; set; }
    }
}
