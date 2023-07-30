namespace EShop.Core.ExtensionMethods
{
    public static class Paging
    {
        public static string GetPaging(this int pagecount, int pagenumber, string classname)
        {
            string str = "";
            if (pagecount > 1)
            {
                str += "<div class='c-pager'><ul class='c-pager_items'>";
                if (pagecount > 10)
                {
                        if (pagenumber + 5 > pagecount)
                        {
                            str += "<li><a class='c-pager_first "+classname+"' data-pagenumber='1'></a></li>";
                            for (int i = pagecount - 10; i <= pagecount; i++)
                            {
                                str += " <li><a class='c-pager_item " + (i == pagenumber ? "is-active" : classname) + "' data-pagenumber='" + i + "'>" + i + "</a> </li>";
                            }
                            if (pagecount != pagenumber)
                            {
                                str += "<li><a class='c-pager_last "+classname+"' data-pagenumber='" + pagecount + "'></a> </li>";
                            }
                        }
                        else
                        {
                            str += "<li><a class='c-pager_first "+classname+"' data-pagenumber='1'></a></li>";
                            for (int i = pagenumber - 5; i < pagenumber + 5; i++)
                            {
                                if(i > 0)
                                str += " <li><a class='c-pager_item " + (i == pagenumber ? "is-active" : classname) + "' data-pagenumber='" + i + "'>" + i + "</a> </li>";
                            }
                            str += "<li><a class='c-pager_last "+classname+"' data-pagenumber='" + pagecount + "'></a> </li>";
                        }
                }
                else
                {
                    if (pagenumber != 1)
                    {
                        str += "<li><a class='c-pager_first "+classname+"' data-pagenumber='1'></a></li>";
                    }
                    for (int i = 1; i <= pagecount; i++)
                    {
                        str += " <li><a class='c-pager_item " + (i == pagenumber ? "is-active" : classname) + "' data-pagenumber='" + i + "'>" + i + "</a> </li>";

                    }
                    if (pagecount != pagenumber)
                    {
                        str += "<li><a class='c-pager_last "+classname+"' data-pagenumber='" + pagecount + "'></a> </li>";
                    }
                }

                str += "</ul></div>";
            }

            return str;
        }
    }
}
