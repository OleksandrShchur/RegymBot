using AutoMapper;
using RegymBot.Data.Entities;
using RegymBot.Data.Models;
using System.Linq;

namespace RegymBot.Helpers
{
    public class MapperProfile : Profile
    {
        private readonly string _hostAddress;


        public MapperProfile(string hostAddress)
        {
            _hostAddress = hostAddress;

            CreateMap<UserEntity, UserModel>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(
                    r => r.Role.Role).ToList()))
                .ForMember(dest => dest.ImageUrl, opt => 
                    opt.MapFrom(src => $"{_hostAddress}/avatars/{src.UserGuid}.jpg"));

            CreateMap<UserModel, UserEntity>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore());

            CreateMap<StaticMessageEntity, MessageModel>()
                .ForMember(dest => dest.MessageGuid, opt => opt.MapFrom(src => src.StaticMessageGuid))
                .ForMember(dest => dest.PageName, opt => opt.MapFrom(src => src.Page.Name));

            CreateMap<MessageModel, StaticMessageEntity>()
                .ForMember(dest => dest.StaticMessageGuid, opt => opt.MapFrom(src => src.MessageGuid));
        }
    }
}
