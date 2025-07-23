using AutoMapper;
using MovieCore.Models.DTOs.ActorDTOs;
using MovieCore.Models.DTOs.MovieActorDto;
using MovieCore.Models.DTOs.MovieDtos;
using MovieCore.Models.Entities;

namespace MovieData.Data.Configurations;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		// ToDo: separate profiles into controller specific files 
		// Movie Profiles
		CreateMap<VideoMovie, MovieBaseDto>();
		CreateMap<VideoMovie, MovieWithGenreDto>()
			.ForMember(dest => dest.MovieGenre, opt => opt.MapFrom(src => src.MoviesGenre!.Genre));

		CreateMap<VideoMovie, MovieWithGenreDetailsDto>()
			.ForMember(dest => dest.MovieGenre, opt => opt.MapFrom(src => src.MoviesGenre!.Genre))
			.ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.MoviesDetails!.Synopsis))
			.ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.MoviesDetails!.Language))
			.ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.MoviesDetails!.Budget));

		CreateMap<MovieCreateDto, VideoMovie>();

		CreateMap<VideoMovie, MovieWithGenreIdDto>();

		CreateMap<MovieWithGenreIdUpdateDto, VideoMovie>();

		CreateMap<MovieActorCreateDto, MovieActor>();


		// Actor profiles
		CreateMap<Actor, ActorDto>()
			.ForMember(dest => dest.VideoMovies, opt => opt.MapFrom(src => 
				src.MovieActors!.Select(ma => ma.Movie).ToList()));
		CreateMap<ActorCreateDto, Actor>();


	}
}
