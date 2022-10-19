using System;
namespace eso_lang;
using  eso_lang_classes;

class Program {
	
	tokenRegexsPascal = new List<Token>();

            tokenRegexsPascal.Add(new Token(1, "Int", 0));
            tokenRegexsPascal.Add(new Token(1, "Int", 1));
            tokenRegexsPascal.Add(new Token(1, "Int", 2));
            tokenRegexsPascal.Add(new Token(1, "Int", 3));
            tokenRegexsPascal.Add(new Token(1, "Int", 4));
            tokenRegexsPascal.Add(new Token(1, "Int", 5));
            tokenRegexsPascal.Add(new Token(1, "Int", 6));
            tokenRegexsPascal.Add(new Token(1, "Int", 7));
            tokenRegexsPascal.Add(new Token(1, "Int", 8));
            tokenRegexsPascal.Add(new Token(1, "Int", 9));
            tokenRegexsPascal.Add(new Token(2, "Addition-Operator", @"+\s"));
            tokenRegexsPascal.Add(new Token(3, "Subtraction-Operator", @"-\s"));
            tokenRegexsPascal.Add(new Token(4, "Division-Operator", @"/\s"));
            tokenRegexsPascal.Add(new Token(5, "Multiplication-Operator", @"*\s"));
            tokenRegexsPascal.Add(new Token(6, "Asignment-Operator", @":=\s"));
            tokenRegexsPascal.Add(new Token(7, "Lable", @"_[a-zA-Z]*\s"));
            // any sting of letters and numbers starting with lowercase letter
	
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
	
       public static void Main(string[] args) {
        Console.WriteLine(FsharpInterop.add(5, 5));
        FsharpInterop.printFromFsharp("hello f#");
	    Lexer l1 = new Lexer();
        //                1       2     3     4     5       6    7       8   
        string test = " Let the _wORD Have Meats Put with Rum Put with Food ";
        string test1 =" Meats Put with Goats ";
        List<Token> tokens = l1.Lex(test1);
        Console.WriteLine("found matches: " + tokens.Count);
        foreach(Token tok in tokens){

            Console.WriteLine(tok.name , tok.id);
        }
        List<string> types = new List<string>();
        foreach(Token tok in tokens){
            Console.Write(tok.name);
            types.Add(tok.name);
        }
         FsharpInterop.printToken(tokens[0]);
         FsharpInterop.printTokenList(tokens);
    }

}
