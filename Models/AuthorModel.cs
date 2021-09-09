using System.Collections.Generic;

namespace ProjectZero.Models {
    public class AuthorModel {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public ICollection<LibraryConnectionModel> Connections { get; set; }
    }
}