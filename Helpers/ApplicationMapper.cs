using AutoMapper;
using MyAPINetCore6.Models;
using System.Diagnostics.Eventing.Reader;

namespace MyAPINetCore6.Helpers
{
    public class ApplicationMapper: Profile
    {
       public ApplicationMapper()
        {
            CreateMap<MyHero,  HeroModel>().ReverseMap();
        }
    }
}
