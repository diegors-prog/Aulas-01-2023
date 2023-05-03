using FluentValidation;
using FluentValidation.Results;
using Domain.Entities;


namespace Services
{
    public abstract class BaseService
    {
        // private readonly INotificador _notificador;

        protected BaseService()
        {
        }

        // protected void Notificar(ValidationResult validationResult)
        // {
        //     foreach (var error in validationResult.Errors)
        //     {
        //         Notificar(error.ErrorMessage);
        //     }
        // }

        // protected void Notificar(string mensagem)
        // {
        //     _notificador.Handle(new Notificacao(mensagem));
        // }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if(validator.IsValid) return true;

            return false;
        }
    }
}