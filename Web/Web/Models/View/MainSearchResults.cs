using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.View
{
    public class MainSearchResults
    {
        public SearchModel SearchModel { get; set; }
        public PagedList.IPagedList<Web.Models.View.SearchResultsModel> SearchResults { get; set; }
    }
}