using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Inverted_Index {
    public class Index {
        private Lexicon lex; // Lexicon containing all words in all documents in the index.
        private ConcurrentDictionary<int, Document> docs; // All documents in the index.

        public Index() {
            lex = new Lexicon();
            docs = new ConcurrentDictionary<int, Document>();
        }

        private Index(Lexicon lex, ConcurrentDictionary<int, Document> docs) {
            this.lex = lex;
            this.docs = docs;
        }

        public List<Object> Search(String query) {
            Posts posts = lex.GetTermPosts(query);
            if (posts != null) {
                foreach (KeyValuePair<int, int> post in posts.GetPosts()) {
                    Debug.WriteLine("Indexed String: \"" + docs[post.Key].GetIndexedString()[0] + "\" Frequency of search term: " + post.Value);
                }
            } else {
                Debug.WriteLine("Nothing found : (");
            }

            return null;
        }

        public int AddDoc(String stringToIndex, Dictionary<String, String> stringsToStore) {

            #region Add Document to Document Dictionary
            String stringToIndexLowerCase = stringToIndex.ToLower();
            Document doc = new Document(stringToIndexLowerCase, new Dictionary<string, string>(stringsToStore));
            int docPosition = docs.Count;
            docs.TryAdd(docPosition, doc);

            #endregion

            #region Add Document terms to the index
            Dictionary<String, int> termFrequency = new Dictionary<string, int>(); // Contains all terms with their respective frequency.
            foreach (String str in stringToIndexLowerCase.Split(' ')) { // Splits input String that will be indexed.
                if (termFrequency.ContainsKey(str)) { // Checks if the Dictonary already contains given term
                    termFrequency[str]++; // if so +1
                } else {
                    termFrequency.Add(str, 1); // else add term to Dictonary with count 1.
                }
            }

            foreach (KeyValuePair<String, int> term in termFrequency) { // Goes through all of the terms
                lex.AddPost(term.Key, docPosition, term.Value);
            }

            #endregion

            return docPosition;
        }

        public void UpdateDoc(int id, String updatedStringToIndex, Dictionary<String, String> updatedStringsToStore) {
            RemoveDoc(id);
            AddDoc(updatedStringToIndex, updatedStringsToStore);
        }

        public void RemoveDoc(int id) {
            Document doc;
            if (docs.TryGetValue(id, out doc)) { // Gets document of a id.
                foreach (String term in doc.GetIndexedString()) { // Splits the indexed string from the document.
                    // Loops through all terms of the document.
                    lex.RemovePost(term, id);
                }
            }
        }

        public void SaveIndexToFile(String path) {
            FileHandler.SaveToXML<ConcurrentDictionary<int, Document>>(path, docs);
            FileHandler.SaveToXML<Lexicon>(path, lex);
        }

        public static Index LoadIndexFromFile(String path) {
            return new Index(FileHandler.LoadFromXML<Lexicon>(path), FileHandler.LoadFromXML<ConcurrentDictionary<int, Document>>(path));
        }
    }
}
