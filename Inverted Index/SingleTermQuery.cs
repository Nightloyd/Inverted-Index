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
        private String searchString;

        public SingleTermQuery(String searchString) {
            this.searchString = searchString;
        }

        public List<Document> Search(Lexicon lex, ConcurrentDictionary<String, Document> docs) {
            Posts posts = lex.GetTermPosts(searchString); // Retrieves all posts from a given term.
            List<Document> returnDocuments = new List<Document>();
            if (posts != null) {
                
                foreach (KeyValuePair<String, int> post in posts.GetPosts()) {
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
