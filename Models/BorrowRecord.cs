using System;


namespace LibraryAPI.Models
{
    public class BorrowRecord
    {
        public int RecordID { get; set; }
        public int MemberID { get; set; }
        public int BookID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Member Member { get; set; }
        public Book Book { get; set; }
    }
}
