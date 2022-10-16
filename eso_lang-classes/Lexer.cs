using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using eso_lang_classes;
// Regex tok1 = new Regex("Hello Sailor");

namespace eso_lang_classes
{

    public class Lexer
    {
        const int MAX_LEN = 32;
        public int[] Tokens;
        public string[] Symbols;
        char[] tmp;
        List<Token> tokenRegexs;
        public Lexer()
        {
            Tokens = new int[MAX_LEN];
            Symbols = new string[MAX_LEN];
            tmp = new char[MAX_LEN];

            tokenRegexs = new List<Token>();

            tokenRegexs.Add(new Token(1, "Int", @"Naught\s", 0));
            tokenRegexs.Add(new Token(1, "Int", @"Boots\s", 1));
            tokenRegexs.Add(new Token(1, "Int", @"Goats\s", 2));
            tokenRegexs.Add(new Token(1, "Int", @"Powder\s", 3));
            tokenRegexs.Add(new Token(1, "Int", @"Rum\s", 4));
            tokenRegexs.Add(new Token(1, "Int", @"Meats\s", 5));
            tokenRegexs.Add(new Token(1, "Int", @"Wines\s", 6));
            tokenRegexs.Add(new Token(1, "Int", @"Cloth\s", 7));
            tokenRegexs.Add(new Token(1, "Int", @"Ropes\s", 8));
            tokenRegexs.Add(new Token(1, "Int", @"Food\s", 9));
            tokenRegexs.Add(new Token(2, "Addition-Operator", @"Put\swith\s"));
            tokenRegexs.Add(new Token(3, "Subtraction-Operator", @"Take\sfrom\s"));
            tokenRegexs.Add(new Token(4, "Division-Operator", @"By\scount\sof\s"));
            tokenRegexs.Add(new Token(5, "Multiplication-Operator", @"By\scount\sper\s"));
            tokenRegexs.Add(new Token(6, "Asignment-Operator", @"Let\sthe\s"));
            tokenRegexs.Add(new Token(6, "Asignment-Operator", @"Have\s"));
            tokenRegexs.Add(new Token(7, "Lable", @"_[a-zA-Z]*\s"));
            // any sting of letters and numbers starting with lowercase letter

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


        public List<Token> Lex(string source)
        {
            List<Token> tokens = new List<Token>();
            List<char> fragment = new List<char>();
            List<string> tokenSymbols = new List<string>();
            for (int i = 0; i < source.Length; i++)
            {
                fragment.Add((source[i]));
                foreach (Token t in tokenRegexs)
                {
                    //Console.WriteLine("lookin for mathch in fragent: " + string.Join("", fragment));
                    var match = t.match_str(string.Join("", fragment));
                    if (match != "")
                    {
                        Console.WriteLine("matched with: " + match);
                        tokens.Add(new Token(t.id, t.name, t.tokenRE));
                        tokenSymbols.Add(match);
                        fragment.Clear();
                    }
                }
            }
            return tokens;
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
