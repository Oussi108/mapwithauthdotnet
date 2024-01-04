namespace Taskmangaer.models
{
    public class User
    {
         
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<task> Tasks { get; set; } // Collection of tasks associated with this user

        public User()
        {
            Role = "USER";
        }

        public User(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
    
}
}
