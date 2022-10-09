using JsBlog.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JsBlog.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public IHttpActionResult PostBlog(Blog blog)
        {
            try
            {
                if(blog != null)
                {
                    JsBlogDevelopmentEntities _dbContext = new JsBlogDevelopmentEntities();
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
                    _dbContext.SaveChangesAsync();
                    
                }
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.BadRequest, JToken.Parse(JsonConvert.SerializeObject(new { status = false, message= e.ToString() })));

            }
            return Content(HttpStatusCode.OK, JToken.Parse(JsonConvert.SerializeObject(new { status = true })));
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
