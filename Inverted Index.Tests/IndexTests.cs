using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inverted_Index.Tests {
    [TestClass]
    public class IndexTests {


        [TestMethod]
        public void CreateIndex() {
            var result = new Index();
            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void AddDocumentToIndex() {
            var index = new Index();
            Dictionary<String, String> stringsToStore = new Dictionary<string, string>();

            stringsToStore.Add("Name", "Intel i7-6600k");
            stringsToStore.Add("Sku", "6600k");


            var result = index.AddDoc(index.GetNoOfDocuments().ToString(), "Carrots should be orange and bananas should be yellow", stringsToStore);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void AddSameDocumentTwiceToIndexWithSameKey() {
            var index = new Index();
            Dictionary<String, String> stringsToStore = new Dictionary<string, string>();

            stringsToStore.Add("Name", "Intel i7-6600k");
            stringsToStore.Add("Sku", "6600k");


            var result = index.AddDoc("0", "Carrots should be orange and bananas should be yellow", stringsToStore);
            Assert.AreEqual(true, result);
            result = index.AddDoc("0", "Carrots should be orange and bananas should be yellow", stringsToStore);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void AddSameDocumentTwiceToIndexWithDifferentKey() {
            var index = new Index();
            Dictionary<String, String> stringsToStore = new Dictionary<string, string>();

            stringsToStore.Add("Name", "Intel i7-6600k");
            stringsToStore.Add("Sku", "6600k");


            var result = index.AddDoc("0", "Carrots should be orange and bananas should be yellow", stringsToStore);
            Assert.AreEqual(true, result);
            result = index.AddDoc("1", "Carrots should be orange and bananas should be yellow", stringsToStore);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void DeleteDocument() {
            var index = new Index();
            Dictionary<String, String> stringsToStore = new Dictionary<string, string>();

            stringsToStore.Add("Name", "Intel i7-6600k");
            stringsToStore.Add("Sku", "6600k");


            index.AddDoc(index.GetNoOfDocuments().ToString(), "Carrots should be orange and bananas should be yellow", stringsToStore);
            index.AddDoc(index.GetNoOfDocuments().ToString(), "Testing the index should work", stringsToStore);
            var result = index.Search(new MultiTermQuery("testing", ' ', 5));
            Assert.AreEqual(1, result.Count);
            index.RemoveDoc("1");
            result = index.Search(new MultiTermQuery("testing", ' ', 5));
            Assert.AreEqual(0, result.Count);
        }
    }
}
