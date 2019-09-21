using Project.UserControls;
using System.Linq;
using CleanCode.FullRefactoring;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

namespace CleanCode.FullRefactoring
{
    public class PostRepository
    {
        private Post_dbContext _dbContext;

        public PostRepository()
        {
            _dbContext = new Post_dbContext();
        }

        public Post GetPost(int postId)
        {
            Post entity = _dbContext.Posts.SingleOrDefault(p => p.Id == postId);
            return entity;
        }

        public void SavePost(Post post)
        {
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
        }
    }
}
