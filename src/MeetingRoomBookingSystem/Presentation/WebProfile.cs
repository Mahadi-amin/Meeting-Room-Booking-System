using AutoMapper;
using Domain.Entities;
using Presentation.Models;

namespace Presentation
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<MeetingRoomCreateModel, MeetingRoom>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImagePath));

            CreateMap<MeetingRoom, MeetingRoomUpdateModel>()
                .ForMember(dest => dest.ImageFile, opt => opt.Ignore()); 

            CreateMap<MeetingRoomUpdateModel, MeetingRoom>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
