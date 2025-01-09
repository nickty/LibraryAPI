namespace LibraryAPI.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public Author Author { get; set; } // Navigation Property
        public string PhoneNumber {get; set;}
    }
}
