using MediatR;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Commands.Block;

public record BlockUserCommand(Guid UserId) : IRequest<SimpleOperationResult>;