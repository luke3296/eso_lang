using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eso_Lang
{
    public class Lexer
    {
        const int MAX_LEN = 32;
        public int[] Tokens;
        public string[] Symbols;
        char[] tmp;
        List<Token> tokenRegexs;
        public Lexer(List<Token> toks)
        {
            Tokens = new int[MAX_LEN];
            Symbols = new string[MAX_LEN];
            tmp = new char[MAX_LEN];

            tokenRegexs = toks;

            // any sting of letters and numbers starting with lowercase letter

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
