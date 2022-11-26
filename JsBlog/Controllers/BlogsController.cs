﻿using Newtonsoft.Json;
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
                    if(result != null)
                    {
                        return Content(HttpStatusCode.OK, JToken.Parse(JsonConvert.SerializeObject(new { status = true, data = blog  })));
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
                return Content(HttpStatusCode.OK, JToken.Parse(JsonConvert.SerializeObject(new { status = true, data = blogList })));
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.ExpectationFailed, JToken.Parse(JsonConvert.SerializeObject(new { status = false, data = "Something went wrong" })));
            }
        }

    }
}
