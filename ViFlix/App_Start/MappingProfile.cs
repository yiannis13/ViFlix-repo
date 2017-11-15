using AutoMapper;
using ViFlix.DataAccess.Models;
using ViFlix.Dtos;

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