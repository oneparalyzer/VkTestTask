using VKTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;
using VkTestTask.Domain.AggregateModels.UserAggregate.ValueObjects;
using VKTestTask.Domain.SeedWorks;
using VkTestTask.Domain.AggregateModels.UserAggregate.Entities;
using VkTestTask.Domain.AggregateModels.Constants;
using VkTestTask.Domain.Common.OperationResults;

namespace VkTestTask.Domain.AggregateModels.UserAggregate;

public sealed class User : AggregateRoot<UserId>
{
    private User(UserId id, string login, Password password, UserState userState, UserGroup userGroup) : base(id)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            throw new ArgumentNullException(nameof(login));
        }
        Login = login;
        Password = password ?? throw new ArgumentNullException(nameof(password));
        UserState = userState ?? throw new ArgumentNullException(nameof(userState));
        UserGroup = userGroup ?? throw new ArgumentNullException(nameof(UserGroup));
        UserGroupId = userGroup.Id;
        UserStateId = userState.Id;
    }

    private User(UserId id) : base(id) { }

    public string Login { get; private set; }
    public Password Password { get; private set; }
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public UserStateId UserStateId { get; private set; }
    public UserGroupId UserGroupId { get; private set; }
    public UserState UserState { get; private set; }
    public UserGroup UserGroup { get; private set; }

    public static User Create(string login, Password password, UserState userState, UserGroup userGroup)
    {
        return new User(UserId.Create(), login, password, userState, userGroup);
    }

    public void Update(string newLogin, Password newPassword, UserState newUserState, UserGroup newUserGroup)
    {
        Login = newLogin ?? throw new ArgumentNullException(nameof(newLogin));
        Password = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
        UserState = newUserState ?? throw new ArgumentNullException(nameof(newUserState));
        UserGroup = newUserGroup ?? throw new ArgumentNullException(nameof(newUserState));
        UserGroupId = newUserGroup.Id;
        UserStateId = newUserState.Id;
    }

    public bool IsActive()
    {
        return UserState.Code == UserStateCodeConstants.Active;
    }
}
