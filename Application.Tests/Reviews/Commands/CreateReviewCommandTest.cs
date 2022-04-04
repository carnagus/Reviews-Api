namespace Application.Tests.Reviews.Commands
{
    using Application.Tests.Infrastructure;
    using global::Application.Reviews.Commands.CreateReview;
    using global::Infrastructure.Persistance;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public class CreateReviewCommandTest
    {
        private ApplicationDbContext? _context;
        private CreateReviewCommandHandler? _handler;
        private Guid _id = Guid.NewGuid();
        private DateTime _date = new DateTime(2022, 1, 1);
        private CreateReviewCommand? _command;

        [SetUp]
        public void SetUp()
        {
            _context = ApplicationDbContextFactory.Create();
            _handler = new CreateReviewCommandHandler(_context);
            _command = new CreateReviewCommand()
            {
                Id = _id,
                Content = "testContent",
                Name = "testName",
                StartDate = _date,
                EndDate = _date.AddDays(1),
                CreateDate = _date.AddDays(2),
            };
        }

        [Test]
        public async Task HandleCreateNewReviewShouldReturnSuccess()
        {
            var result = await _handler!.Handle(_command!, CancellationToken.None);

            Assert.That(result.IsSuccess, Is.EqualTo(true));
        }

        [Test]
        public async Task HandleCreateNewReviewShouldHaveOneEntityInDb()
        {
            var result = await _handler!.Handle(_command!, CancellationToken.None);

            Assert.That(_context!.Reviews.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task HandleCreateNewReviewShouldHaveValues()
        {
            var result = await _handler!.Handle(_command!, CancellationToken.None);
            
            Assert.That(_context!.Reviews.Single().Id, Is.EqualTo(_id));
            Assert.That(_context!.Reviews.Single().Name, Is.EqualTo("testName"));
            Assert.That(_context!.Reviews.Single().Content, Is.EqualTo("testContent"));
            Assert.That(_context!.Reviews.Single().StartDate, Is.EqualTo(new DateTime(2022, 1, 1)));
            Assert.That(_context!.Reviews.Single().EndDate, Is.EqualTo(new DateTime(2022, 1, 2)));
            Assert.That(_context!.Reviews.Single().CreateDate, Is.EqualTo(new DateTime(2022, 1, 3)));
        }

        [TearDown]
        public void TearDown()
        {
            ApplicationDbContextFactory.Destroy(_context!);
        }

    }
}