

using VKTestTask.Domain.SeedWorks;

namespace VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;

public sealed class UserGroupId : ValueObject
{
    private UserGroupId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(value));
        }
        Value = value;
    }

    public Guid Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static UserGroupId Create(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }
        return new UserGroupId(id);
    }

    public static UserGroupId Create()
    {
        return new UserGroupId(Guid.NewGuid());
    }
}
