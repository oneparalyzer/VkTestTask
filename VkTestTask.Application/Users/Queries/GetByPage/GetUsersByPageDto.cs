using VkTestTask.Application.Users.Queries.GetAll;

namespace VkTestTask.Application.Users.Queries.GetByPage;

public class GetUsersByPageDto
{
    public int QuantityPages { get; set; }
    public int CurrentPages { get; set; }
    public IEnumerable<GetAllUsersDto> Users { get; set; }
}
