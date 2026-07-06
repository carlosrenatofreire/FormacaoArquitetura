using Architecture.Business.Models.Enums;
using Architecture.Business.Models.Internals.Entities;
using FluentValidation;

namespace Architecture.Business.Validations.Entities
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(f => f.SupplierType == SupplierType.Individual, () =>
            {
                RuleFor(f => f.Document)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(9).WithMessage("O campo {PropertyName} precisa ter {MinLength}");
            });

            When(f => f.SupplierType == SupplierType.LegalEntity, () =>
            {
                RuleFor(f => f.Document)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(9).WithMessage("O campo {PropertyName} precisa ter {MinLength}");
            });

        }

    }
