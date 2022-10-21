using JsBlogDataLayer;
using JsBlogServiceLayer.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsBlogs.Model;
namespace JsBlogBusinessLayer
{
    public class BlogsBAL
    {
        BlogsService _jsBlogService = new BlogsService();
        public async Task<Blog> PostBlogs(BlogsDTO blog)
        {
            return await _jsBlogService.PostBlogs(blog);
        }
    }
}
