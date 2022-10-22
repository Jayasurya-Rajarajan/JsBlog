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
            try
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
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Blog> GetBlogs(long startNum, long endNum)
        {
            try
            {
                var blogs = _dbContext.Blogs.ToList();
                var blogsFromSp = _dbContext.usp_GetBlogs(startNum, endNum).ToList();
                Blog blog = new Blog();
                List<Blog> blogList = new List<Blog>();
                foreach (var b in blogsFromSp)
                {
                    
                    blog.Id = (long)b.Id;
                    blog.Title = b.Title;
                    blog.Content = b.Content;
                    blog.CreatedBy = b.CreatedBy;
                    //DateTime dt = Convert.ToDateTime(b.CreatedOn.HasValue? b.CreatedOn.ToString("G") : "<Not Available>");
                    blog.CreatedOn = (DateTime)b.CreatedOn; 
                    //blog.CreatedOn = dt;
                    blog.IsActive = (bool)b.IsActive;
                    blogList.Add(blog);
                }
                return blogList;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
