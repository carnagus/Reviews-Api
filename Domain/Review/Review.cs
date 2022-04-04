namespace Domain.Review
{
    public class Review
    {
        protected Review()
        {
        }

        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public string? Content { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime CreateDate { get; private set; }

        public static Review Create(Guid id, string name, string content, DateTime startDate, DateTime endDate, DateTime createDate)
            => new Review()
            {
                Id = id,
                Name = name,
                Content = content,
                StartDate = startDate,
                EndDate = endDate,
                CreateDate = createDate,
            };
    }
}