namespace SampleWebAPI.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }    
        public string Token { get; set; }


        public AuthenticateResponse(SampleWebAPI.Domain.User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.UserName;
            Password = user.Password;
            Token = token;
        }
    }
}
