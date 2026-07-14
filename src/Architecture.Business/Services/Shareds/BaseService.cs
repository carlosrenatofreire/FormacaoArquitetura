using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Shareds;
using FluentValidation;
using FluentValidation.Results;

namespace Architecture.Business.Services.Shareds
{
    public abstract class BaseService
    {
        private readonly INotifierService _notifier;
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(INotifierService notifier, IUnitOfWork unitOfWork)
        {
            _notifier = notifier;
            _unitOfWork = unitOfWork;
        }

        // Grava, numa única transação, todas as operações que os repositórios acumularam
        // no ChangeTracker durante o caso de uso corrente.
        protected async Task<bool> Commit()
        {
            var success = await _unitOfWork.Commit();

            if (!success)
            {
                Notify("Ocorreu um erro ao gravar os dados. Tente novamente.");
            }

            return success;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notify(item.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity)
                                        where TV : AbstractValidator<TE>
                                        where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}
