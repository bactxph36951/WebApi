using Dtos.Comments;

namespace Dtos.Results
{
    public class CommentResult
    {
        public CommentDto? Comment { get; set; }
        public string? Error { get; set; }
        public List<string>? Errors { get; set; }
    }
}
