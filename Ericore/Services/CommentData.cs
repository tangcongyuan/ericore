using Ericore.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Ericore.Services
{
    public interface ICommentData
    {
        IEnumerable<Comment> GetAllComments();
        Comment Get(int id);
        void Add(Comment newComment);
    }

    public class InMemoryCommentData : ICommentData
    {
        static List<Comment> _comments;

        static InMemoryCommentData()
        {
            _comments = new List<Comment>() {
                new Comment{ Id = 1, Text = "First comment is always important."},
                new Comment{ Id = 2, Text = "Who cares about the second comment?"}
            };
        }
        public IEnumerable<Comment> GetAllComments() => _comments;

        public Comment Get(int id)
        {
            return _comments.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Comment newComment)
        {
            newComment.Id = _comments.Max(c => c.Id) + 1;
            _comments.Add(newComment);
        }
    }

    public class SqlCommentData : ICommentData
    {
        private EricoreDbContext _context;

        public SqlCommentData(EricoreDbContext context)
        {
            _context = context;
        }

        public void Add(Comment newComment)
        {
            _context.Comments.Add(newComment);
            _context.SaveChanges();
        }

        public Comment Get(int id)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments.ToList<Comment>();
        }
    }
}
