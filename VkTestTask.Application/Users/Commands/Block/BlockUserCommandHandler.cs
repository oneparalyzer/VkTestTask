using MediatR;
using Microsoft.EntityFrameworkCore;
using VkTestTask.Application.Common.Interfaces;
using VkTestTask.Domain.AggregateModels.Constants;
using VkTestTask.Domain.AggregateModels.UserAggregate;
using VkTestTask.Domain.AggregateModels.UserAggregate.Entities;
using VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Commands.Block;

public sealed class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, SimpleOperationResult>
{
    private readonly IApplicationDbContext _context;

    public BlockUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SimpleOperationResult> Handle(BlockUserCommand command, CancellationToken cancellationToken)
    {
        var operationResult = new SimpleOperationResult();

        using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                User? existingUser = await _context.Users
            .       FirstOrDefaultAsync(user => user.Id == UserId.Create(command.UserId), cancellationToken);
                if (existingUser is null)
                {
                    operationResult.AddError("Пользователь не найден.");
                    return operationResult;
                }

                UserState? userStateBlock = await _context.UserStates
                    .FirstOrDefaultAsync(userSate => userSate.Code == UserStateCodeConstants.Blocked, cancellationToken);

                existingUser.Update(
                    existingUser.Login,
                    existingUser.Password,
                    userStateBlock,
                    existingUser.UserGroup);

                _context.Users.Update(existingUser);
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
