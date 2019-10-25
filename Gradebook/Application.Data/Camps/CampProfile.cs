using AutoMapper;
using System;
using System.Collections.Generic;

namespace Application.Data
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModel>().ReverseMap();

            this.CreateMap<Talk, TalkModel>().ReverseMap()
                .ForMember(t => t.Camp, opt => opt.Ignore())
                .ForMember(t => t.Speaker, opt => opt.Ignore());

            this.CreateMap<Speaker, SpeakerModel>().ReverseMap();

        }
    }
}