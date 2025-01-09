namespace LibraryAPI.Models
{
    public class Member
    {
       public int MemberID { get; set; }


       public string FullName { get; set; } // Ensure this matches your new column.

  
       public string Username { get; set; }

   
       public string Password { get; set; }
    }
}
