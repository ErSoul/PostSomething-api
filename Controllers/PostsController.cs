using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostSomething_api.Repositories.Interface;
using PostSomething_api.Requests;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostSomething_api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController(IPostRepository postRepository) : ControllerBase
    {

        // GET: api/<PostController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok(postRepository.GetList().Select(post => new DTO.Post(post)));
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var post = await postRepository.Find(p => p.Id == id);

            if (post is null)
                return new NotFoundObjectResult(id);

            return new JsonResult(new DTO.Post(post));
        }

        // POST api/<PostController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostRequest post)
        {
            post.Author = User.FindFirstValue(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sid);
            return new JsonResult(new DTO.Post(await postRepository.CreatePost(post)));
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await postRepository.Find(p => p.Id == id);

            if (post is null)
                return new NotFoundResult();

            await postRepository.Delete(post);
            return NoContent();
        }
    }
}