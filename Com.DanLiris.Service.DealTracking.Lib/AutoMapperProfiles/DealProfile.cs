using AutoMapper;
using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.DanLiris.Service.DealTracking.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.AutoMapperProfiles
{
    public class DealProfile : Profile
    {
        public DealProfile()
        {
            CreateMap<Deal, DealViewModel>()
                .ForPath(d => d.Company.Id, opt => opt.MapFrom(s => s.CompanyId))
                .ForPath(d => d.Company.Code, opt => opt.MapFrom(s => s.CompanyCode))
                .ForPath(d => d.Company.Name, opt => opt.MapFrom(s => s.CompanyName))
                .ForPath(d => d.Contact.Id, opt => opt.MapFrom(s => s.ContactId))
                .ForPath(d => d.Contact.Code, opt => opt.MapFrom(s => s.ContactCode))
                .ForPath(d => d.Contact.Name, opt => opt.MapFrom(s => s.ContactName))
                .ForPath(d => d.Uom.Unit, opt => opt.MapFrom(s => s.UomUnit))
                .ReverseMap();

            CreateMap<Deal, StageDealViewModel>().ReverseMap();
        }
    }
}
