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
        private ConcurrentDictionary<String, Document> docs; // All documents in the index.

        public Index() {
            lex = new Lexicon();
            docs = new ConcurrentDictionary<String, Document>();
        }

        private Index(Lexicon lex, ConcurrentDictionary<String, Document> docs) {
            this.lex = lex;
            this.docs = docs;
        }

        public List<Document> Search(Query query) {
            return query.Search(lex, docs);
        }

        public bool AddDoc(String key, String stringToIndex, Dictionary<String, String> stringsToStore) {

            if (!docs.ContainsKey(key)) {
                
                String stringToIndexLowerCase = stringToIndex.ToLower();

                #region Add Document to Document Dictionary
                Document doc = new Document(stringToIndexLowerCase, new Dictionary<string, string>(stringsToStore));
                //int docPosition = docs.Count;
                docs.TryAdd(key, doc);

                #endregion

                #region Add Document terms to the index

                Dictionary<String, int> termFrequency = new Dictionary<string, int>();
                    // Contains all terms with their respective frequency.
                foreach (String str in stringToIndexLowerCase.Split(' ')) {
                    // Splits input String that will be indexed.
                    if (termFrequency.ContainsKey(str)) {
                        // Checks if the Dictonary already contains given term
                        termFrequency[str]++; // if so +1
                    }
                    else {
                        termFrequency.Add(str, 1); // else add term to Dictonary with count 1.
                    }
                }

                foreach (KeyValuePair<String, int> term in termFrequency) {
                    // Goes through all of the terms
                    lex.AddPost(term.Key, key, term.Value);
                }

                #endregion

                return true;
            }
            return false;
        }

        public void UpdateDoc(String key, String updatedStringToIndex, Dictionary<String, String> updatedStringsToStore) {
            RemoveDoc(key);
            AddDoc(key, updatedStringToIndex, updatedStringsToStore);
        }

        public void RemoveDoc(String key) {
            Document doc;
            if (docs.TryRemove(key, out doc)) { // Gets document of a id.
                foreach (String term in doc.GetIndexedString()) { // Splits the indexed string from the document.
                    // Loops through all terms of the document.
                    lex.RemovePost(term, key);
                }
            }
            
        }

        public int GetNoOfDocuments() {
            return docs.Count;
        }

        public void SaveIndexToFile(String path) {
            FileHandler.SaveToXML<ConcurrentDictionary<String, Document>>(path, docs);
            FileHandler.SaveToXML<Lexicon>(path, lex);
        }

        public static Index LoadIndexFromFile(String path) {
            return new Index(FileHandler.LoadFromXML<Lexicon>(path), FileHandler.LoadFromXML<ConcurrentDictionary<String, Document>>(path));
        }
    }
}
