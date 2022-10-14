// See https://aka.ms/new-console-template for more information
using System;
namespace eso_lang;

class Program {
       public static void Main(string[] args) {
        Console.WriteLine(FsharpInterop.add(5, 5));
        FsharpInterop.printFromFsharp("hello f#");
	Lexer l1 = new Lexer();
        string test = "hello i might contain some tokens ";
        List<Token> tokens = l1.Lex(test);
        Console.WriteLine("found matches: " + tokens.Count);
    }

}
