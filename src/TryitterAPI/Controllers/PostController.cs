using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TryitterAPI.Models;
using TryitterAPI.Repository;
using static TryitterAPI.Models.Entities.Entities;

namespace TryitterAPI.Controllers
{
    [ApiController]
    [Route("post")]
    public class PostController : ControllerBase
    {
        private readonly ITryitterRepository _twitterRepository;
        public PostController(ITryitterRepository twitterRepository)
        {
            _twitterRepository = twitterRepository;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreatePost([FromBody] Post post)
        {
            var studentId = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);
            Post newPost = new() { StudentId = studentId, Images = post.Images, Text = post.Text, Title = post.Title };

            _twitterRepository.AddPost(newPost);

            return CreatedAtAction(nameof(CreatePost), new { id = studentId }, newPost);

        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult RemovePost(int id)
        {
            var studentId = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);
            var post = _twitterRepository.GetPost(id);
            if (post == null)
            {
                return NotFound(new { Message = "Post não encontrado" });
            }
            if (post.StudentId != studentId)
            {
                return Unauthorized(new { Message = "Você não é autor desse post" });
            }
            _twitterRepository.RemovePost(post);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult EditPost([FromBody] UpdatePost updatePost, int id)
        {

            int idStudent = Convert.ToInt32(User?.Claims.First(claim => claim.Type == "id").Value);

            if (updatePost.Title == null && updatePost.Text == null)
            {
                return BadRequest(new { message = "Insira os dados corretamente" });
            }
            var post = _twitterRepository.GetPost(id);


            if (post == null)
            {
                return NotFound(new { Message = "Post não encontrado" });
            }
            if (post.StudentId != idStudent)
            {
                return Unauthorized(new { Message = "Você não é autor desse post" });
            }

            _twitterRepository.EditPost(post, updatePost);
            return Ok();

        }

        [HttpGet("{id}")]
        public IActionResult AllPosts(int id)
        {
            var posts = _twitterRepository.AllPosts(id);
            if (posts == null)
            {
                return NotFound(new { Message = "Usuário não encontrado" });
            }
            return Ok(posts);
        }

        [HttpGet("{id}/last")]
        public IActionResult LastPost(int id)
        {
            var posts = _twitterRepository.LastPost(id);
            if (posts == null)
            {
                return NotFound(new { Message = "Usuário não encontrado ou não possui ultimo post" });
            }

            return Ok(posts);
        }
    }
}