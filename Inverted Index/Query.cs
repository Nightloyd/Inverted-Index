using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Inverted_Index {
    public interface Query {
        List<Document> Search(Lexicon lex, ConcurrentDictionary<String, Document> docs);
    }
}
