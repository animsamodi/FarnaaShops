using Newtonsoft.Json;

namespace EShop.Core.ViewModels.Seo
{
    public class PageStructureViewModel
    {
        public PageStructureViewModel()
        {
        }
        public PageStructureViewModel(string text,string metaTitle, string metaDescription, bool isNoIndex, string metaKeywords, string schema, string ogTitle)
        {
            Text = text;
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            IsNoIndex = isNoIndex;
            MetaKeywords = metaKeywords;
            Schema = schema;
            OgTitle = ogTitle;
        }

        public PageStructureViewModel(string ogTitle, string metaTitle, string metaDescription, string schema)
        {
            OgTitle = ogTitle;
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            Schema = schema;
        }
        public PageStructureViewModel(string pageTitle,string ogTitle, string metaTitle, string metaDescription, string schema)
        {
            OgTitle = ogTitle;
            PageTitle = pageTitle;
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            Schema = schema;
        }
        public PageStructureViewModel(string pageTitle,string ogTitle, string metaTitle, string metaDescription, string schema, string faqSchema)
        {
            OgTitle = ogTitle;
            PageTitle = pageTitle;
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            Schema = schema;
            FAQSchema = faqSchema;
        }
        [JsonConstructor]
        public PageStructureViewModel(
            string ogTitle
            , string pageTitle
            , string text
            , string metaTitle
            , string metaDescription
            , bool isNoIndex
             , string metaKeywords
            , string schema
            , string enTitle
            , string banner
            , string bannerMobile
            , string bannerUrl
            , string fAQSchema
            )
        {
            OgTitle = ogTitle;
            PageTitle = pageTitle;
            Text = text;
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            IsNoIndex = isNoIndex;
            MetaKeywords = metaKeywords;
            Schema = schema;
            EnTitle =enTitle;
            Banner = banner;
            BannerMobile = bannerMobile;
            BannerUrl = bannerUrl;
            FAQSchema = fAQSchema;
        }

        public string OgTitle { get; set; }
        /// <summary>
        /// description of page for example description text of category 
        /// </summary>
        public string PageTitle { get; set; }
        public string Text { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public bool IsNoIndex { get; set; }
        public string MetaKeywords { get; set; }
        public string Schema { get; set; }
        public string EnTitle { get; set; }
        public string Banner { get; set; }
        public string BannerMobile { get; set; }
        public string BannerUrl { get; set; }
        public string FAQSchema { get; set; }

    }
}