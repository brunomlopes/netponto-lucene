using System.IO;
using Common;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace Indexer
{
    class Program
    {
        static void Main(string[] args)
        {
            var indexWriter = new IndexWriter(Configuration.IndexDirectory,
                                              new StandardAnalyzer(),
                                              true,
                                              IndexWriter.MaxFieldLength.UNLIMITED);

            foreach(var filepath in Directory.GetFiles(Configuration.FilesDirectory, "*.txt"))
            {
                var document = new Document();
                var title = Path.GetFileNameWithoutExtension(filepath);

                var titleField = new Field(Configuration.Fields.Title,
                                           new StringReader(title));
                document.Add(titleField);

                var textField = new Field(Configuration.Fields.Text,
                                          new StreamReader(filepath));
                document.Add(textField);

                indexWriter.AddDocument(document);
            }
            indexWriter.Close();
        }
    }
}
