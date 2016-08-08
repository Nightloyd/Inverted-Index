using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Inverted_Index {
    class FileHandler {
        public static void SaveToXML<T>(String path, T obj) {
            /*TextWriter w = null;

            try {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                w = new StreamWriter(path, false);
                ser.Serialize(w, obj);
            } finally {
                if (w != null) {
                    w.Close();
                }
            }*/
            DataContractSerializer s = new DataContractSerializer(typeof(T));
            using (FileStream fs = File.Open(path + typeof(T).Name + ".xml", FileMode.Create)) {
                s.WriteObject(fs, obj);
            }
        }

        public static T LoadFromXML<T>(String path) where T : new() {
            TextReader r = null;
            try {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                r = new StreamReader(path);
                return (T)ser.Deserialize(r);
            } finally {
                if (r != null) {
                    r.Close();
                }
            }
        }
    }
}

