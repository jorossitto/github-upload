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

            this.CreateMap<Talk, TalkModel>().ReverseMap();

            this.CreateMap<Speaker, SpeakerModel>().ReverseMap();

        }
    }
}