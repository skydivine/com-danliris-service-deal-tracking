using AutoMapper;
using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.DanLiris.Service.DealTracking.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.AutoMapperProfiles
{
    public class StageProfile : Profile
    {
        public StageProfile()
        {
            CreateMap<Stage, StageViewModel>()
                .ReverseMap();
        }
    }
}
