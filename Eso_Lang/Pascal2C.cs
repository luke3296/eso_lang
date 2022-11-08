using System;
using System.Collections.Generic;
using System.Text;

namespace Eso_Lang
{
    class Pascal2C
    {
        List<Token> Tokens;
        public Pascal2C(List<Token> tokes) {
            this.Tokens = tokes;
        }
        string genIf(Token[] tokes) {
            StringBuilder cstring = new StringBuilder("if(");
            foreach (Token t in tokes) {
                switch (t.id){
                    case (int)TOKENSPASCAL.T_NR:
                            cstring.Append(t.intval);
                            break;
                    case (int)TOKENSPASCAL.T_LTHAN:
                            cstring.Append("<");
                            break;
                    default:
                        Console.WriteLine("code gen if found a token that was'nt matched " + t.name);
                            break;
                }
            }
            return cstring.ToString();

        }
    }
}
    