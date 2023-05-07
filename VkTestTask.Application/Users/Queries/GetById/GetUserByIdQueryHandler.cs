using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VkTestTask.Application.Common.Interfaces;
using VkTestTask.Application.Users.Queries.GetAll;
using VkTestTask.Domain.AggregateModels.UserAggregate;
using VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Application.Users.Queries.GetById;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, OperationResult<GetUserByIdDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetUserByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OperationResult<GetUserByIdDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<GetUserByIdDto>();

        try
        {
            User? user = await _context.Users
                .Include(user => user.UserGroup)
                .Include(user => user.UserState)
                .FirstOrDefaultAsync(user => user.Id == UserId.Create(query.UserId));
            if (user is null)
            {
                operationResult.AddError("Пользователь не найден.");
                return operationResult;
            }

            var userDto = _mapper.Map<GetUserByIdDto>(user);

            operationResult.Data = userDto;
            return operationResult;
        }
        catch (Exception ex)
        {
            operationResult.AddError("Произошла ошибка.");
            return operationResult;
        }
    }
}
