using FluentValidation;

namespace DoroTest.Application.Services.User.Commands.CreateUser;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.Password)
         .NotEmpty().WithMessage("Senha não pode ser vazia")

         .MinimumLength(8).WithMessage("Quantidade minima de caracteres não atingida")

         .Matches("(?=.*[a-z])").WithMessage("Senha Inválida")

         .Matches("(?=.*[A-Z])").WithMessage("Senha Inválida")

         .Matches("(?=.*[0-9])").WithMessage("Senha Inválida")

         .Matches("[^A-Za-z0-9a-áàâãéèêíïóôõöúçñ]").WithMessage("Senha Inválida");

        RuleFor(v => v.UserType)
            .NotEmpty().WithMessage("UserType não pode ser vazio")
            .Must(userType => userType == "Admin" || userType == "Public")
            .WithMessage("UserType deve ser 'Admin' ou 'Public'");
    }
}
