using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;
using VkTestTask.Application.Common.Interfaces;
using VkTestTask.Domain.AggregateModels.Constants;
using VkTestTask.Domain.AggregateModels.UserAggregate;
using VkTestTask.Domain.AggregateModels.UserAggregate.Entities;

namespace VkTestTask.Infrastructure.Persistance;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserState> UserStates { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<UserState>().HasData(new UserState[]
        {
            UserState.Create(UserStateCodeConstants.Active, "Описание статуса пользователя 'Active'."),
            UserState.Create(UserStateCodeConstants.Blocked, "Описание статуса пользователя 'Blocked'."),
        });

        modelBuilder.Entity<UserGroup>().HasData(new UserGroup[]
        {
            UserGroup.Create(UserGroupCodeConstants.Admin, "Описание роли пользователя 'Admin'."),
            UserGroup.Create(UserGroupCodeConstants.User, "Описание роли пользователя 'User'.")
        });
    }
}
