using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EShop.DataLayer.Enum;
using Microsoft.Extensions.Configuration;

namespace EShop.Web.Helper
{
    static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
    public static class UiHelper
    {
        public static bool UseTagManager()
        {
            return Convert.ToBoolean(ConfigurationManager.AppSetting["Global:UseTagManager"]);

        }
        public static EnumTypeSystem GetTypeSystem()
        {
            return Convert.ToString(ConfigurationManager.AppSetting["Global:TypeSystem"])== "Farnaa"?EnumTypeSystem.Farnaa:EnumTypeSystem.FarnaaPlus;

        }
    }
}
