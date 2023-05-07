using MediatR;
using Microsoft.EntityFrameworkCore;
using VkTestTask.Application.Common.Interfaces;
using VkTestTask.Domain.AggregateModels.Constants;
using VkTestTask.Domain.AggregateModels.UserAggregate;
using VkTestTask.Domain.AggregateModels.UserAggregate.Entities;
using VkTestTask.Domain.Common.OperationResults;
using VKTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;

namespace VkTestTask.Application.Users.Commands.Create;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, SimpleOperationResult>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SimpleOperationResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var operationResult = new SimpleOperationResult();

        using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                string? userLogin = await _context.Users
            .Select(x => x.Login)
            .FirstOrDefaultAsync(userLogin => userLogin == command.Login, cancellationToken);
                if (userLogin is not null)
                {
                    operationResult.AddError("Пользователь с таким логином уже существует.");
                }

                if (command.UserGroupCode == UserGroupCodeConstants.Admin)
                {
                    User? userAdmin = await _context.Users
                        .Include(x => x.UserGroup)
                        .FirstOrDefaultAsync(user => user.UserGroup.Code == UserGroupCodeConstants.Admin, cancellationToken);
                    if (userAdmin is not null)
                    {
                        operationResult.AddError($"Пользователь с ролью: '{UserGroupCodeConstants.Admin}' уже существует.");
                    }
                }

                UserGroup? userGroup = await _context.UserGroups
                    .FirstOrDefaultAsync(userGroup => userGroup.Code == command.UserGroupCode, cancellationToken);
                if (userGroup is null)
                {
                    operationResult.AddError($"Роли: '{command.UserGroupCode}' не существует.");
                }

                UserState? userState = await _context.UserStates
                    .FirstOrDefaultAsync(userState => userState.Code == UserStateCodeConstants.Active, cancellationToken);

                if (!operationResult.Success)
                {
                    return operationResult;
                }

                var user = User.Create(
                    command.Login,
                    Password.Create(command.Password),
                    userState,
                    userGroup);

                await _context.Users.AddAsync(user, cancellationToken);
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
