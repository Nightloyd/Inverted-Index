using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inverted_Index {
    public class MultiTermQuery : Query {
        private String[] searchTerms;
        private int maxNoOfResults;

        public MultiTermQuery(String searchString, char separator, int maxNoOfResults) {
            searchTerms = searchString.ToLower().Split(separator);
            this.maxNoOfResults = maxNoOfResults;
        }


        public List<Document> Search(Lexicon lex, ConcurrentDictionary<string, Document> docs) {
            List<Document> returnDocuments = new List<Document>();
            Dictionary<String, int> noOfTermHits = new Dictionary<string, int>();
            foreach (String term in searchTerms) { // Loop through all terms

                Posts posts = lex.GetTermPosts(term);
                if (posts != null) {
                    foreach (var post in posts.GetPosts()) { // Loops through all posts for given term.

                        if (noOfTermHits.ContainsKey(post.Key)) { // If the document key already has a term hit
                            noOfTermHits[post.Key]++; // then add 1 to that count.
                        }
                        else {
                            noOfTermHits.Add(post.Key, 1); // Otherwise add the document key to the dictionary.
                        }
                    }

                }

            }

            var sortedNoOfTermHits = noOfTermHits.ToList();
            sortedNoOfTermHits.Sort((p1, p2) => p2.Value.CompareTo(p1.Value));

            for (int i = 0; i < maxNoOfResults && i < sortedNoOfTermHits.Count; i++) { 
                Document doc;
                if (docs.TryGetValue(sortedNoOfTermHits[i].Key, out doc)) { // Retrives the Document for given post
                    returnDocuments.Add(doc); // and adds it to the List to return.
                }
            }

            return returnDocuments;
        }
    }
}
