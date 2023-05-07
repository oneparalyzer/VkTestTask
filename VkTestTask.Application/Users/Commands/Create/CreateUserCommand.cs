using MediatR;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Commands.Create;

public record CreateUserCommand(
    string Login,
    string Password,
    string UserGroupCode) : IRequest<SimpleOperationResult>;
