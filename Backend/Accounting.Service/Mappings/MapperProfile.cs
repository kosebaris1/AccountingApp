using Accounting.Core.DTOs;
using Accounting.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service.Mappings
{
    public class MapperProfile : Profile
    {
        protected MapperProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<GroupInRole, GroupInRole  >().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Sale, SaleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
