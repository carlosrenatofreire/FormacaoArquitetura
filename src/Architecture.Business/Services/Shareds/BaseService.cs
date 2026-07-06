using Architecture.Business.Interfaces.Shareds;
using Architecture.Business.Models.Shareds;
using FluentValidation;
using FluentValidation.Results;

namespace Architecture.Business.Services.Shareds
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
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
