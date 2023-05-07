using MediatR;
using Microsoft.EntityFrameworkCore;
using VkTestTask.Application.Common.Interfaces;
using VkTestTask.Domain.AggregateModels.UserAggregate;
using VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Commands.Remove;

public sealed class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, SimpleOperationResult>
{
    private readonly IApplicationDbContext _context;

    public RemoveUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SimpleOperationResult> Handle(RemoveUserCommand command, CancellationToken cancellationToken)
    {
        var operationResult = new SimpleOperationResult();

        using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                User? existingUser = await _context.Users
                    .FirstOrDefaultAsync(user => user.Id == UserId.Create(command.UserId), cancellationToken);
                if (existingUser is null)
                {
                    operationResult.AddError("Пользователь не найден.");
                    return operationResult;
                }

                _context.Users.Remove(existingUser);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return operationResult;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                operationResult.AddError("Произошла ошибка.");
                return operationResult;
            }
        }
    }
}
