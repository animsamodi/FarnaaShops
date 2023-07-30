using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;

namespace EShop.Web.Filter
{
    public class TrimDashRule : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            var req = context.HttpContext.Request;
            var currentHost = req.Host;
            if (req.Path.Value != null && req.Path.Value.Length > 1 && req.Path.Value.EndsWith("-"))
            {
                var newHost = new HostString(currentHost.Host);
                if (currentHost.Port != null)
                    newHost = new HostString(currentHost.Host, currentHost.Port ?? 80);
                var newUrl = new StringBuilder().Append("https://").Append(newHost).Append(req.PathBase).Append(req.Path).Append(req.QueryString);
                context.HttpContext.Response.Redirect(newUrl.ToString().TrimEnd('-'), true);
                context.Result = RuleResult.EndResponse;
            }
        }
    }
}