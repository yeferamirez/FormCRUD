namespace Form.Domain.Entities.Responses
{
    public class UserResponse
    {
        public User User { get; set; }
        public List<User> Users { get; set; }
        public string StatusMessage { get; set; }
        public string Message { get; set; }

        public UserResponse(string statusMessage, string message)
        {
            StatusMessage = statusMessage;
            Message = message;
        }

        public UserResponse(string statusMessage, User user)
        {
            StatusMessage = statusMessage;
            User = user;
        }

        public UserResponse(string statusMessage, List<User> users)
        {
            StatusMessage = statusMessage;
            Users = users;
        }
    }
}
