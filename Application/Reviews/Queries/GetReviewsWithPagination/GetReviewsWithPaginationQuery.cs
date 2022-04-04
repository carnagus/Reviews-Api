using Application.Interfaces;
using Application.Models;
using Domain.Review;
using MediatR;

namespace Application.Reviews.Queries.GetReviewsWithPagination
{
    public class GetReviewsWithPaginationQuery : IRequest<Result<PagedList<ReviewDto>>>
    {
        public PagingParams Params { get; set; } = new PagingParams();
    }

    public class GetReviewsWithPaginationQueryHandler : IRequestHandler<GetReviewsWithPaginationQuery, Result<PagedList<ReviewDto>>>
    {
        private readonly IApplicationDbContext _context;

        public GetReviewsWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<ReviewDto>>> Handle(GetReviewsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Reviews.OrderBy(d => d.StartDate)
                .Select(MapFrom).AsQueryable();

            return Result<PagedList<ReviewDto>>.Success(
                await PagedList<ReviewDto>.CreateAsync(query, request.Params.PageNumber,
                request.Params.PageSize));
        }

        private ReviewDto MapFrom(Review review) => new ReviewDto()
        {
            StartDate = review.StartDate,
            EndDate = review.EndDate,
            CreateDate = review.CreateDate,
            Name = review.Name,
            Content = review.Content,
            Id = review.Id
        };
    }
}
