using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Inverted_Index {
    class Index {
        private ConcurrentDictionary<String, Posts> dic; // Dictionary containing all words in all documents in the index.
        private ConcurrentDictionary<int, Document> docs; // All documents in the index.

        public Index() {
            dic = new ConcurrentDictionary<string, Posts>();
            docs = new ConcurrentDictionary<int, Document>();
        }

        public List<Object> Search(String query) {
            Posts posts;
            if (dic.TryGetValue(query, out posts)) {
                foreach (KeyValuePair<int, int> post in posts.GetPosts()) {
                    Debug.WriteLine("Indexed String: \"" + docs[post.Key].GetIndexedString() + "\" Frequency of search term: " + post.Value);
                }
            }


            return null;
        }

        // TO-DO: Remove characters such as ., from stringToIndex?
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
                if (dic.ContainsKey(term.Key)) { // If term already exist
                    dic[term.Key].AddPost(docPosition, term.Value); // add document id and frequency to the list of posts.
                } else {
                    dic.TryAdd(term.Key, new Posts(docPosition, term.Value)); // Else add the term and create a new posts object.
                }
            }

            #endregion

            return docPosition;
        }

        public void UpdateDoc(int id, String updatedStringToIndex, Dictionary<String, String> updatedStringsToStore) {

        }

        public void RemoveDoc(int id) {

        }
    }
}
