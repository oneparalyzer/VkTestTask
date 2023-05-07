

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VkTestTask.Domain.AggregateModels.UserAggregate.Entities;
using VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;

namespace VkTestTask.Infrastructure.Persistance.EntityTypeConfigurations;

public sealed class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder.ToTable("user_groups");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasColumnName("id")
               .HasConversion(
                   id => id.Value,
                   value => UserGroupId.Create(value));

        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Code).IsRequired();

        builder.HasIndex(x => x.Code).IsUnique();
    }
}
