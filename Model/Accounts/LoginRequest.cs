namespace Dtos.Accounts
{
    public class LoginRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
