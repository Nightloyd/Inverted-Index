using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inverted_Index {
    class Document {
        private String indexedString;
        private Dictionary<String, String> storedStrings
        {
            get;
        }

        public Document(String indexedString, Dictionary<String, String> storedStrings) {
            this.indexedString = indexedString;
            this.storedStrings = storedStrings;
        }
    }
}
