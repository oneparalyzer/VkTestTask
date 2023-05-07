using MediatR;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Queries.GetAll;

public record GetAllUsersQuery() : IRequest<OperationResult<IEnumerable<GetAllUsersDto>>>;
