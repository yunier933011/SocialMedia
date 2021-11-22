using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<User> _userRepository;
        public UnitOfWork(SocialMediaContext context, IRepository<Post> postRepository, IRepository<Comment> commentRepository, IRepository<User> userRepository)
        {
            _context = context;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }
        public IRepository<Post> PostRepository => _postRepository ?? new Repository<Post>(_context);

        public IRepository<User> UserRepository => _userRepository ?? new Repository<User>(_context);

        public IRepository<Comment> CommentRepository => _commentRepository ?? new Repository<Comment>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
