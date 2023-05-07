using VKTestTask.Domain.SeedWorks;

namespace VkTestTask.Domain.Common.OperationResults;

public sealed class PageResult<TEntity, TId> : SimpleOperationResult where TEntity : AggregateRoot<TId> where TId : ValueObject
{
    public IEnumerable<TEntity> Entities { get; set; } = new List<TEntity>();
    public int QuantityPages { get; set; }
    public int CurrentPage { get; set; }
}
