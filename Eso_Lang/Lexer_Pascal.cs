using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/*
 issues 
identifiers can't contain numbers
pascal comments not done
doubles not done
 */
namespace Eso_Lang
{
    public class Lexer_Pascal
    {
        const int MAX_LEN = 32;
        public int[] Tokens;
        public string[] Symbols;
        List<Token> tokenRegexs;
        public static List<Token> tokensRegexPascal;
        public Lexer_Pascal()
        {
            Tokens = new int[MAX_LEN];
            Symbols = new string[MAX_LEN];
           

            tokensRegexPascal = new List<Token>();
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_PROGRAM, "Program", @"^program$"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimiter", @";"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_BEGIN, "Begin", @"^begin$"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_END, "End", @"^end$"));
            // tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_PERIOD, "Period", @"\."));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_WRITELINE, "write Line", @"^writeln$"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_COMMENT, "CommentL", @"\(\*"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_COMMENT, "CommentR", @"\*\)"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_GTHANEQ, "Greater-or-Equal-Operator", @"\>="));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_LTHANEQ, "Less-or-Equal-Operator", @"\<="));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_INTDIV, "Integer-Division", @"^div$"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_INTMOD, "Modulous", @"^mod$"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_VAR, "VarBlock", @"^var$"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_CONST, "ConstBlock", @"^const$"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_INT_TYPE, "Int type", @"^integer$"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_STRING_TYPE, "string type", @"^string$"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_TRUE, "true bool type", @"^true$"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_FALSE, "false bool type", @"^false$"));

            //versions of if
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_IF, "IF", @"^if$"));
            // tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_IF, "IF", @"if"));

            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_THEN, "THEN", @"^then$"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_ELSE, "ElSE", @"^else$"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_FOR, "FOR", @"for"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_WHILE, "WHILE", @"^while$"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_DO, "DO", @"^do$"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_REPEAT, "REPEAT", @"repeat"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_UNTIL, "UNTIL", @"until"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_LPAR, "LPAR", @"\("));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @"\)"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_COMMA, "COMMA", @"\,"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_APOSTROPHE, "APOSTROPHE", "\'"));
            //checked last
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_IDENT, "Indetifier", @"\b[a-zA-Z]\b"));
            // ^ don't change    v can change
            /*
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_PLUS, "Addition-Operator", @"\+\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_MINUS, "Subtraction-Operator", @"\-\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_DIVIDE, "Division-Operator", @"/\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_MULTIPLY, "Multiplication-Operator", @"\*\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_SLEFT, "Shift-Left-Operator", @"<<\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_SRIGHT, "Shift-Right-Operator", @">>\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_ASSIGN, "Assignment-Operator", @":=\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_ASSIGN, "Assignment-Operator", @"=\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_GTHAN, "Greater-Than-Operator", @">\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_LTHAN, "Less-Than-Operator", @"<\s"));
            */

            this.tokenRegexs = tokensRegexPascal;

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

            while (!done){
                //make sure we haven't gone out of bounds
                if (currentChar < source.Length){ 
                    c = source[currentChar];
                }else {
                    Console.WriteLine("reached end of string");
                    break;
                }

                Console.WriteLine("got char at start : " + c);

                //match single character tokens 
                if (c == '\n')
                {
                    lines += 1;
                    currentChar++;
                }
                else if (c == ';'){
                    tokens.Add(new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimiter", @";"));
                    currentChar++;
                } else if (c == '=') {
                    tokens.Add(new Token((int)TOKENSPASCAL.T_EQUAL, "is-equal", @"="));
                    currentChar++;
                } else if (c == ',') {
                    tokens.Add(new Token((int)TOKENSPASCAL.T_COMMA, "COMMA", @"\,"));
                    currentChar++;
                } else if (c == '\'') {
                    tokens.Add(new Token((int)TOKENSPASCAL.T_APOSTROPHE, "APOSTROPHE", "\'"));
                    currentChar++;
                } else if (c == '+') {
                    tokens.Add(new Token((int)TOKENSPASCAL.T_PLUS, "plus", @"\+"));
                    currentChar++;
                }else if (c == '-')
                {
                    tokens.Add(new Token((int)TOKENSPASCAL.T_MINUS, "minus", @"-"));
                    currentChar++;
                }else if (c == ' ') {
                    currentChar++;
                }else if (c == '.') {
                    currentChar++;
                    tokens.Add(new Token((int)TOKENSPASCAL.T_PERIOD, "Period", @"\."));
                }
                //read a number 
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
                   //removes the last character from fragment as it will be a non digit
                    string fragment_str = string.Join("", fragment).Remove(string.Join("", fragment).Length - 1, 1);
                    Token t = new Token((int)TOKENSPASCAL.T_NR, "number", @"\d+");
                    t.intval = Int32.Parse(fragment_str);
                    tokens.Add(t);
                        Console.Write(fragment_str);
                        fragment.Clear();
                    
                        //put the churernt character back to catch the non-digit 
                        currentChar--;

                // if the token started with a letter, needs to match idenetifiers and reserved words
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
                    //checks for a match in the token list provided in the constructor, if found then a reserved word was 
                    // found, make a token for it
                    foreach (Token t in tokenRegexs){
                        var match = t.match_str(fragment_str);
                        foundAmatch = false;
                        if (match != "") {
                            Console.WriteLine("MATCH: " + fragment_str);
                            foundAmatch = true;
                            tokens.Add(new Token(t.id, t.name, t.tokenRE));
                            fragment.Clear();
                            //break on the first matched reserved word
                            break;
                        }
                    }
                    
                    //else the word wasn't reserved set it as an identifier
                    if (!foundAmatch) {
                        Console.WriteLine("looking for id in: " + fragment_str);
                        Console.WriteLine("identifier: " + fragment_str);
                        Token t = new Token((int)TOKENSPASCAL.T_IDENT, "identifier", "[a-zA-Z]+");
                        t.stringval = fragment_str;
                        tokens.Add(t);
                        fragment.Clear();
                    }
                 
                    //put the churernt character back to catch the non-letter 
                    currentChar--;
                    //else collect blocks of symbols
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
                    //remove the last non-symbol
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
                            case ":":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_COLON, "COLON", @":"));
                                break;
                            case "('":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_LPAR, "LPAR", @"\("));
                                tokens.Add(new Token((int)TOKENSPASCAL.T_APOSTROPHE, "APOSTROPHE", "'"));
                                // read characters untill next '
                                fragment.Clear();
                                currentChar--;

                                Console.WriteLine("index is " + currentChar + " char at index is " + source[currentChar] + " the source length is" + source.Length);

                                while (c != '\'')
                                {
                                    Console.WriteLine("index is " + currentChar + " char at index is " + source[currentChar]);

                                    //Console.WriteLine("looking for ' in  ");
                                    if (currentChar < source.Length)
                                    {
                                        c = source[currentChar++];
                                        fragment.Add(c);
                                    }
                                    else
                                    {
                                        //fragment.Add(c);
                                        Console.WriteLine("reached end of file while looking for a closing '");
                                        break;
                                    }
                                }
                                if (fragment.Count != 0)
                                {
                                    Token t = new Token((int)TOKENSPASCAL.T_STRING, "character String", @"[a-zA-Z\s]");
                                     string.Join("", fragment).Remove(string.Join("", fragment).Length - 1, 1);
                                    t.stringval = string.Join("", fragment).Remove(string.Join("", fragment).Length - 1, 1);
                                    tokens.Add(t);
                                }


                                Console.WriteLine("character string was " + string.Join("", fragment));
                                break;
                            case "')":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_APOSTROPHE, "APOSTROPHE", "'"));
                                tokens.Add(new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @"\("));
                                break;
                            case ")":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @"\)"));
                                break;
                            case "(":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_LPAR, "LPAR", @"\("));
                                break;
                            case ");":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @"\)"));
                                tokens.Add(new Token((int)TOKENSPASCAL.T_SCOLON, "Semi-Colon", @";"));
                                break;
                            case "<":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_LTHAN, "LESS-THAN", @"\<"));
                                break;
                            case ">":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_GTHAN, "GREATER-THAN", @"\>"));
                                break;
                            case "<=":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_LTHANEQ, "LESS-THAN-EQ", @"\<="));
                                break;
                            case ">=":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_GTHANEQ, "GREATER-THAN-EQ", @"\>="));
                                break;
                            case "=":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_EQUAL, "EQUAL", @"="));
                                break;
                            case "<>":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_NOTEQUAL, "Not-EQUAL", @"<>"));
                                break;
                            case "+":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_PLUS, "plus", @"\+"));
                                break;
                            case ":=":
                                tokens.Add(new Token((int)TOKENSPASCAL.T_ASSIGN, "assign", @":="));
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
