

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkTestTask.Application.Common.Interfaces;
using VkTestTask.Application.Users.Queries.GetAll;
using VkTestTask.Domain.AggregateModels.UserAggregate;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Queries.GetByPage;

public sealed class GetUsersByPageQueryHandler : IRequestHandler<GetUsersByPageQuery, OperationResult<GetUsersByPageDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetUsersByPageQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<OperationResult<GetUsersByPageDto>> Handle(GetUsersByPageQuery query, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<GetUsersByPageDto>();

        try
        {
            float pageResult = query.QuantityFildsOnPage;
            double pageCount = Math.Ceiling(_context.Users.Count() / pageResult);

            IEnumerable<User> users = await _context.Users
                .Skip((query.Page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToListAsync(cancellationToken);

            var usersDto = _mapper.Map<IEnumerable<GetAllUsersDto>>(users);

            var usersOnPage = new GetUsersByPageDto
            {
                QuantityPages = (int)pageCount,
                CurrentPages = query.Page,
                Users = usersDto
            };
            operationResult.Data = usersOnPage;
            return operationResult;
        }
        catch (Exception ex)
        {
            operationResult.AddError("Произошла ошибка.");
            return operationResult;
        }
    }
}
