namespace ProjectZero.Models {
    public class LibraryConnectionModel {
        public int Id { get; set; }
        public BookModel Book { get; set; }
        public AuthorModel Author { get; set; }
    }
}