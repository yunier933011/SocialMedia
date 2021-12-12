using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postservice;
        private readonly IMapper _mapper;
        public PostController(IPostService postservice, IMapper mapper)
        {
            _postservice = postservice;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postservice.GetPost(id);
            var postdto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postdto);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var posts = _postservice.GetPosts();
            var postdtos = _mapper.Map<IEnumerable<PostDto>>(posts);
            var response = new ApiResponse<IEnumerable<PostDto>>(postdtos);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PostDto postdto)
        {
            var post = _mapper.Map<Post>(postdto);
            await _postservice.InsertPost(post);
            var result = _mapper.Map<PostDto>(post);

            var response = new ApiResponse<PostDto>(result);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDto postdto)
        {
            var post = _mapper.Map<Post>(postdto);
            post.Id = id;

            var result = await _postservice.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            var result = await _postservice.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
