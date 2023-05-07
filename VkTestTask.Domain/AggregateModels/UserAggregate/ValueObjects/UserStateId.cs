using VKTestTask.Domain.SeedWorks;

namespace VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;

public sealed class UserStateId : ValueObject
{
    private UserStateId(Guid value)
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

    public static UserStateId Create(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }
        return new UserStateId(id);
    }

    public static UserStateId Create()
    {
        return new UserStateId(Guid.NewGuid());
    }
}
