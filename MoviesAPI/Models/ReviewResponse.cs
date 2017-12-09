namespace MoviesAPI.Models
{
    public class ReviewResponse
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string Comment { get; set; }

        public short Rate { get; set; }
    }
}
