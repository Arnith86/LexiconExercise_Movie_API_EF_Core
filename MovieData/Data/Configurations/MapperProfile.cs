using AutoMapper;
using MovieCore.Models.DTOs.MovieActorDto;
using MovieCore.Models.DTOs.MovieDtos;
using MovieCore.Models.Entities;

namespace MovieData.Data.Configurations;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
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

	}
}
