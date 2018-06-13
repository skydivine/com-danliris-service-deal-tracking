using AutoMapper;
using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.DanLiris.Service.DealTracking.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.AutoMapperProfiles
{
    public class MasterProfile : Profile
    {
        public MasterProfile()
        {
            CreateMap<Company, CompanyViewModel>().ReverseMap();

            CreateMap<Contact, ContactViewModel>()
                .ForPath(d => d.Company.Id, opt => opt.MapFrom(s => s.CompanyId))
                .ForPath(d => d.Company, opt => opt.MapFrom(s => s.Company))
                .ReverseMap();
            
            CreateMap<Reason, ReasonViewModel>().ReverseMap();
        }
    }
}
