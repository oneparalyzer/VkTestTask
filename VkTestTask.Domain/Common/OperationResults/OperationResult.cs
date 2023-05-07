

namespace VkTestTask.Domain.Common.OperationResults;

public sealed class OperationResult<TData> : SimpleOperationResult
{
    public TData? Data { get; set; }
}
