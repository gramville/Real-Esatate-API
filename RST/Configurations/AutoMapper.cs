using AutoMapper;
using RST.Models.DTOs.AddDTOs;
using RST.Models.DTOs.UpdateDTOs;
using RST.Models.Tables;
using System.Security.Principal;

namespace RST.Configurations
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {

            CreateMap<Agent, AgentDTO>();
            CreateMap<AgentDTO, Agent>();
            CreateMap<Agent, UpdateAgentDTO>();
            CreateMap<UpdateAgentDTO, Agent>()
                  .ForMember(dest => dest.FirstName, opt => opt.Condition((src, dest) => src.FirstName != null))
                    .ForMember(dest => dest.LastName, opt => opt.Condition((src, dest) => src.LastName != null))
                      .ForMember(dest => dest.PhoneNumber, opt => opt.Condition((src, dest) => src.PhoneNumber != null))
                        .ForMember(dest => dest.Password, opt => opt.Condition((src, dest) => src.Password != null));


            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, UpdateCustomerDTO>();
            CreateMap<UpdateCustomerDTO, Customer>()
                  .ForMember(dest => dest.FirstName, opt => opt.Condition((src, dest) => src.FirstName != null))
                    .ForMember(dest => dest.LastName, opt => opt.Condition((src, dest) => src.LastName != null))
                      .ForMember(dest => dest.PhoneNumber, opt => opt.Condition((src, dest) => src.PhoneNumber != null))
                        .ForMember(dest => dest.Password, opt => opt.Condition((src, dest) => src.Password != null));


            CreateMap<Apartment, ApartmentDTO>();
            CreateMap<ApartmentDTO, Apartment>();
            CreateMap<Apartment, UpdateApartmentDTO>();
            CreateMap<UpdateApartmentDTO, Apartment>()
                    .ForMember(dest => dest.Location, opt => opt.Condition((src, dest) => src.Location != null))
                    .ForMember(dest => dest.Area, opt => opt.Condition((src, dest) => src.Area != null))
                    .ForMember(dest => dest.Image1, opt => opt.Condition((src, dest) => src.Image1 != null))
                    .ForMember(dest => dest.Image2, opt => opt.Condition((src, dest) => src.Image2 != null))
                    .ForMember(dest => dest.Image3, opt => opt.Condition((src, dest) => src.Image3 != null))
                    .ForMember(dest => dest.Image4, opt => opt.Condition((src, dest) => src.Image4 != null))
                    .ForMember(dest => dest.AgentId, opt => opt.Condition((src, dest) => src.AgentId != null))
                    .ForMember(dest => dest.Price, opt => opt.Condition((src, dest) => src.Price != null));

            CreateMap<SoldApartments, SoldApartmentsDTO>();
            CreateMap<SoldApartmentsDTO, SoldApartments>();
            CreateMap<SoldApartments, UpdateSoldApartmentsDTO>();
            //CreateMap<UpdateApartmentDTO, Apartment>()
            //        .ForMember(dest => dest.Location, opt => opt.Condition((src, dest) => src.Location != null))
            //        .ForMember(dest => dest.Area, opt => opt.Condition((src, dest) => src.Area != null))
            //        .ForMember(dest => dest.Image1, opt => opt.Condition((src, dest) => src.Image1 != null))
            //        .ForMember(dest => dest.Image2, opt => opt.Condition((src, dest) => src.Image2 != null));


        }
    }
}
