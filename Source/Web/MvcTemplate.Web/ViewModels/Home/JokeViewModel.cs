using System;
using AutoMapper;
using MvcTemplate.Data.Models;
using MvcTemplate.Web.Infrastructure.Mapping;

namespace MvcTemplate.Web.ViewModels.Home
{
    public class JokeViewModel : IMapFrom<Joke>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string Category { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Joke, JokeViewModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));
        }
    }
}
