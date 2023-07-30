using Audit.Core;
using AutoMapper;
using EShop.Core.Helpers;
using EShop.Core.Sender;
using EShop.Core.Services.Implementations;
using EShop.Core.Services.Implementations.Components;
using EShop.Core.Services.Implementations.Order;
using EShop.Core.Services.Implementations.Product;
using EShop.Core.Services.Implementations.Seller;
using EShop.Core.Services.Implementations.User;
using EShop.Core.Services.Implementations.Variant;
using EShop.Core.Services.Interfaces;
using EShop.Core.Services.Interfaces.Components;
using EShop.DataLayer.Context;
using EShop.DataLayer.Repository;
using EShop.Web.Attribute;
using EShop.Web.Filter;
using EShop.Web.Helper;
using Ganss.XSS;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using EShop.Core.Services.Implementations.Seo;
using EShop.Core.Services.Interfaces.Seo;
using EShop.Web.Middlewares;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Primitives;
using Nest;

namespace EShop.Web
{
    public class Startup
    {

        public const string CookieScheme = "EshopCookie";
        private IWebHostEnvironment CurrentEnvironment { get; set; }
        readonly string MyAllowSpecificOrigins = "_EshopAllowSpecificOrigins";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            //CQRS
            if (IsProduction())
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(name: MyAllowSpecificOrigins,
                        policy =>
                        {

                            //policy.WithOrigins("https://localhost:44388/",
                            //        "https://farnaa.com/").AllowAnyHeader()
                            //    .AllowAnyMethod();      
                            policy.WithOrigins("https://farnaa.com/").AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });
            }

            //CQRS


