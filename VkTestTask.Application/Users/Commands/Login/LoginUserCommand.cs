

using MediatR;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Commands.Login;

public record LoginUserCommand(string Login, string Password) : IRequest<OperationResult<string>>;