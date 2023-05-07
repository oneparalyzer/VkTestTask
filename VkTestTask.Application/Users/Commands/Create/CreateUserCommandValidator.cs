

using FluentValidation;

namespace VkTestTask.Application.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Login).MinimumLength(5).WithMessage("Минимальная длина логина: 5");
        RuleFor(x => x.Password).MinimumLength(5).WithMessage("Минимальная длина пароля: 5");
        RuleFor(x => x.UserGroupCode).NotEmpty();
    }
}
