namespace Dtos.Comments
{
    public class CommentDto : CommentCreateRequest
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }
    }
}
