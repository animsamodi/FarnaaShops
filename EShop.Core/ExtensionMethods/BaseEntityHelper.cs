using System;
using System.Security.Claims;
using EndPoint.Web.Utilities;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.User;

namespace EShop.Core.ExtensionMethods
{
    public static class BaseEntityHelper
    {


        public static T SetCreateDefaultValue<T>(this T entity, long? userId) where T : BaseEntity
        {

            entity.ChangeUserId = userId;


            entity.CreateDate = DateTime.Now;
            entity.LastUpdateDate = entity.CreateDate;
            entity.IsDelete = false;

            return entity;
        }
        public static T SetEditDefaultValue<T>(this T entity, long? userId ) where T : BaseEntity
        {
            entity.ChangeUserId = userId;
            entity.LastUpdateDate = DateTime.Now;

            return entity;
        }
        public static T SetRemoveDefaultValue<T>(this T entity, long? userId) where T : BaseEntity
        {
            entity.ChangeUserId = userId;


            entity.LastUpdateDate = DateTime.Now;
            entity.IsDelete = true;

            return entity;
        }
    }
}
