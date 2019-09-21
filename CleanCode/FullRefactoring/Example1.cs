using CleanCode.FullRefactoring;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.WebControls;

namespace Project.UserControls
{
    public class PostControl : System.Web.UI.UserControl
    {
        private readonly PostRepository _postRepository;

        public PostControl()
        {
            _postRepository = new PostRepository();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                TrySavePost();
            else
                DisplyPostForm();
        }

        public void DisplyPostForm()
        {
            int postId = Convert.ToInt32(Request.QueryString["id"]);
            Post post = _postRepository.GetPost(postId);
            PostBody.Text = post.Body;
            PostTitle.Text = post.Title;
        } 


        public void TrySavePost()
        {
            PostValidator validator = new PostValidator();
            Post post = new Post()
            {
                // Map form fields to post properties
                Id = Convert.ToInt32(PostId.Value),
                Title = PostTitle.Text.Trim(),
                Body = PostBody.Text.Trim()
            };
            ValidationResult results = validator.Validate(post);

            if (results.IsValid)
            {
                _postRepository.SavePost(post);
            }
            else
            {
                BulletedList summary = (BulletedList)FindControl("ErrorSummary");

                // Display errors to the user
                foreach (var failure in results.Errors)
                {
                    Label errorMessage = FindControl(failure.PropertyName + "Error") as Label;

                    if (errorMessage == null)
                    {
                        summary.Items.Add(new ListItem(failure.ErrorMessage));
                    }
                    else
                    {
                        errorMessage.Text = failure.ErrorMessage;
                    }
                }
            }
        }

        public Label PostBody { get; set; }

        public Label PostTitle { get; set; }

        public int? PostId { get; set; }
    }

    internal class PostDbControl
    {
    }

    #region helpers

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public IEnumerable<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public class PostValidator
    {
        public ValidationResult Validate(Post post)
        {
            throw new NotImplementedException();
        }
    }

    public class DbSet<T> : IQueryable<T>
    {
        public void Add(T post)
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression
        {
            get { throw new NotImplementedException(); }
        }

        public Type ElementType
        {
            get { throw new NotImplementedException(); }
        }

        public IQueryProvider Provider
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class Post_dbContext
    {
        public DbSet<Post> Posts { get; set; }

        public void SaveChanges()
        {
        }
    }
    #endregion

}