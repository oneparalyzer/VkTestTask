using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VkTestTask.Domain.AggregateModels.UserAggregate;
using VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;

namespace VkTestTask.Infrastructure.Persistance.EntityTypeConfigurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .ValueGeneratedNever()
               .HasColumnName("id")
               .HasConversion(
                   id => id.Value,
                   value => UserId.Create(value));

        builder.Property(x => x.UserGroupId)
                   .ValueGeneratedNever()
                   .HasColumnName("user_group_id")
                   .HasConversion(
                       id => id.Value,
                       value => UserGroupId.Create(value));

        builder.Property(x => x.UserStateId)
                   .ValueGeneratedNever()
                   .HasColumnName("user_state_id")
                   .HasConversion(
                       id => id.Value,
                       value => UserStateId.Create(value));

        builder.HasOne(x => x.UserGroup).WithMany().HasForeignKey(x => x.UserGroupId);
        builder.HasOne(x => x.UserState).WithMany().HasForeignKey(x => x.UserStateId);

        builder.OwnsOne(x => x.Password).Property(x => x.Value)
            .HasColumnName("password")
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .IsRequired()
            .HasColumnName("created_date");

        builder.Property(x => x.Login)
            .IsRequired()
            .HasColumnName("login");

        builder.HasIndex(x => x.Login).IsUnique();

    }
}
