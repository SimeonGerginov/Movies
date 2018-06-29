using System.Collections.Generic;
using Movies.Core.Models;

namespace Movies.Services.Contracts
{
    public interface ICommentService
    {
        void CreateComment(Comment comment);

        IEnumerable<Comment> GetCommentsForMovie(int movieId);
    }
}
