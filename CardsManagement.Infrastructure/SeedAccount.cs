namespace CardsManagement.Infrastructure
{
    public class SeedAccountSettings: ISeedAccountSettings
    {
      public  SeedAccount AdminAccount { get; set; }
      public  SeedAccount UserAccount { get; set; }
    }
    public interface ISeedAccountSettings
    {
        SeedAccount AdminAccount { get; set;}
        SeedAccount UserAccount { get; set;}
    }
    public class SeedAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
