using System.Text;
using Microsoft.AspNetCore.Rewrite;

namespace EShop.Web.Filter
{
    public class RedirectFromHomeToRoot : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            var req = context.HttpContext.Request;
            var currentHost = req.Host;
            var path = req.Path.Value;
            if (path.ToLower().Equals("/home"))
            {
               
                var newUrl = new StringBuilder().Append("https://").Append(currentHost).Append(req.PathBase);
                context.HttpContext.Response.Redirect(newUrl.ToString(), true);
                context.Result = RuleResult.EndResponse;
            }
        }
    }
}