using Dtos.Accounts;

namespace Dtos.Results
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public string? Token { get; set; }
        public List<string>? Errors { get; set; }
    }
}
