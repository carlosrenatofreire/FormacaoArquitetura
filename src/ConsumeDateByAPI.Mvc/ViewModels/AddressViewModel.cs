using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ConsumeDateByAPI.Mvc.ViewModels
{
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Morada")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Street { get; set; } = string.Empty;

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string Number { get; set; } = string.Empty;

        [Display(Name = "Complemento ou Referencia")]
        public string? Complement { get; set; }

        [Display(Name = "Freguesia")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Neighborhood { get; set; } = string.Empty;

        [Display(Name = "Código Postal")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo {0} precisa ter {1} caracteres", MinimumLength = 8)]
        public string PostalCode { get; set; } = string.Empty;

        [Display(Name = "Conselho")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string City { get; set; } = string.Empty;

        [Display(Name = "Distrito")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string State { get; set; } = string.Empty;

        [HiddenInput]
        public Guid SupplierId { get; set; }
    }
}
