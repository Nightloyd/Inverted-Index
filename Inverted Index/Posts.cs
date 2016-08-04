using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inverted_Index {
    class Posts {
        private ConcurrentDictionary<int, int> posts
        {
            get
            {
                return posts;
            }
        }

        public void addPost(int docId, int frequency) {
            posts.TryAdd(docId, frequency);
        }
    }
}
