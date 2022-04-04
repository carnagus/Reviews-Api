using Application.Models;
using Application.Reviews.Commands.CreateReview;
using Application.Reviews.Queries.GetReviewsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ReviewsController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetReviews([FromQuery] PagingParams parameters)
        {
            return HandlePagedResult(await Mediator!.Send(new GetReviewsWithPaginationQuery() { Params = parameters }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewCommand command)
        {
            return HandleResult(await Mediator!.Send(command));
        }
    }
}