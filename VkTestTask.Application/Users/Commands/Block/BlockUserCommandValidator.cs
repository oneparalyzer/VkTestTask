using FluentValidation;

namespace VkTestTask.Application.Users.Commands.Block;

public sealed class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
{
    public BlockUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
    }
}
