using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EndPoint.Web.Utilities
{
    public static class ClaimUtility
    {
        public static long? GetUserId(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst("userid");
                if (userId != null)
                {
	                return long.Parse(userId.Value);
 				}
                else
                {
	                return null;
                }
			
            }
            catch (Exception)
            {

                return null;
            }

        }   public static string GetUserFullName(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                string name = claimsIdentity.FindFirst("name").Value;
                return name;
            }
            catch (Exception)
            {

                return null;
            }

        }   
        public static bool IsColleauge(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var isColleauge = claimsIdentity.FindFirst("IsColleauge");
                if (isColleauge != null)
                {
	                bool res = Convert.ToBoolean(isColleauge.Value);
	                return res;
				}
                else
                {
	                return false;
                }
				
                
            }
            catch (Exception)
            {

                return false;
            }

        }   
        
        
        public static string GetUserEmail(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                
                return claimsIdentity.FindFirst(ClaimTypes.Email).Value;
            }
            catch (Exception)
            {

                return null;
            }

        }


        public static List<long> GetPermissons(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                List<long> permissons = new List<long>();
                foreach (var item in claimsIdentity.Claims.Where(p => p.Type.EndsWith("permissons")))
                {
                    var userpermissons = item.Value.Replace("[", "").Replace("]", "").Split(',').ToList();
                    foreach (var userpermisson in userpermissons)
                    {
                        
                        permissons.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<long>(userpermisson));
                    }
                }
                return permissons;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static List<string> GetRolse(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                List<string> rolse = new List<string>();
                foreach (var item in claimsIdentity.Claims.Where(p => p.Type.EndsWith("role")))
                {
                    rolse.Add(item.Value);
                }
                return rolse;
            }
            catch (Exception )
            {
                return null;
            }

        }
    }
}
