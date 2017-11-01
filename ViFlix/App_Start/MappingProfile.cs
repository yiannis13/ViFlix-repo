using AutoMapper;
using ViFlix.DataAccess.Models;
using ViFlix.Dtos;

namespace ViFlix
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(x => x.Id, opt => opt.Ignore());
        }

    }
}