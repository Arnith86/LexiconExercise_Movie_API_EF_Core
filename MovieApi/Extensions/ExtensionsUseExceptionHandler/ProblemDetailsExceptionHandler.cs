using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MovieCore.Models.Exceptions;
using MovieCore.Models.Exceptions.BusinessRuleViolationExceptions;

namespace MovieApi.Extensions.ExtensionsUseExceptionHandler;

/// <summary>
/// Provides an extension method for configuring centralized exception handling using ProblemDetails.
/// </summary>
public static class ProblemDetailsExceptionHandler
{
	/// <summary>
	/// Configures a global exception handler middleware that returns ProblemDetails responses
	/// for specific domain exceptions (e.g., MovieNotFoundException, MovieGenreNotFoundException, ActorNotFoundException)
	/// and general unhandled exceptions.
	/// </summary>
	/// <param name="app">The WebApplication to configure.</param>
	/// <remarks>
	/// The middleware intercepts unhandled exceptions, maps them to appropriate HTTP status codes,
	/// and returns a standardized ProblemDetails response for API consumers.
	/// </remarks>
	public static void ConfigureExceptionHandler(this WebApplication app)
	{
		app.UseExceptionHandler(builder =>
		{
			builder.Run(async context =>
			{
				var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

				if (contextFeature != null)
				{
					var problemDetailsFactory = app.Services.GetRequiredService<ProblemDetailsFactory>();

					ProblemDetails problemDetails;
					int statusCode;

					switch (contextFeature.Error)
					{
						case MovieNotFoundException movieNotFoundException: // Movie Not Found
							statusCode = StatusCodes.Status404NotFound;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: movieNotFoundException.Title,
								detail: movieNotFoundException.Message,
								instance: context.Request.Path
							);
							break;
						case MovieGenreNotFoundException movieGenreNotFoundException: // Movie Genre Not Found
							statusCode = StatusCodes.Status404NotFound;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: movieGenreNotFoundException.Title,
								detail: movieGenreNotFoundException.Message,
								instance: context.Request.Path
							);
							break;
						case ActorNotFoundException actorNotFoundException: // Actor Not Found
							statusCode = StatusCodes.Status404NotFound;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: actorNotFoundException.Title,
								detail: actorNotFoundException.Message,
								instance: context.Request.Path
							);
							break;
						case ReviewNotFoundException reviewNotFoundException: // Review Not Found
							statusCode = StatusCodes.Status404NotFound;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: reviewNotFoundException.Title,
								detail: reviewNotFoundException.Message,
								instance: context.Request.Path
							);
							break;
						case MovieDetailsNotLinkedException movieDetailsNotLinkedException: // No movieDetails linked to movie
							statusCode = StatusCodes.Status400BadRequest;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: movieDetailsNotLinkedException.Title,
								detail: movieDetailsNotLinkedException.Message,
								instance: context.Request.Path
							);
							break;
						case PaginationArgumentOutOfRangeException argumentOutOfRangeException: // Paging parameters out of range
							statusCode = StatusCodes.Status400BadRequest;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: argumentOutOfRangeException.Title,
								detail: argumentOutOfRangeException.Message,
								instance: context.Request.Path
							);
							break;
						case MaximumReviewsReachedException maximumReviewsReachedException: // Maximum reviews reached
							statusCode = StatusCodes.Status400BadRequest;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: maximumReviewsReachedException.Title,
								detail: maximumReviewsReachedException.Message,
								instance: context.Request.Path
							);
							break;
						case DuplicateActorAssignmentException duplicateActorAssignmentException: // Duplicate actor assignment
							statusCode = StatusCodes.Status400BadRequest;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: duplicateActorAssignmentException.Title,
								detail: duplicateActorAssignmentException.Message,
								instance: context.Request.Path
							);
							break;
						case MovieGenreInvalidArgumentException movieGenreInvalidArgumentException: // MovieGenre not assigned on movie creation.
							statusCode = StatusCodes.Status400BadRequest;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: movieGenreInvalidArgumentException.Title,
								detail: movieGenreInvalidArgumentException.Message,
								instance: context.Request.Path
							);
							break;
						case DuplicateMovieDetailsAssignmentException 
							 duplicateMovieDetailsAssignmentException: // Duplicate movieDetails assignment
							statusCode = StatusCodes.Status400BadRequest;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: duplicateMovieDetailsAssignmentException.Title,
								detail: duplicateMovieDetailsAssignmentException.Message,
								instance: context.Request.Path
							);
							break;
						case DuplicateMovieTitleArgumentException
							 duplicateMovieTitleArgumentException: // Duplicate of movie title found
							statusCode = StatusCodes.Status400BadRequest;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: duplicateMovieTitleArgumentException.Title,
								detail: duplicateMovieTitleArgumentException.Message,
								instance: context.Request.Path
							);
							break;
						case MaximumActorsReachedException
							 maximumActorsReachedException: // Documentary 10 actor limit exceeded 
							statusCode = StatusCodes.Status400BadRequest;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: maximumActorsReachedException.Title,
								detail: maximumActorsReachedException.Message,
								instance: context.Request.Path
							);
							break;
						case MovieDetailsBusinessRuleException
							 movieDetailsBusinessRuleException: // Documentary 1 million in budget exceeded 
							statusCode = StatusCodes.Status400BadRequest;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: movieDetailsBusinessRuleException.Title,
								detail: movieDetailsBusinessRuleException.Message,
								instance: context.Request.Path
							);
							break;
						case MissingPatchDocumentException
							 missingPatchDocumentException: // Patch Documentation = null
							statusCode = StatusCodes.Status400BadRequest;
							problemDetails = problemDetailsFactory.CreateProblemDetails(
								context,
								statusCode,
								title: missingPatchDocumentException.Title,
								detail: missingPatchDocumentException.Message,
								instance: context.Request.Path
							);
							break;
						default:
							statusCode = StatusCodes.Status500InternalServerError;  // General server error
							problemDetails = problemDetailsFactory.CreateProblemDetails(
									context,
									statusCode,
									title: "Internal Server Error",
									detail: contextFeature.Error.Message,
									instance: context.Request.Path);
							break;
					}

					context.Response.StatusCode = statusCode;
					await context.Response.WriteAsJsonAsync(problemDetails);
				}
			});
		});
	}
}
