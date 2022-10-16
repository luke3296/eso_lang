using System;
namespace eso_lang;
using  eso_lang_classes;

class Program {
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
        //FsharpInterop.Parse(tokens);
    }

}
