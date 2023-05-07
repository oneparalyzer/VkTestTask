

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkTestTask.Application.Common.Interfaces;
using VkTestTask.Domain.AggregateModels.UserAggregate;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Queries.GetAll;

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, OperationResult<IEnumerable<GetAllUsersDto>>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetAllUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OperationResult<IEnumerable<GetAllUsersDto>>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<IEnumerable<GetAllUsersDto>>();

        try
        {
            IEnumerable<User> users = await _context.Users
                .Include(user => user.UserGroup)
                .Include(user => user.UserState)
                .ToListAsync(cancellationToken);

            var usersDto = _mapper.Map<IEnumerable<GetAllUsersDto>>(users);

            operationResult.Data = usersDto;
            return operationResult;
        }
        catch (Exception ex)
        {
            operationResult.AddError("Произошла ошибка.");
            return operationResult;
        }
    }
}
