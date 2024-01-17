namespace ImagineBookStore.Core.Models.View
{
    public class BookView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int TotalStock { get; set; }
    }
}
