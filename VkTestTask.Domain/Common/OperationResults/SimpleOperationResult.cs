

namespace VkTestTask.Domain.Common.OperationResults;

public class SimpleOperationResult
{
    private readonly HashSet<string> _errors = new();
    public bool Success { get; private set; } = true;
    public IReadOnlyCollection<string> Errors => _errors;

    public void AddError(string errorMessage)
    {
        if (Success)
        {
            Success = false;
        }
        _errors.Add(errorMessage);

    }

    public void AddRangeErrors(ICollection<string> errors)
    {
        foreach (var error in errors)
        {
            AddError(error);
        }
    }
}
