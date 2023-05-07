using System.Security.Cryptography;
using System.Text;
using VKTestTask.Domain.SeedWorks;

namespace VKTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;

public class Password : ValueObject
{
    private Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }
        Value = value;
    }

    public string Value { get; }

    public static Password Create(string value)
    {
        string valueHash = GetHash(value);
        return new Password(valueHash);
    }

    private static string GetHash(string value)
    {
        var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

        return Convert.ToBase64String(hash);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
