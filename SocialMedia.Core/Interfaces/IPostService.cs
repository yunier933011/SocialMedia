using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        Task<bool> DeletePost(int id);
        Task<Post> GetPost(int id);
        IEnumerable<Post> GetPosts();
        Task InsertPost(Post post);
        Task<bool> UpdatePost(Post post);
    }
}