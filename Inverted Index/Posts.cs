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
        private ConcurrentDictionary<int, int> posts;

        public Posts(int docId, int frequency) {
            posts = new ConcurrentDictionary<int, int>();
            AddPost(docId, frequency);
        }

        public ConcurrentDictionary<int, int> GetPosts() {
            return posts;
        }

        public void AddPost(int docId, int frequency) {
            posts.TryAdd(docId, frequency);
        }

        public void RemovePost(int id) {
            int value;
            posts.TryRemove(id, out value);
        }

        public bool IsEmpty() {
            return posts.IsEmpty;
        }
    }
}
