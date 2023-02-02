using JsBlogDataLayer;
using JsBlogs.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


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

        public Tuple<List<Blog>, int> GetBlogs(int startNum, int endNum)
        {
            try
            {
                //var blogs = _dbContext.Blogs.ToList();
                ObjectParameter totalRecords = new ObjectParameter("totalRecords", typeof(int));
                var blogsFromSp = _dbContext.usp_GetBlogs(startNum, endNum, totalRecords).ToList();
                Blog blog = new Blog();
                List<Blog> blogList = new List<Blog>();
                int total = Convert.ToInt32(totalRecords.Value);
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
                return new Tuple<List<Blog>, int>(blogList, total);
                //return blogList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool DeleteBlog(int id)
        {
            try
            {
                Blog blogObj = new Blog();
                blogObj = (
                   from s in _dbContext.Blogs where s.Id == id select s
                   ).FirstOrDefault();
                if (blogObj != null)
                {
                    blogObj.IsActive = false;
                    blogObj.DeletedOn = DateTime.UtcNow;
                    blogObj.IsDeleted = true;
                    _dbContext.Entry(blogObj).State = EntityState.Modified;
                    _dbContext.SaveChangesAsync();
                }
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}