using System.IO;
using Common;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace Indexer
{
    class Program
    {
        public static string[] PortugueseStopWords =
            {
                "a", "à", "ainda", "alem", "ambas", "ambos", "antes",
                "ao", "aonde", "aos", "apos", "aquele", "aqueles",
                "as", "assim", "com", "como", "contra", "contudo",
                "cuja", "cujas", "cujo", "cujos", "da", "das", "de",
                "dela", "dele", "deles", "demais", "depois", "desde",
                "desta", "deste", "dispoe", "dispoem", "diversa",
                "diversas", "diversos", "do", "dos", "durante", "e", "é", 
                "ela", "elas", "ele", "eles", "em", "entao", "entre",
                "essa", "essas", "esse", "esses", "esta", "estas",
                "este", "estes", "ha", "isso", "isto", "logo", "mais",
                "mas", "mediante", "menos", "mesma", "mesmas", "mesmo",
                "mesmos", "na", "nas", "nao", "não", "nas", "nem", "nesse", "neste",
                "nos", "no", "o", "os", "ou", "outra", "outras", "outro", "outros",
                "pela", "pelas", "pelo", "pelos", "perante", "pois", "por",
                "porque", "portanto", "proprio", "propios", "quais", "qual",
                "qualquer", "quando", "quanto", "que", "quem", "quer", "se",
                "seja", "sem", "sendo", "seu", "seus", "sob", "sobre", "sua",
                "suas", "tal", "tambem", "teu", "teus", "toda", "todas", "todo",
                "todos", "tua", "tuas", "tudo", "um", "uma", "umas", "uns"
            };

        static void Main(string[] args)
        {
            var indexWriter = new IndexWriter(Configuration.IndexDirectory,
                                              new StandardAnalyzer(PortugueseStopWords),
                                              true,
                                              IndexWriter.MaxFieldLength.UNLIMITED);

            foreach(var filepath in Directory.GetFiles(Configuration.FilesDirectory, "*.txt"))
            {
                var document = new Document();
                var title = Path.GetFileNameWithoutExtension(filepath);

                var idField = new Field(Configuration.Fields.ID,
                                        filepath, Field.Store.YES, Field.Index.NO);
                document.Add(idField);

                var titleField = new Field(Configuration.Fields.Title,
                                           title,
                                           Field.Store.COMPRESS, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
                
                document.Add(titleField);

                var textField = new Field(Configuration.Fields.Text,
                                          new StreamReader(filepath).ReadToEnd(),
                                          Field.Store.COMPRESS, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
                document.Add(textField);

                indexWriter.AddDocument(document);
            }
            indexWriter.Close();
        }
    }
}
