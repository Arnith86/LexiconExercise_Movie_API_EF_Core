
using AutoMapper;
using Bogus;
using MovieApi.Controller.Tests.Helpers;
using MovieCore.Models.DTOs.MovieDtos;
using MovieCore.Models.Entities;

namespace MovieApi.Controller.Tests.DatabaseFixtures;

public class MovieDatabaseFixture
{
	private readonly IMapper _mapper;
	private static Faker _faker = new Faker("sv");
	private static List<string> _languages = new List<string> { "English", "Swedish", "French", "Spanish", "German" };
	private static List<string> _movieRoles = new List<string>
	{
		"Hero", "Villain", "Sidekick", "Mentor", "Detective",
		"Doctor", "Lawyer", "Parent", "Agent", "Soldier"
	};
	
	public MovieDatabaseFixture()
	{
		_mapper = MapperFactory.Create();
	}


	public Task<IEnumerable<MovieWithGenreDto>> GetMovieWithGenre(int nrOfActors = 5, int nrOfMovies = 1)
	{
		IList<MovieGenre> movieGenre = GenerateMovieGenre();
		var movies = GenerateMoviesAsync(nrOfMovies, movieGenre);
		var dtoList = _mapper.Map<IEnumerable<MovieWithGenreDto>>(movies);

		return Task.FromResult(dtoList);
	}

	private IEnumerable<VideoMovie> GenerateMoviesAsync(
	int numberOfMovies,
	IList<MovieGenre> movieGenres)
	{
		Random random = new Random();
		var movies = new List<VideoMovie>(numberOfMovies);

		for (int i = 0; i < numberOfMovies; i++)
		{
			var fMovieTitle = "The " + _faker.Company.CompanyName();
			var fYear = _faker.Random.Int(1920, 2030);
			var fDuration = _faker.Random.Int(5, 300);

			int nrOfReviews = random.Next(0, 4);
			int whichGenre = random.Next(0, movieGenres.Count);

			var movie = new VideoMovie()
			{
				Title = fMovieTitle,
				Year = fYear,
				Duration = fDuration,
				Reviews = GenerateReviews(nrOfReviews),
				MoviesGenre = movieGenres[whichGenre]
			};

			movies.Add(movie);
		}

		return movies;
	}

	private IList<MovieGenre> GenerateMovieGenre()
	{
		List<string> genreList = new List<string> { "Action", "Comedy", "Drama", "Sci-Fi", "Horror", "Romance", "Documentary" };
		List<MovieGenre> movieGenres = new List<MovieGenre>();

		foreach (string genre in genreList)
		{
			MovieGenre movieGenre = new MovieGenre()
			{
				Genre = genre
			};

			movieGenres.Add(movieGenre);
		}

		return movieGenres;
	}

