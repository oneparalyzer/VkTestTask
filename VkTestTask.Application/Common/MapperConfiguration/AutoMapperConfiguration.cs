using AutoMapper;
using VkTestTask.Application.Users.Queries.GetAll;
using VkTestTask.Application.Users.Queries.GetById;
using VkTestTask.Domain.AggregateModels.UserAggregate;

namespace VkTestTask.Application.Common.MapperConfiguration;

public sealed class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<User, GetAllUsersDto>()
            .ForPath(x => x.UserId, opt => opt.MapFrom(src => src.Id.Value))
            .ForPath(x => x.UserGroupId, opt => opt.MapFrom(src => src.UserGroup.Id.Value))
            .ForPath(x => x.UserStateId, opt => opt.MapFrom(src => src.UserState.Id.Value))
            .ForPath(x => x.UserGroupCode, opt => opt.MapFrom(src => src.UserGroup.Code))
            .ForPath(x => x.UserGroupDescription, opt => opt.MapFrom(src => src.UserGroup.Description))
            .ForPath(x => x.UserStateCode, opt => opt.MapFrom(src => src.UserState.Code))
            .ForPath(x => x.UserStateDescription, opt => opt.MapFrom(src => src.UserState.Description));

        CreateMap<User, GetUserByIdDto>()
            .ForPath(x => x.UserId, opt => opt.MapFrom(src => src.Id.Value))
            .ForPath(x => x.UserGroupId, opt => opt.MapFrom(src => src.UserGroup.Id.Value))
            .ForPath(x => x.UserStateId, opt => opt.MapFrom(src => src.UserState.Id.Value))
            .ForPath(x => x.UserGroupCode, opt => opt.MapFrom(src => src.UserGroup.Code))
            .ForPath(x => x.UserGroupDescription, opt => opt.MapFrom(src => src.UserGroup.Description))
            .ForPath(x => x.UserStateCode, opt => opt.MapFrom(src => src.UserState.Code))
            .ForPath(x => x.UserStateDescription, opt => opt.MapFrom(src => src.UserState.Description));
    }
}
