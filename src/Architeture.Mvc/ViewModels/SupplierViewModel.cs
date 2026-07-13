using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Architeture.Mvc.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "NIF")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 9)]
        public string Document { get; set; }

        [DisplayName("Tipo de Fornecedor")]
        public int SupplierType { get; set; }

        public AddressViewModel? Address { get; set; }

        [DisplayName("Ativo?")]
        public bool Active { get; set; }

        public IEnumerable<ProductViewModel>? Products { get; set; }
    }
}
