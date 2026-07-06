using Architecture.Business.Models.Shareds;

namespace Architecture.Business.Models.Internals.Entities
{
    public class Product : Entity
    {
        public Guid SupplierId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public DateTime RegisterDate { get; set; }

        public bool Active { get; set; }

        /* EF Relation */
        public Supplier Supplier { get; set; }
    }

}
