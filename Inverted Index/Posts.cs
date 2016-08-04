using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inverted_Index {
    class Posts {
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
    }
}
