namespace K.Wms.Data.Entity {
    public class LegalFormTypeName : BaseEntity<int> {
        public string Name { get; set; }
        public string Description { get; set; }

        public int LegalFormTypeId { get; set; }
        public LegalFormType LegalFormType { get; set; }
    }
}
