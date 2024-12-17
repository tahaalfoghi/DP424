using AutoMapper;
using DP424.Domain.Models;
using DP424.Domain.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP424.Application.Services
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductPostDto>().ReverseMap();
        }
    }
}
