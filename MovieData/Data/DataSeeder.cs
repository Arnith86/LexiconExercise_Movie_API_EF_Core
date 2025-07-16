using Bogus;
using Microsoft.EntityFrameworkCore;
using MovieCore.Models.Entities;

namespace MovieData.Data;

/// <summary>
/// Responsible for seeding the database with sample data using the Bogus library.
/// Populates tables such as Movies, Actors, MovieGenres, MovieDetails, and Reviews.
/// This is intended for development or testing and runs only if the database is empty.
/// </summary>
internal class DataSeeder
{
	private static Faker _faker = new Faker("sv");
	private static List<string> _languages = new List<string> { "English", "Swedish", "French", "Spanish", "German" };
	private static List<string> _movieRoles = new List<string>
	{
		"Hero", "Villain", "Sidekick", "Mentor", "Detective",
		"Doctor", "Lawyer", "Parent", "Agent", "Soldier"
	};
	private static MovieApiContext _context;

	internal static async Task InitAsync(MovieApiContext context)
	{
		_context = context;

		if (await context.Movies.AnyAsync()) return;

		int nrOfActiveActors = _faker.Random.Int(0, 100);

		// Generate base entities
		IList<Actor> actors = GenerateActors(100);
		await context.Actors.AddRangeAsync(actors);

		IList<MovieGenre> movieGenre = GenerateMovieGenre();

		var movies = await GenerateMoviesAsync(50, movieGenre, actors);
		await context.SaveChangesAsync();


		// Generate and link entities with relations
		var movieActors = AssignActorsToMovie(actors, movies, nrOfActiveActors);
		await context.AddRangeAsync(movieActors);
		var movieDetails = await GenerateMoviesDetailsAsync(movies);


		await context.SaveChangesAsync();
	}

	private static async Task<IEnumerable<MovieDetails>> GenerateMoviesDetailsAsync(IEnumerable<VideoMovie> movies)
	{
		List<MovieDetails> movieDetails = new List<MovieDetails>();

		foreach (var movie in movies)
		{
			movie.MoviesDetails = new MovieDetails
			{
				Movie = movie,          // Establishes a foreignKey relationship
				Synopsis = _faker.Lorem.Sentence(30),
				Language = _faker.PickRandom(_languages),
				Budget = _faker.Random.Int(300000, 5000000)
			};

			await _context.MovieDetails.AddAsync(movie.MoviesDetails);
			movieDetails.Add(movie.MoviesDetails);
		}

		return movieDetails;
	}

	private static IList<Actor> GenerateActors(int numberOfActors)
	{
		var actors = new List<Actor>(numberOfActors);

		for (int i = 0; i < numberOfActors; i++)
		{
			var fName = _faker.Name.FindName();
			var fYear = _faker.Random.Int(1945, 2020);


			actors.Add(new Actor
			{
				Name = fName,
				BirthYear = fYear
			});
		}

		return actors;
	}

	private static async Task<IEnumerable<VideoMovie>> GenerateMoviesAsync(
		int numberOfMovies,
		IList<MovieGenre> movieGenres,
		IList<Actor> actors)
	{
		Random random = new Random();
		var movies = new List<VideoMovie>(numberOfMovies);

		for (int i = 0; i < numberOfMovies; i++)
		{
			var fMovieTitle = "The " + _faker.Company.CompanyName();
			var fYear = _faker.Random.Int(1920, 2030);
			var fDuration = _faker.Random.Int(5, 300);

			int nrOfReviews = random.Next(0, 4);
			int whichGenre = random.Next(0, movieGenres.Count - 1);

			var movie = new VideoMovie()
			{
				Title = fMovieTitle,
				Year = fYear,
				Duration = fDuration,
				Reviews = GenerateReviews(nrOfReviews),
				MoviesGenre = movieGenres[whichGenre]
			};

			await _context.Movies.AddAsync(movie);
			movies.Add(movie);
		}

		return movies;
	}

	private static IList<MovieGenre> GenerateMovieGenre()
	{
		List<string> genreList = new List<string> { "Action", "Comedy", "Drama", "Sci-Fi", "Horror", "Romance" };
		List<MovieGenre> movieGenres = new List<MovieGenre>();


		foreach (string genre in genreList)
		{
			MovieGenre movieGenre = new MovieGenre()
			{
				Genre = genre
			};

			_context.MovieGenres.AddAsync(movieGenre);
			movieGenres.Add(movieGenre);
		}

		return movieGenres;
	}


	private static IEnumerable<MovieActor> AssignActorsToMovie(
		IEnumerable<Actor> actors,
		IEnumerable<VideoMovie> movies,
		int activeActors)
	{
		var actorList = actors.Take(activeActors);

		return movies.SelectMany(movie => actors
			// Randomly picks a set of actors, each with 1 in 8 chance of being selected
			.Where(_ => _faker.Random.Int(1, 8) == 1)
			.Select(actor => new MovieActor
			{
				MovieId = movie.Id,
				ActorId = actor.Id,
				Role = _movieRoles[_faker.Random.Int(0, _movieRoles.Count - 1)]
			})).ToList();

	}

	private static ICollection<Review> GenerateReviews(int nrOfReviews)
	{
		List<Review> reviews = new List<Review>();

		for (int i = 0; i < nrOfReviews; i++)
		{
			reviews.Add(new Review
			{
				ReviewerName = _faker.Name.FindName(),
				Comment = _faker.Lorem.Sentence(10),
				Rating = _faker.Random.Int(1, 5)
			});
		}

		return reviews;
	}
}