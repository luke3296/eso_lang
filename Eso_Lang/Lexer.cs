using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            bool token_found = false;
            for (int i = 0; i < source.Length; i++)
            {
                fragment.Add((source[i]));

                foreach (Token t in tokenRegexs)
                {
                    //Console.WriteLine("lookin for mathch in fragent: " + string.Join("", fragment));
                    var possibleMatch = t.match_str(string.Join("", fragment));
                    if (possibleMatch != "")
                    {
                        Console.WriteLine("matched with: " + possibleMatch);
                        tokens.Add(new Token(t.id, t.name, t.tokenRE));
                        tokenSymbols.Add(possibleMatch);
                        fragment.Clear();
                    }
                }
                // the lexeme wasnt found in token regex's e.g an identifie

            }
            return tokens;
        }

        public List<Token> LexPascal(string source)
        {
            int lines = 0;
            int currentChar = 0;
            List<Token> tokens = new List<Token>();
            List<char> fragment = new List<char>();
            bool done = false;
            Console.WriteLine("lexing: " + source);
            char c;

            //for( int i = 0; i< source.Length-1;i++){
            while (!done){
                if (currentChar < source.Length){ 
                    c = source[currentChar];
                }else {
                    Console.WriteLine("reached end of string");
                    break;
                }
                Console.WriteLine("got char at start : " + c);
                if (c == '\n') {
                    lines += 1;
                    currentChar++;
                } else if (c == ';') {
                    tokens.Add(new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimiter", @";"));
                    currentChar++;

                } else if (c == ',') {
                    tokens.Add(new Token((int)TOKENSPASCAL.T_COMMA, "COMMA", @"\,"));
                    currentChar++;
                    //Console.WriteLine("got: " + c);
                } else if (c == '\'') {
                    tokens.Add(new Token((int)TOKENSPASCAL.T_APOSTROPHE, "APOSTROPHE", "\'"));
                    currentChar++;
                    //Console.WriteLine("got: " + c);
                } else if (c == ' ') {
                    currentChar++;
                    //Console.WriteLine("got whitespace ");
                } else if (c == '.') {
                    currentChar++;
                    tokens.Add(new Token((int)TOKENSPASCAL.T_PERIOD, "Period", @"\."));
                    //Console.WriteLine("got period ");
                }
                else if(Char.IsDigit(c)){
                        while (Char.IsDigit(c)){
                            if (currentChar < source.Length){
                                c = source[currentChar++];
                                fragment.Add(c);
                            }else{
                                fragment.Add(c);
                                break;
                            }
                        }
                    string fragment_str = string.Join("", fragment).Remove(string.Join("", fragment).Length - 1, 1);

                    tokens.Add(new Token((int)TOKENSPASCAL.T_NR, "number", @"\d+"));
                        Console.Write(fragment_str);
                        fragment.Clear();
                    
                        //put the churernt character back to catch the non-letter 
                        currentChar--;

                }else if (Char.IsLetter(c)){
                    while (Char.IsLetter(c)){
                        if (currentChar < source.Length){
                            c = source[currentChar++];
                            fragment.Add(c);
                        }
                        else{
                            fragment.Add(c);
                            break;
                        }
                    }

                    string fragment_str = string.Join("", fragment).Remove(string.Join("", fragment).Length - 1, 1);
                    Console.WriteLine("looking for word in: " + fragment_str);
                    bool foundAmatch = false;
                    foreach (Token t in tokenRegexs){
                        var match = t.match_str(fragment_str);
                        foundAmatch = false;
                        if (match != "") {
                            Console.WriteLine("MATCH: " + fragment_str);
                            foundAmatch = true;
                            tokens.Add(new Token(t.id, t.name, t.tokenRE));
                            fragment.Clear();
                            break;
                        }
                    }
                    
                    if (!foundAmatch) {
                        Console.WriteLine("looking for id in: " + fragment_str);
                        Console.WriteLine("identifier: " + fragment_str);
                        tokens.Add(new Token((int)TOKENSPASCAL.T_IDENT, "identifier", "[a-zA-Z]+"));
                        fragment.Clear();
                    }
                    //Console.Write(fragment_str);

                    //fragment.Clear();
                    //put the churernt character back to catch the non-letter 
                    currentChar--;
                    //break;
                }else if(!Char.IsDigit(c) & !Char.IsLetter(c) &!Char.IsWhiteSpace(c)){
                    while (!Char.IsDigit(c) & !Char.IsLetter(c)&!Char.IsWhiteSpace(c)){
                        if (currentChar < source.Length)
                        {
                            c = source[currentChar++];
                            fragment.Add(c);
                        }else{
                            fragment.Add(c);
                            break;
                        }
                    }
                    string fragment_str = string.Join("", fragment).Remove(string.Join("", fragment).Length - 1, 1);
                    //string fragment_str =string.Join("", fragment);

                    Console.WriteLine("looking for symbol in: " + fragment_str);
                    bool foundAmatch = false;
                    foreach (Token t in tokenRegexs)
                    {
                        var match = t.match_str(fragment_str);
                        foundAmatch = false;
                        if (match != "")
                        {
                            Console.WriteLine("MATCH: " + fragment_str);
                            foundAmatch = true;
                            tokens.Add(new Token(t.id, t.name, t.tokenRE));
                            fragment.Clear();
                            break;
                        }
                    }

                    if(!foundAmatch){
                        Regex letter = new Regex(@"[a-zA-Z]");
                        Console.WriteLine("looking for symbols in block:"+ fragment_str);
                        switch (fragment_str.Trim())
                        {
                            case "('":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_LPAR, "LPAR", @"\("));
                                tokens.Add(new Token((int)TOKENSPASCAL.T_APOSTROPHE, "APOSTROPHE", "'"));
                                break;
                            case "')":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_APOSTROPHE, "APOSTROPHE", "'"));
                                tokens.Add(new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @"\("));
                                break;
                            case ")":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @"\)"));
                                break;
                            case "(":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_RPAR, "LPAR", @"\("));
                                break;
                            case ");":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @"\)"));
                                tokens.Add(new Token((int)TOKENSPASCAL.T_SCOLON, "Semi-Colon", @";"));
                                break;
                            default:
                                Console.WriteLine("can't match " + fragment_str);
                                if(letter.IsMatch(fragment_str)){
                                    Console.WriteLine("got a letter next to a symbol");
                                }
                                break;
                        }
                    }

                    Console.Write(string.Join("", fragment));
                    fragment.Clear();
                    //put the churernt character back to catch the non-letter 
                    currentChar--;
                    //break;
                }else{
                    Console.Write("did't match %c", c);
                    done = true;
                    break;
                }

            }
                 currentChar--;
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
