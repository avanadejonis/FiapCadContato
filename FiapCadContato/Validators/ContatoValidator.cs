using Fiap.Domain.Entities;
using FluentValidation;

namespace FiapCadContato.Validators
{
    public class ContatoValidator : AbstractValidator<Contato>
    {
        public ContatoValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("O Nome não deve ser vazio.")
                .MinimumLength(2)
                .WithMessage("O 'Nome' deve ter no mínimo 2 caracteres.")
                .MaximumLength(50)
                .WithMessage("O 'Nome' deve er no máximo 50 caracteres.");

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("O email é obrigatório.")
                .Must((contato, builder) => contato.Email.Contains("@") && contato.Email.Contains(".") && contato.Email.Contains(".com"))
                .WithMessage("O email deve conter @ , ponto(.) e finalizado com (.com).");

            RuleFor(x => x.Telefone.ToString()).Length(8, 9)
                .WithMessage("O telefone deve conter 8 ou 9 carcteres.")
                .NotEmpty()
                .WithMessage("O telefone não pode ser vazio.");

            RuleFor(x => x.DDD)
                .NotEmpty()
                .WithMessage("O DDD não pode ser vazio.")
                .GreaterThan(0)
                .WithMessage("O DDD é inválido.")
                .Must((contato, builder) => contato.DDD.ToString().Length == 2)
                .WithMessage("DDD deve conter 2 caracteres.");
        }

        public bool ValidarId(int id)
        {
            if (id.ToString().Length == 0 || id == 0) return false; return true;
        }

        public bool ValidarDDD(int ddd)
        {
            if (ddd.ToString().Length == 2) return true; return false;
        }

    }
}
