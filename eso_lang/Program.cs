using System;
namespace eso_lang;
using  eso_lang_classes;


     public enum TOKENSPASCAL
        {
            T_PLUS = 1,
            T_MINUS = 2,
            T_DIVIDE = 3,
            T_MULTIPLY = 4,
            T_IDENT = 5,
            T_LPAR = 6,
            T_RPAR = 7,
            T_NR = 8,
            T_ASSIGN = 9,
	    T_BEGIN = 10,
	    T_END = 11,
	    T_PROGRAM = 12,
	    T_SCOLON = 13,
	    T_GTHAN = 14,
	    T_LTHAN = 15,
	    T_GTHANE = 16,
	    T_LTHANE = 17,
	    T_INTDIV = 18,
	    T_INTMOD = 19
        }

  public enum TOKENSESO
        {
            T_PLUS = 1,
            T_MINUS = 2,
            T_DIVIDE = 3,
            T_MULTIPLY = 4,
            T_IDENT = 5,
            T_LPAR = 6,
            T_RPAR = 7,
            T_NR = 8,
            T_ASSIGN = 9
        }

class Program {

        public static List<Token> tokenRegexsPascal;
        public static List<Token> tokenRegexsEso;

        public static void Main(string[] args) {
	    
	        tokensRegexPascal = new List<Token>();

            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_PLUS, "Addition-Operator", @"\+\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_MINUS, "Subtraction-Operator", @"\-\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_DIVIDE, "Division-Operator", @"/\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_MULTIPLY, "Multiplication-Operator", @"\*\s"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_SLEFT, "Shift-Left-Operator", @"<<\s"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_SRIGHT, "Shift-Right-Operator", @">>\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_ASSIGN, "Assignment-Operator", @":=\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_ASSIGN, "Assignment-Operator", @"=\s"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_IDENT, "Lable", @"[a-zA-Z]*\s"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_LPAR, "LPAR", @"("));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @")"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_BEGIN, "Begin", @"Begin\s"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_END, "End", @"End\s"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_PROGRAM, "Program", @"Program\s"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimiter", @";"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_GTHAN, "Greater-Than-Operator", @">\s"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_LTHAN, "Less-Than-Operator", @"<\s"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_GTHANE, "Greater-or-Equal-Operator", @">=\s"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_LTHANE, "Less-or-Equal-Operator", @"<=\s"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_INTDIV, "Integer-Division", @"div\s"));
	    tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_INTMOD, "Modulous", @"mod\s"));
            // any sting of letters and numbers starting with lowercase letter

            tokenRegexsEso = new List<Token>();

            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Naught\s", 0));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Boots\s", 1));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Goats\s", 2));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Powder\s", 3));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Rum\s", 4));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Meats\s", 5));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Wines\s", 6));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Cloth\s", 7));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Ropes\s", 8));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Food\s", 9));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_PLUS, "Addition-Operator", @"Put\swith\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_MINUS, "Subtraction-Operator", @"Take\sfrom\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_DIVIDE, "Division-Operator", @"By\scount\sof\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_MULTIPLY, "Multiplication-Operator", @"By\scount\sper\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_ASSIGN, "Asignment-Operator", @"Let\sthe\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_ASSIGN, "Asignment-Operator", @"Have\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_IDENT, "Lable", @"_[a-zA-Z]*\s"));


	       
        //Console.WriteLine(FsharpInterop.add(5, 5));
        //FsharpInterop.printFromFsharp("hello f#");
	    Lexer Eso_lang_lexer = new Lexer(tokenRegexsEso);
        Lexer Pascal_lexer = new Lexer(tokenRegexsPascal);



        //                1       2     3     4     5       6    7       8   
        string pascal_test_tring =  "program HelloWorld; \n  (* Here the main program block starts *) \n begin \n writeln('Hello, World!'); \n end.";
        string eso_lang_test_string =" Meats Put with Goats ";

        List<Token> eso_tokens = Eso_lang_lexer.Lex(eso_lang_test_string);
        List<Token> pascal_tokens = Pascal_lexer.Lex(pascal_test_tring);



        Console.WriteLine("found matches: " + eso_tokens.Count);
        foreach(Token tok in eso_tokens){

            Console.WriteLine(tok.name , tok.id);
        }
        // List<string> types = new List<string>();
        // foreach(Token tok in tokens){
        //     Console.Write(tok.name);
        //     types.Add(tok.name);
        // }
         //FsharpInterop.printToken(tokens[0]);
        // FsharpInterop.printTokenList(tokens);
    }

}
