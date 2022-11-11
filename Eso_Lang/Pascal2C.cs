using System;
using System.Collections.Generic;
using System.Text;

namespace Eso_Lang
{
    class Pascal2C
    {
        List<Token> Tokens;
        public Pascal2C(List<Token> tokes)
        {
            this.Tokens = tokes;
        }

        //pass the tokens if( .. here ..)
        string genIf(Token[] tokes)
        {
            StringBuilder cstring = new StringBuilder("if(");
            foreach (Token t in tokes)
            {
                switch (t.id)
                {
                    case (int)TOKENSPASCAL.T_NR:
                        cstring.Append(t.intval);
                        break;
                    case (int)TOKENSPASCAL.T_LTHAN:
                        cstring.Append("<");
                        break;
                    case (int)TOKENSPASCAL.T_GTHAN:
                        cstring.Append(">");
                        break;
                    case (int)TOKENSPASCAL.T_LTHANEQ:
                        cstring.Append("<=");
                        break;
                    case (int)TOKENSPASCAL.T_GTHANEQ:
                        cstring.Append(">=");
                        break;
                    case (int)TOKENSPASCAL.T_EQUAL:
                        cstring.Append("==");
                        break;
                    case (int)TOKENSPASCAL.T_NOTEQUAL:
                        cstring.Append("!=");
                        break;
                    case (int)TOKENSPASCAL.T_OR:
                        cstring.Append("||");
                        break;
                    case (int)TOKENSPASCAL.T_AND:
                        cstring.Append("&&");
                        break;
                    case (int)TOKENSPASCAL.T_IDENT:
                        cstring.Append(t.stringval);
                        break;
                    default:
                        Console.WriteLine("code gen if found a token that was'nt matched " + t.name);
                        break;
                }
            }
            cstring.Append(")");
            return cstring.ToString();
        }

        //pass the tokens begin .. here .. end 
        string genBlock(Token[] tokes)
        {
            StringBuilder cstring = new StringBuilder("{");
            foreach (Token t in tokes)
            {
                switch (t.id)
                {
                    case (int)TOKENSPASCAL.T_ASSIGN:
                        cstring.Append(t.intval);
                        break;
                    case (int)TOKENSPASCAL.T_WRITELINE:
                        cstring.Append(" handle a writeline ");
                        break;
                    default:
                        break;
                }
            }
            cstring.Append("}");
            return cstring.ToString();
        }

        string genWriteLine(Token[] tokes)
        {
            StringBuilder cstring = new StringBuilder("{");
            foreach (Token t in tokes)
            {
                switch (t.id)
                {
                    case (int)TOKENSPASCAL.T_STRING:
                        cstring.Append(t.intval);
                        break;
                    default:
                        break;
                }
            }
            cstring.Append("}");
            return cstring.ToString();
        }

    }
}
    