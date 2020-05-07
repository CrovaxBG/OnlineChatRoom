using AutoMapper;
using OnlineChatRoom.Common.DTOs;
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
        }
    }
}
