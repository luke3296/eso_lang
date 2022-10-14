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

	    tokenRegexs = new List<Token>();

            tokenRegexs.Add(new Token(1, "symbol", "ya scally wag"));
            tokenRegexs.Add(new Token(2, "symbol", "some text"));
            tokenRegexs.Add(new Token(3, "symbol", "might contain"));
            tokenRegexs.Add(new Token(4, "a token", "might"));
            tokenRegexs.Add(new Token(5, "a token", "hello"));
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
                    Console.WriteLine("lookin for mathch in fragent: " + string.Join("", fragment));
                    if (t.match(string.Join("", fragment)))
                    {
                        Console.WriteLine("matched with: " + t.Name);
                        tokens.Add(new Token(t.Id, t.Name, t.RE));
                        tokenSymbols.Add(string.Join("", fragment));
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
