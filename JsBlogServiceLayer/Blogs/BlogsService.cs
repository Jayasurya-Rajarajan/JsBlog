using JsBlogDataLayer;
using JsBlogs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JsBlogServiceLayer.Blogs
{
    public class BlogsService
    {
        private readonly JsBlogDevelopmentEntities _dbContext;
        public BlogsService()
        {
            _dbContext = new JsBlogDevelopmentEntities();
        }

        public async Task<Blog> PostBlogs(BlogsDTO blog)
        {
            Blog blogObject = new Blog()
            {
                Title = blog.Title,
                Content = blog.Content,
                CreatedBy = "jayasurya@gmail.com",
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = null,
                ModifiedBy = null,
                IsActive = true,
                IsDeleted = false,
                DeletedOn = null

            };
            _dbContext.Blogs.Add(blogObject);
            _ = await _dbContext.SaveChangesAsync();
            return blogObject;
        }
    }
}
