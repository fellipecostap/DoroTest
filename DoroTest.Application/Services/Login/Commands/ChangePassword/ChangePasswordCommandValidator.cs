using FluentValidation;

namespace DoroTest.Application.Services.Login.Commands.ChangePassword;
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(v => v.NewPassword)
         .NotEmpty().WithMessage("Senha não pode estar vazia")
         .MinimumLength(8).WithMessage("Caracteres mínimos não atingidos")
         .Matches("(?=.*[a-z])").WithMessage("Senha inválida")
         .Matches("(?=.*[A-Z])").WithMessage("Senha inválida")
         .Matches("(?=.*[0-9])").WithMessage("Senha inválida")
         .Matches("[^A-Za-z0-9a-áàâãéèêíïóôõöúçñ]").WithMessage("Senha inválida");
    }
}
