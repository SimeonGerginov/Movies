using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using Movies.Core.Contracts;
using Movies.Core.Models;
using Movies.Services.Contracts;

namespace Movies.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> commentRepository;

        public CommentService(IRepository<Comment> commentRepository)
        {
            Guard.WhenArgument(commentRepository, "Comment Repository").IsNull().Throw();

            this.commentRepository = commentRepository;
        }

        public void CreateComment(Comment comment)
        {
            Guard.WhenArgument(comment, "Comment").IsNull().Throw();

            var commentExists = this.commentRepository
                .GetAllFiltered(c => c.UserId == comment.UserId && c.MovieId == comment.MovieId
                                                                && c.Content == comment.Content)
                .Any();

            if (commentExists)
            {
                throw new InvalidOperationException("Comment already exists!");
            }

            comment.CreatedOn = DateTime.UtcNow;
            this.commentRepository.Add(comment);
        }

        public IEnumerable<Comment> GetCommentsForMovie(int movieId)
        {
            var comments = this.commentRepository
                .GetAllFilteredAndOrdered(c => c.MovieId == movieId, c => c.CreatedOn);

            return comments;
        }
    }
}
