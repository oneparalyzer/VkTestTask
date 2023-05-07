using VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;
using VKTestTask.Domain.SeedWorks;

namespace VkTestTask.Domain.AggregateModels.UserAggregate.Entities;

public sealed class UserState : Entity<UserStateId>
{
    private UserState(UserStateId id, string code, string description) : base(id)
    {
        Code = code;
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException(nameof(description));
        }
        Description = description;
    }

    public string Code { get; private set; }
    public string Description { get; private set; }

    public static UserState Create(string code, string description)
    {
        return new UserState(UserStateId.Create(), code, description);
    }
}
