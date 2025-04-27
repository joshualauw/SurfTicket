namespace SurfTicket.Application.Features.Auth.Command.Login
{
    public class LoginCommandResponse
    {
        public string Token { get; set; }
        public LoginCommandUser User { get; set; }
    }

    public class LoginCommandUser
    {
        public string Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
    }
}
