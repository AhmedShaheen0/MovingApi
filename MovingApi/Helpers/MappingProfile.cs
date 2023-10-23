using AutoMapper;
using MovingApi.Models;

namespace MovingApi.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDitailsDto>();
            CreateMap<Movie, MovieDto>().ReverseMap()
                .ForMember(src => src.Poster, option => option.Ignore());


        }



    }
}