            if (IsProduction())
            {
                services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"),
                        new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            DisableGlobalLocks = true
                        }));
                services.AddHangfireServer();
            }
           
 
            services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            // Start Compression
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                //options.Providers.Add<CustomCompressionProvider>();
                options.MimeTypes =
                    ResponseCompressionDefaults.MimeTypes.Concat(
                        new[]
                        {
                            "image/svg+xml",
                            "application/javascript",
                            "application/json",
                            "application/xml",
                            "text/css",
                            "text/html",
                            "text/json",
                            "text/plain",
                            "text/xml"
                        });
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            // End Compression


            IHtmlSanitizer sanitizer = new HtmlSanitizer();
            services.AddSingleton(sanitizer);
            services.AddSingleton<HtmlEncoder>(
                HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
                    UnicodeRanges.Arabic }));
            services.AddHttpContextAccessor();

            //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            #region Add DbContext

            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("ApplicationDbContext")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #endregion

            // Add Redis
            //services.AddCacheServices(Configuration);

            #region Application Services


            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAttributeRatingService, AttributeRatingService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IDiscountCodeService, DiscountCodeService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<IGiftCartService, GiftCartService>();
            services.AddScoped<IMainMenuService, MainMenuService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentDetialService, PaymentDetialService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ISaleTransactionService, SaleTransactionService>();
            services.AddScoped<ISellerService, SellerService>();
            services.AddScoped<IShipmentService, ShipmentService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVariantService, VariantService>();
            services.AddScoped<ICrmService, CrmService>();
            services.AddScoped<IStaticPageService, StaticPageService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IIndexLayoutService, IndexLayoutService>();
            services.AddScoped<ICategoryMainService, CategoryMainService>();
            services.AddScoped<ISiteSettingService, SiteSettingService>();
            services.AddScoped<IEducationalDocService, EducationalDocService>();
            services.AddScoped<IReleaseNoteService, ReleaseNoteService>();
            services.AddScoped<ICategoryBrandPageService, CategoryBrandPageService>();
            //Supplier
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ISupplierFactorService, SupplierFactorService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<IWarehouseProductService, WarehouseProductService>();
            services.AddScoped<ISupplierFactorProductService, SupplierFactorProductService>();

            //Supplier
            services.AddScoped<IProductPricePageService, ProductPricePageService>();
            //
            services.AddScoped<ISiteMenuService, SiteMenuService>();
            services.AddScoped<IFilterPriceService, FilterPriceService>();
            services.AddScoped<IFilterPropertyService, FilterPropertyService>();
            services.AddScoped<IUserProductViewService, UserProductViewService>();
            //
            services.AddScoped<ISiteFotterMenuService, SiteFotterMenuService>();
            services.AddScoped<ISiteFotterLinkService, SiteFotterLinkService>();
            services.AddScoped<IProductOtherPageService, ProductOtherPageService>();
            services.AddScoped<ISearchSuggestionService, SearchSuggestionService>();
            services.AddScoped<IUserSearchService, UserSearchService>();
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IRedirectService, RedirectService>();
            services.AddScoped<IPlusPriceRangeService, PlusPriceRangeService>();

            //
            services.AddTransient<IEmailSender, MessageSender>();
            services.AddTransient<ISmsSender, MessageSender>();
            services.AddTransient<IRenderViewToString, RenderViewToString>();
            services.AddTransient<ICreditService, CreditService>();
            services.AddTransient<IUiCircleComponentService, UiCircleComponentService>();
            //services.AddTransient<IRedirectService, RedirectService>();
            services.AddTransient<IOfoghService, OfoghService>();
            services.AddTransient<ICooperationRequestService, CooperationRequestService>();
            services.AddTransient<IColleaugeSettingService, ColleaugeSettingService>();
            services.AddTransient<IPageSeoService, PageSeoService>();
            services.AddTransient<IProductSeriService, ProductSeriService>();
            services.AddTransient<IColleaguePriceRangeService, ColleaguePriceRangeService>();
            services.AddTransient<IPackingService, PackingService>();

            services.AddTransient<IAuditScopeFactory, AuditScopeFactory>();
            services.AddTransient<EShop.Logging.AuditLog.IAuditService, EShop.Logging.AuditLog.AuditService>();
            services.AddTransient<IColleaguePlusPriceRangeService, ColleaguePlusPriceRangeService>();

            #endregion
            services.AddAuthentication(CookieScheme)
                .AddCookie(CookieScheme, options =>
                {
                    options.AccessDeniedPath = "/users/Register";
                    options.LoginPath = "/users/Register";

                    options.ExpireTimeSpan = TimeSpan.FromDays(31);
                    options.SlidingExpiration = false;

                });

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRedirectMiddleware();

            // Start Compression

            app.UseResponseCompression();

            // End Compression


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseStatusCodePagesWithReExecute("/Error/PageNotFound");

            //app.UseMiddleware<ReidrectMiddleware>();

            var rewriteOptions = new RewriteOptions();
            app.UseRewriter(rewriteOptions);
            //
            //StaticFileOptions staticFileOptionoptions = new StaticFileOptions { ContentTypeProvider = new FileExtensionContentTypeProvider() };
            //((FileExtensionContentTypeProvider)staticFileOptionoptions.ContentTypeProvider).Mappings.Add(new KeyValuePair<string, string>(".gltf", "model/gltf+json"));
            //((FileExtensionContentTypeProvider)staticFileOptionoptions.ContentTypeProvider).Mappings.Add(new KeyValuePair<string, string>(".glb", "model/gltf-buffer"));
            //((FileExtensionContentTypeProvider)staticFileOptionoptions.ContentTypeProvider).Mappings.Add(new KeyValuePair<string, string>(".hdr", "model/gltf-buffer"));


            if (IsProduction())
            {
                app.UseCors(MyAllowSpecificOrigins);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    //// Cache static files for 30 days
                    //ctx.Context.Response.Headers.Append(new KeyValuePair<string, StringValues>("Cache-Control", "public,max-age=259200"));
                    //ctx.Context.Response.Headers.Append(new KeyValuePair<string, StringValues>("Expires", DateTime.UtcNow.AddDays(3).ToString("R", CultureInfo.InvariantCulture)));
                    // Cache static file for 1 year
                    //if (!string.IsNullOrEmpty(ctx.Context.Request.Query["v"]))
                    //{
                        ctx.Context.Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
                        ctx.Context.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") }); // Format RFC1123
                    //}
                },
                ContentTypeProvider = new FileExtensionContentTypeProvider
                {
                    Mappings =
 {
     new KeyValuePair<string, string>(".gltf", "model/gltf+json"),
     new KeyValuePair<string, string>(".glb", "model/gltf-buffer"),
     new KeyValuePair<string, string>(".hdr", "model/gltf-buffer")
 }
                }

            }); ;

            //


            app.UseAuthentication();
            if (IsProduction())
            {
                app.UseHangfireDashboard();
            }

            if (Convert.ToBoolean(ConfigurationManager.AppSetting["Global:UrlRewriter"]))
            {
                var options = new RewriteOptions();
                options.Rules.Add(new NonWwwRule());
                options.Rules.Add(new TrimSlashRule());
                //options.Rules.Add(new TrimDashRule());
                options.Rules.Add(new RedirectFromHomeToRoot());
                //options.Rules.Add(new TrimSpaceRule());
                app.UseRewriter(options);
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "user",
                    pattern: "{controller}/{action}/",
                    defaults: new { area = "User", controller = "Profile", action = "index" });


                if (IsProduction())
                {
                    endpoints.MapHangfireDashboard("/dashboard", new DashboardOptions
                    {
                        Authorization = new[] { new MyAuthorizationFilter() },
                        IsReadOnlyFunc = (DashboardContext context) => true
                    });
                }


            });

        }
        private bool IsProduction()
        {
            // For QA and Prod env's add HangFire with out config dependency
            if (!CurrentEnvironment.IsDevelopment())
                return true;
            // For Dev environment adding HangFire conditionally 
            // Ignoring return value since, the result will have 'true' 
            // only if the parsing is success and config has 'true'
            //bool.TryParse(Configuration["HangFire:RunJobsOnDev"], out bool result);
            bool result = false;
            return result;
        }

    }


}

