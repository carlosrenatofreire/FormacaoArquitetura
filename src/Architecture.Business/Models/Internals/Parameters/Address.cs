using Architecture.Business.Models.Shareds;

namespace Architecture.Business.Models.Internals.Parameters
{
    public class Address : Entity
    {
        public Guid SupplierId { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string? PostalCode { get; set; }
        public string? Neighborhood { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        /* EF Relation */
        public Supplier Supplier { get; set; }
    }
}
