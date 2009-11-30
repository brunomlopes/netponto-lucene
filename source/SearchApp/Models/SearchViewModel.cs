using System.Collections.Generic;

namespace SearchApp.Models
{
    public class SearchViewModel
    {
        public IEnumerable<DocumentViewModel> Documents { get; private set; }

        public SearchViewModel(IEnumerable<DocumentViewModel> documents)
        {
            Documents = documents;
        }
    }
}