using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Post> GetPosts()
        {
            return _unitofwork.PostRepository.GetAll();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitofwork.UserRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new BusinessException("User doesn't exist");
            }
            post.Date = DateTime.Now;
            var userPosts = await _unitofwork.PostRepository.GetPostsbyUser(post.UserId);
            if (userPosts.Count() < 10)
            {
                var lastPost = userPosts.OrderByDescending(x => x.Date).FirstOrDefault();
                if ((post.Date - lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("You are not able to publish");
                }
            }
            if (post.Description.Contains("sexo"))
            {
                throw new BusinessException("Invalid word");
            }
            await _unitofwork.PostRepository.Insert(post);
            await _unitofwork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            _unitofwork.PostRepository.Update(post);
            await _unitofwork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitofwork.PostRepository.Delete(id);
            await _unitofwork.SaveChangesAsync();
            return true;
        }
    }
}
