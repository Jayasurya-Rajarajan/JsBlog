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

        public Blog PostBlogs(BlogsDTO blog)
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

                //var result = _dbContext.Blogs.Add(blogObject);
                var result = _dbContext.usp_createBlogs(blogObject.Title, blogObject.Content, blogObject.CreatedBy,
                    blogObject.CreatedOn, blogObject.DeletedOn, blogObject.IsActive, blogObject.IsDeleted,
                    blogObject.ModifiedBy, blogObject.ModifiedOn);
                //var rs = await _dbContext.SaveChangesAsync();
                var returnResult = result.Select(p => new Blog
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn,
                    ModifiedOn = p.ModifiedOn,
                    ModifiedBy = p.ModifiedBy,
                    IsActive = p.IsActive,
                    IsDeleted = p.IsDeleted,
                    DeletedOn = p.DeletedOn
                }).FirstOrDefault();
                return returnResult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Blog> GetBlogs(long startNum, long endNum)
        {
            try
            {
                //var blogs = _dbContext.Blogs.ToList();
                var blogsFromSp = _dbContext.usp_GetBlogs(startNum, endNum).ToList();
                Blog blog = new Blog();
                List<Blog> blogList = new List<Blog>();
                foreach (var b in blogsFromSp)
                {
                    blog = new Blog
                    {
                        Id= (long)b.Id,
                        Title = b.Title,
                        Content = b.Content,
                        CreatedBy = b.CreatedBy,
                        CreatedOn = (DateTime)b.CreatedOn,
                        IsActive = (bool)b.IsActive,
                    };
                    blogList.Add(blog);
                }
                return blogList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}