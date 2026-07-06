using Architecture.Business.Models.Enums;
using Architecture.Business.Models.Internals.Parameters;
using Architecture.Business.Models.Shareds;

namespace Architecture.Business.Models.Internals.Entities
{
    public class Supplier : Entity
    {
        public string? Name { get; set; }
        public string? Document { get; set; }
        public SupplierType SupplierType { get; set; }
        public bool Active { get; set; }
        public Address? Address { get; set; }

        /* EF Relation */
        public IEnumerable<Product> Products { get; set; }
    }
}
