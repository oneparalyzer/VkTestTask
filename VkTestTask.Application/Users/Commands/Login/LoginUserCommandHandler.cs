

using MediatR;
using Microsoft.EntityFrameworkCore;
using VkTestTask.Application.Common.Interfaces;
using VkTestTask.Application.Services.Interfaces;
using VkTestTask.Domain.AggregateModels.UserAggregate;
using VkTestTask.Domain.Common.OperationResults;
using VKTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;

namespace VkTestTask.Application.Users.Commands.Login;

public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, OperationResult<string>>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginUserCommandHandler(IApplicationDbContext context, IJwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<OperationResult<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<string>();

        User? existingUser = await _context.Users
            .Include(x => x.UserGroup)
            .FirstOrDefaultAsync(user => 
                user.Login == command.Login &&
                user.Password.Value == Password.Create(command.Password).Value);
        if (existingUser is null)
        {
            operationResult.AddError("Не верный логин или пароль.");
            return operationResult;
        }

        operationResult.Data = _jwtTokenService.GenerateJwtToken(existingUser);
        return operationResult;
    }
}
