using Dtos.Comments;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentService.GetAll();

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentService.GetById(id);

            return Ok(comment);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(CommentCreateRequest request)
        {
            var comment = await _commentService.Add(request);

            return Ok(comment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentService.Delete(id);

            return Ok(comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CommentUpdateRequest request)
        {
            var comment = await _commentService.Update(id, request);

            return Ok(comment);
        }
    }
}
