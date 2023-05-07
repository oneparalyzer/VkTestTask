using MediatR;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Queries.GetByPage;

public record GetUsersByPageQuery(int QuantityFildsOnPage, int Page) : IRequest<OperationResult<GetUsersByPageDto>>;
