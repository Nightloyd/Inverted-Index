using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Inverted_Index {
    class Index {
        private ConcurrentDictionary<String, Posts> dic; // Dictionary containing all words in all documents in the index.
        private ConcurrentDictionary<int, Document> docs; // All documents in the index.

        public Index() {
            dic = new ConcurrentDictionary<string, Posts>();
        }

        public void Search(Query query) {

        }

        public int AddDoc(String stringToIndex, Dictionary<String, String> stringsToStore) {

            #region Add Document to Document Dictionary
            Document doc = new Document(stringToIndex, new Dictionary<string, string>(stringsToStore));
            int docPosition = docs.Count;
            docs.TryAdd(docPosition, doc);
            #endregion

            Dictionary<String, int> termFrequency = new Dictionary<string, int>();
            foreach (String str in stringToIndex.Split(' ')) {
                if (termFrequency.ContainsKey(str)) {
                    termFrequency[str]++;
                } else {
                    termFrequency.Add(str, 1);
                }
            }


            return docPosition;
        }

        public void UpdateDoc(int id, String updatedStringToIndex, Dictionary<String, String> updatedStringsToStore) {

        }

        public void RemoveDoc(int id) {

        }
    }
}
