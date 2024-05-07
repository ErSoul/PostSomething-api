using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PostSomething_api.Requests;
using PostSomething_api.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostSomething_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }
        // GET: api/<CommentController>
        [HttpGet]
        [Route("/api/posts/{postId}/comments")]
        public async Task<IEnumerable<DTO.Comment>> GetComments([FromRoute] int postId)
        {
            var comments = await _commentsService.GetCommentsFromPost(postId);
            return comments.Select(comment => new DTO.Comment(comment));
        }

        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var comment = await _commentsService.GetComment(id);

            if (comment == null)
                return NotFound();

            return Ok(new DTO.Comment(comment));
        }

        // POST api/<CommentController>
        [HttpPost]
        [Route("/api/posts/{postId}/comments")]
        [Authorize]
        public async Task<IActionResult> CreateCommentFromPost([FromBody] CommentRequest commentRequest, [FromRoute] int postId)
        {
            var userId = User.FindFirstValue(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sid)!;
            var comment = await _commentsService.CreateCommentFromPost(postId, commentRequest, userId);
            return new JsonResult(new DTO.Comment(comment));
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _commentsService.Delete(id);
            return NoContent();
        }
    }
}