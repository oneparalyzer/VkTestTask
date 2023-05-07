using MediatR;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Queries.GetById;

public record GetUserByIdQuery(Guid UserId) : IRequest<OperationResult<GetUserByIdDto>>;
