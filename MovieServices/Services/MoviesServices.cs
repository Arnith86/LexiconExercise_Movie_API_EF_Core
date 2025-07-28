// Ignore Spelling: Dto

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieCore.DomainContracts.RequestParameters;
using MovieCore.Models.DTOs.MovieDtos;
using MovieCore.Models.Entities;
using MovieCore.Models.Exceptions;
using Services.Contracts.Contracts;

namespace MovieServices.Services;

/// <summary>
/// Implements movie-related business logic and coordinates data access operations between the controller layer
/// and the underlying repositories. Handles mapping between entity models and DTOs using AutoMapper,
/// and ensures validation and existence checks for movie and genre operations.
/// </summary>
public class MoviesServices : IMoviesServices
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public MoviesServices(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}
	
	/// <inheritdoc/>
	public async Task<(IEnumerable<MovieWithGenreDto> moviesWithGenreDto, IPaginationMetaData metaData)> GetAllMoviesAsync(
		MovieRequestParameters requestParameters, 
		bool trackChanges)
	{
		var moviesWithMetaData = await _unitOfWork.Movies.GetAllMoviesAsync(requestParameters, trackChanges);
		var moviesWithGenreDtos = _mapper.Map<IEnumerable<MovieWithGenreDto>>(moviesWithMetaData.Items);

		return (moviesWithGenreDtos, moviesWithMetaData.MetaData);
	}

	/// <inheritdoc/>
	public async Task<MovieWithGenreDto> GetMovieAsync(int id)
	{
		var movie = await _unitOfWork.Movies.GetMovieAsync(id, changeTracker: false);

		if (movie is null) throw new MovieNotFoundException(id);

		return _mapper.Map<MovieWithGenreDto>(movie);
	}

	/// <inheritdoc/>
	public async Task<MovieWithGenreDetailsDto> GetMovieDetailsAsync(int id)
	{
		var movieWithGenreDetailsDto = _mapper.Map<MovieWithGenreDetailsDto>(
			await _unitOfWork.Movies.GetMovieDetailsAsync(id, changeTracker: false)
		);

		if (movieWithGenreDetailsDto is null) throw new MovieNotFoundException(id);

		return movieWithGenreDetailsDto;
	}

	/// <inheritdoc/>
	public async Task<MovieDetailDto?> GetMovieFullDetailsAsync(int id)
	{
		var movieExists = await _unitOfWork.Movies.AnyAsync(id);

		if (!movieExists) throw new MovieNotFoundException(id);

		return await _unitOfWork.Movies.GetMovieFullDetailsAsync(id, changeTracker: false);
	}

	/// <inheritdoc/>
	public async Task<(MovieWithGenreIdDto mwgiDto, int movieId)> AddMovieAsync(MovieCreateDto movieCreateDto)
	{
		if (movieCreateDto.MovieGenreId == 0) throw new MovieGenreInvalidArgumentException();
		
		var genre = await _unitOfWork.MovieGenres.AnyAsync(movieCreateDto.MovieGenreId);

		if (!genre) throw new MovieGenreNotFoundException(movieCreateDto.MovieGenreId);

		VideoMovie movie = _mapper.Map<VideoMovie>(movieCreateDto);

		_unitOfWork.Movies.Add(movie);
		await _unitOfWork.CompleteAsync();

		return (_mapper.Map<MovieWithGenreIdDto>(movie), movie.Id);
	}

	/// <inheritdoc/>
	public async Task<bool> UpdateMovieAsync(int id, MovieWithGenreIdUpdateDto movieWithGenreIdUpdateDto)
	{
		var movie = await _unitOfWork.Movies.GetMovieAsync(id, changeTracker: true);

		if (movie is null) throw new MovieNotFoundException(id);

		var genre = await _unitOfWork.MovieGenres.AnyAsync(movieWithGenreIdUpdateDto.MovieGenreId);

		if (!genre) throw new MovieGenreNotFoundException(movieWithGenreIdUpdateDto.MovieGenreId);

		_mapper.Map(movieWithGenreIdUpdateDto, movie);

		try
		{
			await _unitOfWork.CompleteAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!await _unitOfWork.Movies.AnyAsync(id)) throw new MovieNotFoundException(id);
			else throw;
		}

		return true;
	}

	/// <inheritdoc/>
	public async Task<bool> RemoveMovieAsync(int id)
	{
		var movie = await _unitOfWork.Movies.GetMovieAsync(id);

		if (movie is null) throw new MovieNotFoundException(id);

		_unitOfWork.Movies.Remove(movie);
		await _unitOfWork.CompleteAsync();

		return true;
	}

}
