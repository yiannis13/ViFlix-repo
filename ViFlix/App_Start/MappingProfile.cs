﻿using AutoMapper;
using Common.Models.Domain;
using Common.Models.Dtos;

namespace ViFlix
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>().ForMember(x => x.Id, opt => opt.Ignore());
        }

    }
}