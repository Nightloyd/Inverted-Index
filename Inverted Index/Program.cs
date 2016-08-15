﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Inverted_Index {
    class Program {
        static void Main(string[] args) {

            // 1
            Index index = new Inverted_Index.Index();
            
            Dictionary<String, String> stringsToStore = new Dictionary<string, string>();
            bool success = index.AddDoc("1", "Carrots should be orange and bananas should be yellow", stringsToStore);
            success = index.AddDoc("2", "Testing the index should work", stringsToStore);
            index.Search("should");
            //index.RemoveDoc(id);
            index.Search("should");
            
            index.Search("index");
            //index.SaveIndexToFile("E:\\");


            // 2
            /*Index index = Index.LoadIndexFromFile("E:\\");
            index.Search("should");
            index.Search("should");
            index.Search("index");*/
        }
    }
}
