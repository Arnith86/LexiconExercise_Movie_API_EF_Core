// Ignore Spelling: Dto

using AutoMapper;
using MovieCore.DomainContracts;
using MovieCore.Models.DTOs.ReviewDTOs;
using MovieCore.Models.Entities;
using MovieCore.Models.Exceptions;
using MovieCore.Models.Exceptions.BusinessRuleViolationExceptions;
using ServicesContracts.Contracts;

namespace MovieServices.Services;

/// <summary>
/// Provides review-related operations for movies, such as retrieving all reviews associated with 
/// a specific movie. Implements the <see cref="IReviewServices"/> interface.
/// </summary>
public class ReviewServices : IReviewServices
{
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;

	public ReviewServices(IMapper mapper, IUnitOfWork unitOfWork)
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}

	/// <inheritdoc/>
	public async Task<ReviewDto> AddReview(ReviewCreateDto reviewCreateDto)
	{
		VideoMovie? movie =
			await _unitOfWork.Movies.GetMovieAsync(reviewCreateDto.MovieId, changeTracker: true);

		if (movie is null) throw new MovieNotFoundException(reviewCreateDto.MovieId);
		await ValidateReviewBusinessRulesAsync(movie, reviewCreateDto);

		Review review = _mapper.Map<Review>(reviewCreateDto);

		movie.Reviews.Add(review);
		await _unitOfWork.CompleteAsync();

		return _mapper.Map<ReviewDto>(review);
	}

	private async Task ValidateReviewBusinessRulesAsync(VideoMovie movie, ReviewCreateDto reviewCreateDto)
	{
		int maxAllowed = 0;

		if (IsOlderThan20Years(movie)) maxAllowed = 5;
		else maxAllowed = 10;
		
		if (await MoreThenXReviews(movie, maxAllowed))
			throw new MaximumReviewsReachedException(movie.Id, maxAllowed);
	}

	// ToDo: Create automatic algorithm that checks every 10 minutes, if movie is older than 20 years and deletes the oldest reviews while over 5 reviews.
	private bool IsOlderThan20Years(VideoMovie movie) => (DateTime.Today.Year - movie.Year) > 20;


	private async Task<bool> MoreThenXReviews(VideoMovie movie, int maxAllowed) =>
		(await _unitOfWork.Reviews.CountReviewsForMovieAsync(movie.Id)) >= maxAllowed;


	/// <inheritdoc/>
	public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync(int movieId)
	{
		var movieExists = await _unitOfWork.Movies.AnyAsync(movieId);

		if (movieExists == false) throw new MovieNotFoundException(movieId);

		return await _unitOfWork.Reviews.GetAllReviewsForMovieAsync(movieId, changeTracker: false);
	}


	/// <inheritdoc/>
	public async Task<bool> RemoveReviewAsync(int reviewId)
	{
		var review = await _unitOfWork.Reviews.GetReviewAsync(reviewId, changeTraker: true);

		if (review is null) throw new ReviewNotFoundException(reviewId);

		_unitOfWork.Reviews.Remove(review);
		await _unitOfWork.CompleteAsync();

		return true;
	}
}
