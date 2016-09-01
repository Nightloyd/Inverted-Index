using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Inverted_Index {
    [DataContract]
    public class Document {
        //private readonly String indexedString;
        [DataMember]
        private readonly String[] indexedString;
        [DataMember]
        private readonly Dictionary<String, String> storedStrings;

        public Document(String indexedString, Dictionary<String, String> storedStrings) {
            this.indexedString = indexedString.Split(' ');
            //this.indexedString = indexedString;
            this.storedStrings = storedStrings;
        }

        public String[] GetIndexedString() {
            return indexedString;
        }

        public Dictionary<String, String> GetStoredStrings() {
            return storedStrings;
        }

        public String Get(String name) {
            String value;
            storedStrings.TryGetValue(name, out value);
            return value;
        }
    }
}
