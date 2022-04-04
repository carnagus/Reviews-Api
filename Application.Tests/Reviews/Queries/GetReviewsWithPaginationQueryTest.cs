namespace Application.Tests.Reviews.Queries
{
    using Domain.Review;
    using global::Application.Models;
    using global::Application.Reviews.Queries.GetReviewsWithPagination;
    using global::Application.Tests.Infrastructure;
    using global::Infrastructure.Persistance;
    using NUnit.Framework;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public class GetAllTravelsQueryHandlerTest
    {
        private ApplicationDbContext? _context;
        private GetReviewsWithPaginationQueryHandler? _handler;

        [SetUp]
        public void SetUp()
        {
            _context = ApplicationDbContextFactory.Create();
            _context.Reviews.AddRange(ArrangeData());
            _context.SaveChanges();
            _handler = new GetReviewsWithPaginationQueryHandler(_context);
        }

        [Test]
        public async Task HandleTestWithPageNumber1AndPageSize2ShouldReturn2Reviews()
        {
            var query = new GetReviewsWithPaginationQuery {Params = new PagingParams() {PageNumber=1, PageSize = 2 } };
            
            var result = await _handler!.Handle(query, CancellationToken.None);
            
            Assert.That(result.IsSuccess, Is.EqualTo(true));
            Assert.That(result?.Value?.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task HandleTestWithoutPageNumberAndPageSizeShouldReturnReviewsMax10()
        {
            var query = new GetReviewsWithPaginationQuery {Params = new PagingParams() };
            
            var result = await _handler!.Handle(query, CancellationToken.None);
            
            Assert.That(result.IsSuccess, Is.EqualTo(true));
            Assert.That(result?.Value?.Count, Is.LessThan(11));
        }

        [TearDown]
        public void TearDown()
        {
            ApplicationDbContextFactory.Destroy(_context!);
        }

        private Review[] ArrangeData()
        {
            var startDate = new DateTime(2022, 1, 1);
            var endDate = new DateTime(2022, 2, 1);
            var createDate = new DateTime(2022, 3, 1);
            var result = new Review[]
            {
                Review.Create(Guid.NewGuid(), "Name1", "Content1", startDate,endDate,createDate),
                Review.Create(Guid.NewGuid(), "Name2", "Content1", startDate.AddDays(1),endDate.AddDays(1),createDate.AddDays(1)),
                Review.Create(Guid.NewGuid(), "Name3", "Content1", startDate.AddDays(2),endDate.AddDays(2),createDate.AddDays(2)),
            };

            return result;
        }

    }
}