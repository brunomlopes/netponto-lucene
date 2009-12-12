using System;
using System.Collections.Generic;
using System.IO;
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
                var id = document.GetField(Configuration.Fields.ID).StringValue();
                var wikiDoc = WikiDocument.FromId(id);
               
                docs.Add(new DocumentViewModel(wikiDoc.Title, wikiDoc.Text));
            }
            return View(new SearchViewModel(docs));
        }

        
        
        class WikiDocument
        {
            public string Title { get; private set; }
            public string Text { get; private set; }

            private WikiDocument(string title, string text)
            {
                Title = title;
                Text = text;
            }

            public static WikiDocument FromId(string id)
            {
                var fileContents = System.IO.File.ReadAllText(id);
                return new WikiDocument(Path.GetFileName(id), fileContents);
            }
        }
        
    }
}
