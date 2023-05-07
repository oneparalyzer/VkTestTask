

namespace VkTestTask.Application.Users.Queries.GetAll;

public class GetAllUsersDto
{
    public Guid UserId { get; set; }
    public string Login { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public Guid UserStateId { get; set; }
    public string UserStateCode { get; set; } = null!;
    public string UserStateDescription { get; set; } = null!;
    public Guid UserGroupId { get; set; }
    public string UserGroupCode { get; set; } = null!;
    public string UserGroupDescription { get; set; } = null!;
}
