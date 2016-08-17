using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Inverted_Index {
    public class SingleTermQuery : Query {
        private String searchTerm;

        public SingleTermQuery(String searchTerm) {
            this.searchTerm = searchTerm.ToLower();
        }

        public List<Document> Search(Lexicon lex, ConcurrentDictionary<String, Document> docs) {
            Posts posts = lex.GetTermPosts(searchTerm); // Retrieves all posts from a given term.
            List<Document> returnDocuments = new List<Document>();
            if (posts != null) {
                var sortedList = posts.GetPosts().ToList();
                sortedList.Sort((p1, p2) => p2.Value.CompareTo(p1.Value));
                foreach (KeyValuePair<String, int> post in sortedList) {
                    Document doc;
                    if (docs.TryGetValue(post.Key, out doc)) { // Retrives the Document for given post
                        returnDocuments.Add(doc); // and adds it to the List to return.
                    }
                }
            }
            return returnDocuments;
        }
    }
}
