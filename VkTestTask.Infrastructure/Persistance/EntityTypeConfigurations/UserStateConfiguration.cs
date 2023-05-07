using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VkTestTask.Domain.AggregateModels.UserAggregate.Entities;
using VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;

namespace VkTestTask.Infrastructure.Persistance.EntityTypeConfigurations;

public sealed class UserStateConfiguration : IEntityTypeConfiguration<UserState>
{
    public void Configure(EntityTypeBuilder<UserState> builder)
    {
        builder.ToTable("user_states");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasColumnName("id")
               .HasConversion(
                   id => id.Value,
                   value => UserStateId.Create(value));

        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Code).IsRequired();

        builder.HasIndex(x => x.Code).IsUnique();
    }
}
