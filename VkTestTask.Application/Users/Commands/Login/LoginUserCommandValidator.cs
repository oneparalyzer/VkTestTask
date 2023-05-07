using FluentValidation;

namespace VkTestTask.Application.Users.Commands.Login;

public sealed class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Login).NotEmpty().WithMessage("Логин не должен быть пучтым.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Пароль не должен быть пучтым.");
    }
}
