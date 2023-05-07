

using VKTestTask.Domain.SeedWorks;

namespace VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;

public sealed class UserId : ValueObject
{
    private UserId(Guid value)
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

    public static UserId Create(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }
        return new UserId(id);
    }

    public static UserId Create()
    {
        return new UserId(Guid.NewGuid());
    }
}
