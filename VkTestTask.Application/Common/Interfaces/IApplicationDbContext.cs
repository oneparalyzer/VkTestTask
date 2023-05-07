using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using VkTestTask.Domain.AggregateModels.UserAggregate;
using VkTestTask.Domain.AggregateModels.UserAggregate.Entities;

namespace VkTestTask.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<UserGroup> UserGroups { get; set; }
    DbSet<UserState> UserStates { get; set; }
    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
