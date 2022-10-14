using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
// Regex tok1 = new Regex("Hello Sailor");

namespace eso_lang
{

    class Lexer
    {
        const int MAX_LEN = 32;
        public int[] Tokens;
        public string[] Symbols;
        char[] tmp;

        public Lexer()
        {
            Tokens = new int[MAX_LEN];
            Symbols = new string[MAX_LEN];
            tmp = new char[MAX_LEN];
        }
        public enum TOKENS
        {
            T_PLUS = 1,
            T_MINUS = 2,
            T_DIVIDE = 3,
            T_MULTIPLY = 4,
            T_IDENT = 5,
            T_LPAR = 6,
            T_RPAR = 7,
            T_NR = 8
        }


        public int Lex(string s)
        {
            int tok_i = 0;
            for (; tok_i < s.Length; ++tok_i)
            {
                if (s[tok_i] == '\n') break;
                switch (s[tok_i])
                {
                    case ' ':
                        break;
                    case '+':
                        Tokens[tok_i++] = (int)TOKENS.T_PLUS;
                        break;
                    case '-':
                        Tokens[tok_i++] = (int)TOKENS.T_MINUS;
                        break;
                    case '*':
                        Tokens[tok_i++] = (int)TOKENS.T_MULTIPLY;
                        break;
                    case '/':
                        Tokens[tok_i++] = (int)TOKENS.T_DIVIDE;
                        break;
                    case '(':
                        Tokens[tok_i++] = (int)TOKENS.T_LPAR;
                        break;
                    case ')':
                        Tokens[tok_i++] = (int)TOKENS.T_RPAR;
                        break;
                    default: //entered a number or varible name
                        if (Char.IsDigit(s[tok_i]))
                        {
                            int i = 0;
                            while (Char.IsDigit(s[tok_i]))
                            {

                                tmp[i++] = s[tok_i];
                                ++tok_i;
                            }
                            tmp[i] = '\0';
                            Tokens[tok_i] = (int)TOKENS.T_NR;
                            Symbols[tok_i++] = new string(tmp);
                            tok_i--;
                        }
                        else if (Char.IsLetter(s[tok_i]))
                        {
                            int i = 0;
                            while (Char.IsLetter(s[tok_i]))
                            {
                                tmp[i++] = s[tok_i];
                                ++tok_i;
                            }
                            tmp[i] = '\0';
                            Tokens[tok_i] = (int)TOKENS.T_IDENT;
                            Symbols[tok_i++] = new string(tmp);
                            tok_i--;
                        }
                        else
                        {
                            Console.WriteLine("illegal token entered {0}", s[tok_i]);
                        }
                        break;
                }
            }

            return 0;
        }
        public void printTables()
        {
            for (int i = 0; i < Tokens.Length; i++)
            {

                Console.Write(Tokens[i]);
                Console.Write(" ");
                Console.Write(Symbols[i]);
            }
        }
    }
}
