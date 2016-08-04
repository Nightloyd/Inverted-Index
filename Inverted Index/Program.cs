using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Inverted_Index {
    class Program {
        static void Main(string[] args) {
            Index index = new Inverted_Index.Index();

            Dictionary<String, String> stringsToStore = new Dictionary<string, string>();
            index.AddDoc("Carrots should be orange and bananas should be yellow", stringsToStore);
            index.Search("carrots");
        }
    }
}
