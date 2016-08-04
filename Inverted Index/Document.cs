using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inverted_Index {
    class Document {
        private readonly String indexedString;
        private readonly Dictionary<String, String> storedStrings;

        public Document(String indexedString, Dictionary<String, String> storedStrings) {
            this.indexedString = indexedString;
            this.storedStrings = storedStrings;
        }

        public String GetIndexedString() {
            return indexedString;
        }

        public Dictionary<String, String> GetStoredStrings() {
            return storedStrings;
        }
    }
}
