using System;
namespace eso_lang;
using  eso_lang_classes;

class Program {
	
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
