using AutoMapper;
using System;
using System.Collections.Generic;

namespace Application.Data
{
    public class CampProfile : Profile
    {
        public CampProfile()
        {
            this.CreateMap<Camp, CampModel>();
        }
    }
}