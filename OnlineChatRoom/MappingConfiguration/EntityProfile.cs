using AutoMapper;
using OnlineChatRoom.Common.DTO;
using OnlineChatRoom.DataAccess.Models;

namespace OnlineChatRoom.MappingConfiguration
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            EntityDtoMaps();
        }

        private void EntityDtoMaps()
        {
            CreateMap<Log, LogDTO>()
                .ReverseMap();

            CreateMap<Rooms, RoomsDTO>()
                .ReverseMap();

            CreateMap<ChatConnections, ChatConnectionsDTO>()
                .ReverseMap();

            CreateMap<AspNetUsers, AspNetUsersDTO>()
                .ReverseMap();

            CreateMap<AspNetUserClaims, AspNetUserClaimsDTO>()
                .ReverseMap();

            CreateMap<AspNetUserLogins, AspNetUserLoginsDTO>()
                .ReverseMap();

            CreateMap<AspNetUserRoles, AspNetUserRolesDTO>()
                .ReverseMap();

            CreateMap<AspNetUserTokens, AspNetUserTokensDTO>()
                .ReverseMap();

            CreateMap<AspNetRoleClaims, AspNetRoleClaimsDTO>()
                .ReverseMap();

            CreateMap<AspNetRoles, AspNetRolesDTO>()
                .ReverseMap();
        }
    }
}
