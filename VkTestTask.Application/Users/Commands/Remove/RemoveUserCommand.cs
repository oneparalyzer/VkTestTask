using MediatR;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Commands.Remove;

public record RemoveUserCommand(Guid UserId) : IRequest<SimpleOperationResult>;
