using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkTestTask.Application.Users.Commands.Block;
using VkTestTask.Application.Users.Commands.Create;
using VkTestTask.Application.Users.Commands.Login;
using VkTestTask.Application.Users.Commands.Remove;
using VkTestTask.Application.Users.Queries.GetAll;
using VkTestTask.Application.Users.Queries.GetById;
using VkTestTask.Application.Users.Queries.GetByPage;

namespace VkTestTask.Api.Controllers;

[Route("api/users")]
[ApiController]
public sealed class UsersController : ControllerBase
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var operationResult = await _mediator.Send(new GetAllUsersQuery());
        return Ok(operationResult);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("page/{page}")]
    public async Task<IActionResult> GetAllAsync([FromRoute]int page, [FromBody]int quantityFildsOnPage)
    {
        var operationResult = await _mediator.Send(new GetUsersByPageQuery(quantityFildsOnPage, page));
        return Ok(operationResult);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute]Guid id)
    {
        var operationResult = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(operationResult);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}/remove")]
    public async Task<IActionResult> RemoveAsync([FromRoute]Guid id)
    {
        var command = new RemoveUserCommand(id);

        var validator = new RemoveUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return UnprocessableEntity(validationResult);
        }

        var operationResult = await _mediator.Send(command);
        return Ok(operationResult);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id}/block")]
    public async Task<IActionResult> BlockAsync([FromRoute]Guid id)
    {
        var command = new BlockUserCommand(id);

        var validator = new BlockUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return UnprocessableEntity(validationResult);
        }

        var operationResult = await _mediator.Send(command);
        return Ok(operationResult);
    }

    [HttpPut("create")]
    public async Task<IActionResult> CreateAsync([FromBody]CreateUserCommand command)
    {
        var validator = new CreateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return UnprocessableEntity(validationResult);
        }

        var operationResult = await _mediator.Send(command);
        return Ok(operationResult);
    }

    [HttpPost("auth")]
    public async Task<IActionResult> Authorize([FromBody]LoginUserCommand command)
    {
        var validator = new LoginUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return UnprocessableEntity(validationResult);
        }

        var operationResult = await _mediator.Send(command);
        return Ok(operationResult);
    }
}
