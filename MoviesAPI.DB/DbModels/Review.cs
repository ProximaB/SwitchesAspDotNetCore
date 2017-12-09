namespace MoviesAPI.DB.DbModels
{
    public class Review
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public short Rate { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
