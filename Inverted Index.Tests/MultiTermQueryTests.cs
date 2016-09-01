using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inverted_Index.Tests {
    [TestClass]
    public class MultiTermQueryTests {
        private Index index;

        //Stored: Id, Price, Name, Sku, ShortDescription
        //Analyzed: Name, ShortDescription, FullDescription, Sku, Price
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

            //Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
            int textLength = 20;
            int noOfDocuments = 100000;

            for (int i = 0; i < noOfDocuments; i++) {
                stringsToStore = new Dictionary<string, string>();
                StringBuilder sb = new StringBuilder();

                String name = RandomString(5, chars) + " " + RandomString(5, chars); // Name
                String sku = RandomString(5, chars); // Sku
                String Price = StaticRandom.Rand(1000).ToString();
                String ShortDescription = RandomString(30, chars);
                String FullDescription = RandomString(100, chars);

                stringsToStore.Add("Id", i.ToString());
                stringsToStore.Add("Price", Price);
                stringsToStore.Add("Name", name);
                stringsToStore.Add("Sku", sku);
                stringsToStore.Add("ShortDescription", ShortDescription);

                sb.Append(name)
                    .Append(" ")
                    .Append(ShortDescription)
                    .Append(" ")
                    .Append(FullDescription)
                    .Append(" ")
                    .Append(sku)
                    .Append(" ")
                    .Append(Price)
                    .Append(" ");

                index.AddDoc(i.ToString(), sb.ToString(), stringsToStore);

                //index.AddDoc(index.GetNoOfDocuments().ToString(), new string(Enumerable.Repeat(chars, textLength)
                //.Select(s => s[random.Next(s.Length)]).ToArray()), stringsToStore);
            }

            index.SaveIndexToFile("D:\\IndexTest\\TestOneHundredThousand\\");

            //index = Index.LoadIndexFromFile("D:\\IndexTest\\TestOneHundredThousand\\");
        }

        [TestMethod]
        public void SearchWithTwoTermsShouldGiveFiveResults() {
            var result = index.Search(new MultiTermQuery("should carrots", ' ', 5));
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void SearchWithTwoTermsAndUpToLengthPlusOneResultsShouldNotGoOutOfBounds() {
            index.Search(new MultiTermQuery("should carrots", ' ', index.GetNoOfDocuments() + 1));
        }

        public string RandomString(int length, string chars) {
            return new string(Enumerable.Repeat(chars, length).Select(s => s[StaticRandom.Rand(s.Length)]).ToArray());
        }
    }

    public static class StaticRandom {
        static int seed = Environment.TickCount;

        static readonly ThreadLocal<Random> random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        public static int Rand(int length) {
            return random.Value.Next(length);
        }
    }
}
