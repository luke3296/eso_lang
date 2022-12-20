using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FSharp.Core;
using Microsoft.FSharp.Collections;
using System.IO;

namespace Eso_Lang
{
   public class Program
    {
        public static List<Token> tokenRegexsEso;

        public static void Main(string[] args)
        {
            


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


            //Lexer_Pascal Eso_lang_lexer = new Lexer();
            Lexer_Pascal Pascal_lexer = new Lexer_Pascal();

            string pascal_test_string = "program donothing; begin end.";
            string pascal_test_string_2 = "program writealine(output); begin writeln('Hello World') end.";
            string pascal_test_string_3 = "program writeManyLines(output); \n begin  writeln('abc'); \n writeln('def'); \n end.";
            string pascal_test_string_4 = "program simpleIfElse ; begin  \n if( 10 + 10 - 10 ) then \n writeln('a is less than 20' ) \n else writeln('an error occured' ); \n end.";
            string t4 = "program test; begin if( 10 > 20 ) then writeline(\"oof\") end .";
            //string pascal_test_string_4 = "program simpleIfElse ; begin  \n if( 10  20 ) then \n writeln('a is less than 20' ) \n else writeln('an error occured' ); \n end.";
            //passes and it shoudnt
            string pascal_test_string_5 = "program simpleifelse;begin\nif(10+10-10)then\nwriteln('a is less than 20' )\nelsewriteln('an error occured' );\nend.";

            //string eso_lang_test_string = " Meats Put with Goats ";

            //List<Token> eso_tokens = Eso_lang_lexer.Lex(eso_lang_test_string);
            List<Token> pascal_tokens_1 = Pascal_lexer.LexPascal(t4);
            //List<Token> pascal_tokens_2 = Pascal_lexer.LexPascal(pascal_test_string);
            //List<Token> pascal_tokens_3 = Pascal_lexer.LexPascal(pascal_test_string_3);
            //List<Token> pascal_tokens_4 = Pascal_lexer.LexPascal(pascal_test_string_4);

           

            Console.WriteLine("attempting to parse");
            Parser_Pascal p1 = new Parser_Pascal(pascal_tokens_1);
            //Pascal_parser_with_tree ppwt = new Pascal_parser_with_tree(pascal_tokens_2);

            int passed=p1.Parse();
            if (passed == 0) {
                Console.WriteLine("parsed sucsessfully");
            }

            Pascal2C p2c = new Pascal2C(pascal_tokens_1);
            string c_source = p2c.generate();
            string path = p2c.Write_file();

            //from https://stackoverflow.com/questions/1469764/run-command-prompt-commands
            //string strCmdText;
            
            
            //string MVSC_dir = "\"C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\VC\\Tools\\MSVC\\14.29.30133\\bin\\Hostx64\x64\\cl\"";
            //string gcc_dir = "C:\\cygwin64\\bin\\gcc.exe";
            /*
            if (File.Exists(MVSC_dir))
            {
                
                strCmdText = " cd "+ Directory.GetCurrentDirectory()+" ; "+MVSC_dir + " " + path;
                Console.WriteLine("cmd string  " + strCmdText);
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            }
            else if (File.Exists(gcc_dir)) {
                strCmdText = " cd " + Directory.GetCurrentDirectory() + " && " + gcc_dir +" "+ path;
                Console.WriteLine("cmd string  " + strCmdText);
                System.Diagnostics.Process.Start("cmd.exe", strCmdText);
                //System.Diagnostics.Process.Start("cmd.exe", strCmdText);
            }
            */
       
            /*
            Console.WriteLine("found matches: " + eso_tokens.Count);

            // List<string> types = new List<string>();
            // foreach(Token tok in tokens){
            //     Console.Write(tok.name);
            //     types.Add(tok.name);
            // }



            Pascal2c_Code_Gen pascal2c = new Pascal2c_Code_Gen(pascal_tokens);
            string c_str = pascal2c.generate();

            Console.WriteLine(c_str);

            //debug stuff
            var CS_list = new List<double> { 1.0, -9.0, 25.0, -15.0, 10.0, -5.0, 12.0 };
            var FS_list = ListModule.OfSeq(CS_list); // Convert to F# list
            double result = SumFloat.sum(FS_list); // Call F# function 
            Console.WriteLine("Final result = {0:F1}", result);

            Console.Read();
            */
        }
    }
}
