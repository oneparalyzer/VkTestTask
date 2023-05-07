using VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;
using VKTestTask.Domain.SeedWorks;

namespace VkTestTask.Domain.AggregateModels.UserAggregate.Entities;

public sealed class UserGroup : Entity<UserGroupId>
{
    private UserGroup(UserGroupId id, string code, string description) : base(id)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentNullException(nameof(description));
        }
        Description = description;
    }

    public string Code { get; private set; }
    public string Description { get; private set; }

    public static UserGroup Create(string code, string description)
    {
        return new UserGroup(UserGroupId.Create(), code, description);
    }
}
