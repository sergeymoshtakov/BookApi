namespace BookApi.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; } = "No Description";
        public int Year { get; set; }
    }
}
