using System.Web.Mvc;
using Common;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;

namespace SearchApp.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Search(string query)
        {
            ViewData["Message"] = "query : " + query;

            var searcher = new IndexSearcher(Configuration.IndexDirectory);

            var fieldsToSearchIn = new[] {Configuration.Fields.Title, Configuration.Fields.Text};
            var queryanalizer = new MultiFieldQueryParser(fieldsToSearchIn,
                                                          new StandardAnalyzer());

            var top10Results = searcher.Search(queryanalizer.Parse(query), 10);
            
            return View("Index");
        }
    }
}
