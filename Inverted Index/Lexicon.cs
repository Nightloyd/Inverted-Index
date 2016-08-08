using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace Inverted_Index {
    [DataContract]
    public class Lexicon {
        [DataMember]
        private ConcurrentDictionary<String, Posts> lex;

        public Lexicon() {
            lex = new ConcurrentDictionary<string, Posts>();
        }

        public Posts GetTermPosts(String term) {
            Posts posts;
            lex.TryGetValue(term, out posts);
            return posts;

        }

        public void AddPost(String term, int docId, int docFrequency) {
            if (lex.ContainsKey(term)) { // If term already exist
                lex[term].AddPost(docId, docFrequency); // add document id and frequency to the list of posts.
            } else {
                lex.TryAdd(term, new Posts(docId, docFrequency)); // Else add the term and create a new posts object.
            }
        }

        public void RemovePost(String term, int docId) {
            Posts posts;
            if (lex.TryGetValue(term, out posts)) { // Gets the posts of term.
                posts.RemovePost(docId); // Remove the document from the term.

                if (posts.IsEmpty()) { // If the term have zero posts,
                    Posts value;
                    lex.TryRemove(term, out value); // then delete the term from the lexicon.
                }
            }
        }
    }
}
