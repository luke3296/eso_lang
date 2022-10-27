using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eso_Lang
{
    class Pascal2c_Code_Gen
    {
        List<Token> Tokens;

        public Pascal2c_Code_Gen(List<Token> toks) {
            this.Tokens = toks;
        }

        public string generate() {
            List<char> c_source = new List<char>();

            c_source.AddRange(Program());

            var c_source_str = String.Concat(c_source);
            return c_source_str;
        }

        private string Program() {
            return "program";
        }
    }
}
