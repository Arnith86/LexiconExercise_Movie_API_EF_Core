using AutoMapper;
using MovieApi.Models.DTOs.MovieActorDto;
using MovieApi.Models.DTOs.MovieDtos;
using MovieApi.Models.Entities;

namespace MovieApi.Data.Configurations;

public class MapperProfile: Profile
{
	public MapperProfile()
	{
		CreateMap<Movie, MovieWithGenreDto>()
			.ForMember(dest => dest.MovieGenre, opt => opt.MapFrom(src => src.MoviesGenre!.Genre));
		
		CreateMap<Movie, MovieWithGenreDetailsDto>()
			.ForMember(dest => dest.MovieGenre, opt => opt.MapFrom(src => src.MoviesGenre!.Genre))
			.ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.MoviesDetails!.Synopsis))
			.ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.MoviesDetails!.Language))
			.ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.MoviesDetails!.Budget));

		CreateMap<MovieCreateDto, Movie>();

		CreateMap<Movie, MovieWithGenreIdDto>();

		CreateMap<MovieWithGenreIdUpdateDto, Movie>();

		CreateMap<MovieActorCreateDto, MovieActor>();	

	}
}
