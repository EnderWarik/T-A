namespace TitsAndAss.Models
{
    public class Account
    {

        public int Id { get; set; }

        //public int Identificator { get; set; }

        public string? Name { get; set; }

        public string? Email{ get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }

        public int Money { get; set; }

    }

    public enum Role
    {
        User,
        Admin
    }
}
