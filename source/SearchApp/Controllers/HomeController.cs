using System.Collections.Generic;
using System.Web.Mvc;
using Common;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using SearchApp.Models;

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

            var numberOfResults = 10;
            var top10Results = searcher.Search(queryanalizer.Parse(query), numberOfResults);
            var docs = new List<DocumentViewModel>();
            foreach (var scoreDoc in top10Results.scoreDocs)
            {
                var document = searcher.Doc(scoreDoc.doc);
                var title = document.GetField(Configuration.Fields.Title).StringValue();
                var text = document.GetField(Configuration.Fields.Text).StringValue();
                docs.Add(new DocumentViewModel(title, text));
            }
            return View(new SearchViewModel(docs));
        }

       

        
    }
}
