using System.Collections.Generic;

namespace ProjectZero.Models {
    public class LibraryModel {
        public List<BookModel> AllTheBooks { get; set; }
        public List<AuthorModel> AllTheAuthors { get; set; }
    }
}