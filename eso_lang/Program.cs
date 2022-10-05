// See https://aka.ms/new-console-template for more information
using System;
namespace eso_lang;

class Program {
       public static void Main(string[] args) {
        Lexer l1 = new Lexer();
        l1.Lex("+ - / * ( ) hello 123  ");
        l1.Lex("  < ? > ");
        l1.printTables();
        Console.WriteLine(FsharpInterop.add(5, 5));
        FsharpInterop.printFromFsharp("hello f#");
    }

}

