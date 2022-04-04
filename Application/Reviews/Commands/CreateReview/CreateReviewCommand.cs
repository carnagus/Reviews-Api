using Application.Interfaces;
using Application.Models;
using Domain.Review;
using MediatR;

namespace Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<Unit>>
    {
        private readonly IApplicationDbContext _context;

        public CreateReviewCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var entity = Review.Create(request.Id, request.Name!,
                request.Content, request.StartDate, request.EndDate, request.CreateDate);

            _context.Reviews.Add(entity);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to create review");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
