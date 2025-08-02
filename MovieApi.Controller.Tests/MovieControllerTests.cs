using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApi.Controller.Tests.DatabaseFixtures;
using MovieApi.Controllers;
using MovieCore.Models.DTOs.MovieDtos;
using MovieCore.Models.Entities;
using Services.Contracts;

namespace MovieApi.Controller.Tests
{
	public class MovieControllerTests
	{
		private Mock<IServiceManager> _mockServiceManager;
		private MoviesController _controller;
		private MovieDatabaseFixture _movieDatabaseFixture;

		public MovieControllerTests()
		{
			_mockServiceManager = new Mock<IServiceManager>();
			_controller = new MoviesController(_mockServiceManager.Object);
			_movieDatabaseFixture = new MovieDatabaseFixture();
		}

		[Fact]
		public async Task GetMovie_ShouldReturn_StatusCode200OkAsync()
		{
			// Arrange 
			var response = await GetMovieWithGenre();

			_mockServiceManager
				.Setup(sm => sm.MovieServices.GetMovieAsync(1))
				.ReturnsAsync(response);

			// Act 
			var result = await _controller.GetMovie(1);
			var okResult = result.Result as OkObjectResult;

			// Assert 
			Assert.IsType<OkObjectResult>(okResult);
			Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
			Assert.Equal(response, okResult.Value);
		}

		private async Task<MovieWithGenreDto> GetMovieWithGenre()
		{
			var movies = await _movieDatabaseFixture.GetMovieWithGenre();

			return movies.First();
		}


	}
}
