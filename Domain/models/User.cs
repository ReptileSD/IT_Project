using Domain.Logic;


namespace Domain.Models
{
    public class User
    {
        public int Id;
        public string Phone;
        public string Fullname;
        public Role Role;

        public string Username;
        public string Password;

        public User(
            int id,
            string phone,
            string fullname,
            string username,
            string password,
            Role role = Role.Patient
            )
        {
            Id = id;
            Phone = phone;
            Fullname = fullname;
            Username = username;
            Password = password;
            Role = role;
        }
        public Result IsValid() 
        {
            if (string.IsNullOrEmpty(Username))
                return Result.Fail("Empty username.");
            if (string.IsNullOrEmpty(Password))
                return Result.Fail("Empty password.");
            if (string.IsNullOrEmpty(Phone))
                return Result.Fail("Empty phone number.");
            if (string.IsNullOrEmpty(Fullname))
                return Result.Fail("Empty fullname.");

            return Result.Ok();
        }
        public static bool operator true(User u) => u.Id != -1;
        public static bool operator false(User u) => u.Id == -1;
    }
}
