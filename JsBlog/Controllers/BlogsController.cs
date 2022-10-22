using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JsBlogBusinessLayer;
using JsBlogs.Model;

namespace JsBlog.Controllers
{
    public class BlogsController : ApiController
    {
        
        JsBlogBusinessLayer.BlogsBAL _blogBAL = new BlogsBAL();
        public IHttpActionResult PostBlog(BlogsDTO blog)
        {
            try
            {
                

                if (blog != null)
                {
                    var result = _blogBAL.PostBlogs(blog);
                    //JsBlogDevelopmentEntities _dbContext = new JsBlogDevelopmentEntities();
                    //Blog blogObject = new Blog()
                    //{
                    //    Title = blog.Title,
                    //    Content = blog.Content,
                    //    CreatedBy = "jayasurya@gmail.com",
                    //    CreatedOn = DateTime.UtcNow,
                    //    ModifiedOn = null,
                    //    ModifiedBy = null,
                    //    IsActive = true,
                    //    IsDeleted = false,
                    //    DeletedOn = null

                    //};
                    //_dbContext.Blogs.Add(blogObject);
                    //_dbContext.SaveChangesAsync();
                    if(result != null)
                    {
                        return Content(HttpStatusCode.OK, JToken.Parse(JsonConvert.SerializeObject(new { status = true, blog = blog  })));
                    }

                }
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, JToken.Parse(JsonConvert.SerializeObject(new { status = false, message = e.ToString() })));

            }
            return Content(HttpStatusCode.OK, JToken.Parse(JsonConvert.SerializeObject(new { status = true })));
        }
        public IHttpActionResult GetBlogs(long startNum, long endNum)
        {
            try
            {
                var blogList = _blogBAL.GetBlogs(startNum, endNum);
                //var blogs = dbContext.Blogs.ToList();
                //Blog blog = new Blog();
                //List<Blog> blogsList = new List<Blog>();
                //foreach(var b in blogs)
                //{
                //    blog.Id = b.Id;
                //    blog.Title = b.Title;
                //    blog.Content = b.Content;
                //    blog.CreatedBy = b.CreatedBy;
                //    DateTime dt = Convert.ToDateTime(b.CreatedOn.ToString("G"));
                //    blog.CreatedOn = dt;
                //    blog.IsActive = b.IsActive;
                //    blogsList.Add(blog);

                //}
                return Content(HttpStatusCode.OK, JToken.Parse(JsonConvert.SerializeObject(new { status = true, blogs = blogList })));
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.ExpectationFailed, JToken.Parse(JsonConvert.SerializeObject(new { status = false, message = "Something went wrong" })));
            }
        }

    }
}
