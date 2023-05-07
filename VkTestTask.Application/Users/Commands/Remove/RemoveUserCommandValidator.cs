

using FluentValidation;

namespace VkTestTask.Application.Users.Commands.Remove;

public sealed class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
{
    public RemoveUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
    }
}
