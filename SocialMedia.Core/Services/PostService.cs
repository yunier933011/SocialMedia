using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitofwork;

        public PostService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task<Post> GetPost(int id)
        {
            return await _unitofwork.PostRepository.GetById(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _unitofwork.PostRepository.GetAll();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitofwork.UserRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }
            if (post.Description.Contains("sexo"))
            {
                throw new Exception("Invalid word");
            }
            await _unitofwork.PostRepository.Insert(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            await _unitofwork.PostRepository.Update(post);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitofwork.PostRepository.Delete(id);
            return true;
        }
    }
}
