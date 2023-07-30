using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces.Seo;
using EShop.DataLayer.Context;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;
using EShop.DataLayer.Entities.Seo;
using Microsoft.Extensions.Caching.Memory;

namespace EShop.Web.Middlewares
{
    public class RedirectMiddleware
    {
        private readonly RequestDelegate _next;


        public RedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationDbContext dataContext,
            IRedirectService redirectService)
        {
            string path = context.Request.Path.ToString().ToLower();
            string query = context.Request.QueryString.Value;

            if (!string.IsNullOrEmpty(query))
                path += query;

            // ignore static files 
            if (
                !path.Contains("css") &&
                !path.Contains("woff") &&
                !path.Contains("js") &&
                !path.Contains("png") &&
                !path.Contains("png") &&
                !path.Contains("jpg") &&
                !path.Contains(".") &&
                !path.Contains("/error/") &&
                !path.ToLower().Contains("/admin/") &&
                 !path.ToLower().Contains("/user/"))
            {
                // delete extra / end of Url
                if (path.Length > 2)
                {
                    if (path[path.Length - 1] == '/' || path[path.Length - 1].ToString() == @"\")
                        path = path.Remove(path.Length - 1, 1);
                }
                // delete extra / start of Url

                if (path.Length > 2)
                {
                    if (path[0] == '/' || path[0].ToString() == @"\")
                        path = path.Remove(0, 1);
                }

                // Handle all %20 in route 
                if (path.EndsWith("%20"))
                {
                    string searchStr = "%20";
                    int lastIndex = path.LastIndexOf(searchStr);
                    if (lastIndex >= 0)
                        path = path.Substring(0, lastIndex) + path.Substring(lastIndex + searchStr.Length);

                    context.Response.Redirect($"{path}");
                }

                // Check Redirects from cache
                Redirect[] RedirectUrls = new Redirect[] { };
          

                    RedirectUrls = (await redirectService.GetAllRedirects()).ToArray();

                  
               // var all = ;
                for (int i = 0; i < RedirectUrls.Length; i++)
                {
                    var url = RedirectUrls[i].OldUrl.ToLower();
                    if (url.Length > 2)
                    {
                        if (url[url.Length - 1] == '/' || url[url.Length - 1].ToString() == @"\")
                            url = url.Remove(url.Length - 1, 1);
                    }
                    // delete extra / start of Url

                    if (url.Length > 2)
                    {
                        if (url[0] == '/' || url[0].ToString() == @"\")
                            url = url.Remove(0, 1);
                    }
                    if (path == url)
                    {
                        var newUrl = String.Join(
                                                    "/",
                                                    RedirectUrls[i].NewUrl.Split("/").Select(s => System.Net.WebUtility.UrlEncode(s))
                                                );
                        newUrl = newUrl.Replace("%3A", ":");
                        context.Response.Redirect(newUrl,false,false);
                        context.Response.StatusCode = 301;
                        await _next(context);
                        if (context.Response.StatusCode == 404)
                        {
                            context.Response.StatusCode = 301;
                        }

                        break;
                    }
                

                }

            }
          
            await _next(context);
        }
        // Extension method used to add the middleware to the HTTP request pipeline.

    }
    public static class RedirectMiddlewareExtensions
    {
        public static IApplicationBuilder UseRedirectMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RedirectMiddleware>();
        }
    }
}


