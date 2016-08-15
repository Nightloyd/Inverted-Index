using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Inverted_Index {
    [DataContract]
    public class Posts {
        [DataMember]
        private ConcurrentDictionary<String, int> posts;

        public Posts(String docId, int frequency) {
            posts = new ConcurrentDictionary<String, int>();
            AddPost(docId, frequency);
        }

        public ConcurrentDictionary<String, int> GetPosts() {
            return posts;
        }

        public void AddPost(String docKey, int frequency) {
            posts.TryAdd(docKey, frequency);
        }

        public void RemovePost(String docKey) {
            int value;
            posts.TryRemove(docKey, out value);
        }

        public bool IsEmpty() {
            return posts.IsEmpty;
        }
    }
}
