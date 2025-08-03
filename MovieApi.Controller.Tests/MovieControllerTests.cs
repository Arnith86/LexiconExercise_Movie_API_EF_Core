using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApi.Controller.Tests.DatabaseFixtures;
using MovieApi.Controllers;
using MovieCore.Models.DTOs.MovieDtos;
using MovieCore.Models.Entities;
using MovieCore.Models.Exceptions;
using Services.Contracts;

namespace MovieApi.Controller.Tests
{
	public class MovieControllerTests
	{
		private MoviesController _sut;
		private Mock<IServiceManager> _mockServiceManager;
		private MovieDatabaseFixture _movieDatabaseFixture;

		private const int _c_MoviesCreateDefault_10 = 10;
		private const int _c_MovieCreate_SingleMovie_1 = 1;

		private const int _c_MovieId_1 = 1;

		public MovieControllerTests()
		{
			_mockServiceManager = new Mock<IServiceManager>();
			_sut = new MoviesController(_mockServiceManager!.Object);
			_movieDatabaseFixture = new MovieDatabaseFixture();
		}

		[Fact]
		public async Task GetMovie_ShouldReturn_StatusCode200OkAsync_MovieWithGenreDto()
		{
			// Arrange 
			var response = await GetMovieWithGenre();

			_mockServiceManager
				.Setup(sm => sm.MovieServices.GetMovieAsync(_c_MovieCreate_SingleMovie_1))
				.ReturnsAsync(response);

			// Act 
			var result = await _sut.GetMovie(_c_MovieId_1);
			var resultType = result.Result as OkObjectResult;

			// Assert 
			Assert.IsType<OkObjectResult>(resultType);
			Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
			Assert.Equal(response, resultType.Value);
		}


		/// <summary>
		/// This test ensures that the controller correctly propagates the exception from the service layer
		/// when an invalid movie ID is provided.
		/// </summary>
		[Fact]
		public async Task GetMovie_ShouldReturn_StatusThrow_MovieNotFoundException()
		{
			// Arrange
			_mockServiceManager
				.Setup(sm => sm.MovieServices.GetMovieAsync(It.IsAny<int>()))
				.ThrowsAsync(new MovieNotFoundException(_c_MovieId_1));

			// Act & Assert
			await Assert.ThrowsAsync<MovieNotFoundException>(() => _sut.GetMovie(_c_MovieId_1));
		}

			_mockPaginationMetaDate.SetupGet(pmd => pmd.hasNext).Returns(true);
		}

		private async Task<MovieWithGenreDto> GetMovieWithGenre()
		{
			var movies = await _movieDatabaseFixture.GetMovieWithGenre();
			return movies.First();
		}


	}
}
