using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inverted_Index.Exceptions {
    class NoFileFoundAtPath : Exception {
        public NoFileFoundAtPath() {

        }

        public NoFileFoundAtPath(String msg) : base(msg) {

        }

        public NoFileFoundAtPath(String msg, Exception e) : base(msg, e) {

        }
    }
}
