// Ignore Spelling: Dto

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieApi.Controller.Tests.DatabaseFixtures;
using MovieApi.Controller.Tests.Extension;
using MovieApi.Controllers;
using MovieCore.DomainContracts.RequestParameters;
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
		private MovieRequestParameters _movieRequestParameter;
		private MovieDatabaseFixture _movieDatabaseFixture;
		private Mock<IPageList<VideoMovie>> _mockPageList;
		private Mock<IPaginationMetaData> _mockPaginationMetaDate;

		private const int _c_MoviesCreateDefault_10 = 10;
		private const int _c_MovieCreate_SingleMovie_1 = 1;

		private const int _c_MovieId_1 = 1;

		public MovieControllerTests()
		{
			_mockServiceManager = new Mock<IServiceManager>();
			_sut = new MoviesController(_mockServiceManager!.Object);
			_movieRequestParameter = new MovieRequestParameters();
			_movieDatabaseFixture = new MovieDatabaseFixture();
			_mockPageList = new Mock<IPageList<VideoMovie>>();
			_mockPaginationMetaDate = new Mock<IPaginationMetaData>();
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
		public async Task GetMovie_ShouldReturn_StatusThrow_StatusCode404_MovieNotFoundException()
		{
			// Arrange
			_mockServiceManager
				.Setup(sm => sm.MovieServices.GetMovieAsync(It.IsAny<int>()))
				.ThrowsAsync(new MovieNotFoundException(_c_MovieId_1));

			// Act & Assert
			await Assert.ThrowsAsync<MovieNotFoundException>(() => _sut.GetMovie(_c_MovieId_1));
		}

		/// <summary>
		/// This test verifies that when a valid movie request with pagination parameters is made,
		/// the controller returns a 200 OK response containing the expected paginated list of 
		/// <see cref="MovieWithGenreDto"/> items and sets the "X-Pagination" response header.
		/// </summary>
		[Fact]
		public async Task GetMovies_ShouldReturn_StatusCode200OkAsync_PaginatedIEnumerableOf_MovieWithGenreDto()
		{
			// Arrange 
			var moviesWithGenre = await GetMoviesWithGenre();

			_mockPaginationMetaDate.SetupGet(pmd => pmd.CurrentPage).Returns(1);
			_mockPaginationMetaDate.SetupGet(pmd => pmd.PageSize).Returns(5);
			_mockPaginationMetaDate.SetupGet(pmd => pmd.TotalItemCount).Returns(_c_MoviesCreateDefault_10);
			_mockPaginationMetaDate.SetupGet(pmd => pmd.TotalPages).Returns(2);
			_mockPaginationMetaDate.SetupGet(pmd => pmd.hasPrevious).Returns(false);
			_mockPaginationMetaDate.SetupGet(pmd => pmd.hasNext).Returns(true);

			_movieRequestParameter.PageNumber = 1;
			_movieRequestParameter.PageSize = 5;

			_mockServiceManager
				.Setup(sm => sm.MovieServices.GetAllMoviesAsync(_movieRequestParameter, false))
				.ReturnsAsync((moviesWithGenre, _mockPaginationMetaDate.Object));

			_sut.SetupDefaultHttpContext();

			// Act 
			var result = await _sut.GetMovies(_movieRequestParameter);
			var resultType = result.Result as OkObjectResult;

			// Assert 
			Assert.True(_sut.HttpContext.Response.Headers.ContainsKey("X-Pagination"));
			var headerValue = _sut.HttpContext.Response.Headers["X-Pagination"].ToString();
			Assert.False(string.IsNullOrEmpty(headerValue));
			Assert.Contains("\"CurrentPage\":1", headerValue);
			Assert.Contains("\"PageSize\":5", headerValue);

			Assert.IsType<OkObjectResult>(resultType);
			Assert.Equal(StatusCodes.Status200OK, resultType.StatusCode);
			Assert.Equal(moviesWithGenre, resultType.Value);
		}

		/// <summary>
		/// Ensures that the controller correctly propagates the PaginationArgumentOutOfRangeException
		/// when invalid pagination parameters are passed to the service layer.
		/// </summary>
		[Fact]
		public async Task GetMovies_ShouldThrow_StatusCode400_PaginationArgumentOutOfRangeException()
		{
			// Arrange 
				_mockServiceManager
				.SetupSequence(sm => sm.MovieServices.GetAllMoviesAsync(_movieRequestParameter, false))
				.ThrowsAsync(new PaginationArgumentOutOfRangeException(It.IsAny<string>()));

			// Act & Assert 
			var exception = await Assert.ThrowsAsync<PaginationArgumentOutOfRangeException>(() =>
				_sut.GetMovies(_movieRequestParameter));
		}

		private async Task<MovieWithGenreDto> GetMovieWithGenre()
		{
			var movies = await _movieDatabaseFixture.GetMovieWithGenre();
			return movies.First();
		}

		private async Task<IEnumerable<MovieWithGenreDto>> GetMoviesWithGenre() =>
			await _movieDatabaseFixture.GetMovieWithGenre(10);
	}
}
