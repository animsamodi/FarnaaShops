using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Core.Security
{
    public class CheckPermissonAttribute : ActionFilterAttribute
    {
       private int _id;
        public CheckPermissonAttribute(int id)
        {
            _id = id;
        }
        public override void OnActionExecuting(ActionExecutingContext context) {
            try
            {
   var userpermisson = 
                Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(context.HttpContext.User.FindFirst("permissons").Value);
            if(userpermisson.Count > 0)
            {
                if(!userpermisson.Any(c=> c==_id))
                    context.HttpContext.Response.Redirect("/admin/user/Register");
            }
            else
            {
                context.HttpContext.Response.Redirect("/admin/user/Register");
            }
            }
            catch (Exception e)
            {
                context.HttpContext.Response.Redirect("/admin/user/Register");

            }

        }

    }
}
