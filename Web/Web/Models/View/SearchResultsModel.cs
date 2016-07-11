using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.View
{
    public class SearchResultsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<string> Cause { get; set; }

        public List<string> Subjects { get; set; }

        public List<string> Level { get; set; }

        public string AddedBy { get; set; }

        public DateTime Date { get; set; }
    }
}