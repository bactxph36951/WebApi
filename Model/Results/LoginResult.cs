namespace Dtos.Results
{
    public class LoginResult
    {
        public string? Token { get; set; }
        public List<string>? Errors { get; set; }
    }
}
