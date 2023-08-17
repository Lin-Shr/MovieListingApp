using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MovieApi.DTO;
using MovieApp.Areas.Identity.Data;

namespace MovieApi.AutoMapper
{
    public class AutoMapper:Profile
    {
      public AutoMapper() 
        {
            CreateMap<Movie, MovieDTO>();
            CreateMap<MovieDTO, Movie>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreDTO, Genre>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<ReviewDTO, Review>();
            CreateMap<Movie, GenreDisplayDTO>();
            CreateMap<GenreDisplayDTO, Movie>();
            CreateMap<MovieAppUser, UserDTO>();
            CreateMap<UserDTO, MovieAppUser>();
            CreateMap<RegisterDTO, MovieAppUser>();
            CreateMap<MovieAppUser, RegisterDTO>();
        }   
    }
}
