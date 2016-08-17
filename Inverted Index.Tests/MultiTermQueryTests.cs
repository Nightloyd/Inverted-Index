using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inverted_Index.Tests {
    [TestClass]
    public class MultiTermQueryTests {
        private Index index;


        [TestInitialize]
        public void TestInit() {
            index = new Inverted_Index.Index();
            Dictionary<String, String> stringsToStore = new Dictionary<string, string>();

            stringsToStore.Add("Name", "Intel i7-6600k");
            stringsToStore.Add("Sku", "6600k");


            index.AddDoc(index.GetNoOfDocuments().ToString(), "Carrots should be orange and bananas should be yellow", stringsToStore);
            index.AddDoc(index.GetNoOfDocuments().ToString(), "Testing the index should work", stringsToStore);
            index.AddDoc(index.GetNoOfDocuments().ToString(), "Lots of testing just testing", stringsToStore);
            index.AddDoc(index.GetNoOfDocuments().ToString(), "Need more bananas", stringsToStore);
            index.AddDoc(index.GetNoOfDocuments().ToString(), "Too many carrots", stringsToStore);
            index.AddDoc(index.GetNoOfDocuments().ToString(), "Do not mix carrots and bananas", stringsToStore);
            index.AddDoc(index.GetNoOfDocuments().ToString(), "The index should be quick", stringsToStore);
            index.AddDoc(index.GetNoOfDocuments().ToString(), "But maybe not as quick as light", stringsToStore);
            index.AddDoc(index.GetNoOfDocuments().ToString(), "But way faster than the Bolt", stringsToStore);
            index.AddDoc(index.GetNoOfDocuments().ToString(), "So we should strive for a 100m world record", stringsToStore);
        }

        [TestMethod]
        public void SearchWithTwoTermsShouldGiveFiveResults() {
            var result = index.Search(new MultiTermQuery("should carrots", ' ', 5));
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void SearchWithTwoTermsAndUpToLengthPlusOneResultsShouldNotGoOutOfBounds() {
            index.Search(new MultiTermQuery("should carrots", ' ', index.GetNoOfDocuments()+1));
        }
    }
}
