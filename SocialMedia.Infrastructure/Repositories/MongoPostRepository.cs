using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class MongoPostRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;
        public MongoPostRepository( SocialMediaContext socialMediaContext)
        {
            _context = socialMediaContext;
        }

        public Task<Post> GetPost(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Posts.Where( x => x.UserId == 17).ToListAsync();
            return posts;
        }
    }
}
