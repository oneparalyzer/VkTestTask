using VkTestTask.Domain.AggregateModels.UserAggregate;

namespace VkTestTask.Application.Services.Interfaces;

public interface IJwtTokenService
{
    string GenerateJwtToken(User user);
}
