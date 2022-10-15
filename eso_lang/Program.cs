// See https://aka.ms/new-console-template for more information
using System;
namespace eso_lang;

class Program {
       public static void Main(string[] args) {
        Console.WriteLine(FsharpInterop.add(5, 5));
        FsharpInterop.printFromFsharp("hello f#");
	Lexer l1 = new Lexer();
        //                1       2     3     4     5       6    7       8   
        string test = "Let the _wORD Have Meats Put with Rum Put with Food ";
        List<Token> tokens = l1.Lex(test);
        Console.WriteLine("found matches: " + tokens.Count);
        foreach(Token tok in tokens){

            Console.WriteLine(tok.name , tok.id);
        }
    }

}
