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
        public Blog PostBlogs(BlogsDTO blog)
        {
            try
            {
                return _jsBlogService.PostBlogs(blog);
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
                return _jsBlogService.GetBlogs(startNum, endNum);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool DeleteBlog(int id)
        {
            try
            {
                var result = _jsBlogService.DeleteBlog(id);
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
