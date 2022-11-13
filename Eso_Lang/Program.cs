using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.FSharp.Core;
using Microsoft.FSharp.Collections;

namespace Eso_Lang
{
    class Program
    {
        public static List<Token> tokensRegexPascal;
        public static List<Token> tokenRegexsEso;

        public static void Main(string[] args)
        {

            tokensRegexPascal = new List<Token>();
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_PROGRAM, "Program", @"program"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimiter", @";"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_BEGIN, "Begin", @"begin"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_END, "End", @"end"));
           // tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_PERIOD, "Period", @"\."));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_WRITELINE, "write Line", @"writeln"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_COMMENT, "CommentL", @"\(\*"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_COMMENT, "CommentR", @"\*\)"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_GTHANEQ, "Greater-or-Equal-Operator", @"\>="));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_LTHANEQ, "Less-or-Equal-Operator", @"\<="));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_INTDIV, "Integer-Division", @"div"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_INTMOD, "Modulous", @"mod"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_VAR, "Modulous", @"var"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_CONST, "Modulous", @"const"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_IF, "IF", @"if"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_THEN, "THEN", @"then"));
            tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_ELSE, "ElSE", @"else"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_FOR, "FOR", @"for"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_WHILE, "WHILE", @"while"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_DO, "DO", @"do"));
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


            Lexer Eso_lang_lexer = new Lexer(tokenRegexsEso);
            Lexer Pascal_lexer = new Lexer(tokensRegexPascal);
      
            string pascal_test_string = "program donothing; begin end.";
            string pascal_test_string_2 = "program writealine(output); begin writeln('Hello World') end.";
            string pascal_test_string_3 = "program writeManyLines(output); \n begin  writeln('abc'); \n writeln('def'); \n end.";
            string pascal_test_string_4 = "program simpleIfElse ; begin  \n if( 10 + 10 - 10 ) then \n writeln('a is less than 20' ) \n else writeln('an error occured' ); \n end.";

            //string pascal_test_string_4 = "program simpleIfElse ; begin  \n if( 10  20 ) then \n writeln('a is less than 20' ) \n else writeln('an error occured' ); \n end.";
            //passes and it shoudnt
            string eso_lang_test_string = " Meats Put with Goats ";

            List<Token> eso_tokens = Eso_lang_lexer.Lex(eso_lang_test_string);
            List<Token> pascal_tokens_1 = Pascal_lexer.LexPascal(pascal_test_string);
            List<Token> pascal_tokens_2 = Pascal_lexer.LexPascal(pascal_test_string_2);
            List<Token> pascal_tokens_3 = Pascal_lexer.LexPascal(pascal_test_string_3);
            List<Token> pascal_tokens_4 = Pascal_lexer.LexPascal(pascal_test_string_4);

            List<int> pascal_ints = new List<int>();
            foreach (Token tok in pascal_tokens_2)
            {
                Console.Write(tok.name + " ");
                pascal_ints.Add(tok.id);
            }

            Console.WriteLine("attempting to parse");
            Pascal_Parser_test p1 = new Pascal_Parser_test(pascal_ints, pascal_tokens_2);
           


            // Pascal_Parser p = new Pascal_Parser(pascal_tokens);
            int passed=p1.Parse();
            if (passed == 0) {
                Console.WriteLine("parsed sucsessfully");
            }

            Pascal2C p2c = new Pascal2C(pascal_tokens_2);
            string c_source = p2c.generate();
            Console.WriteLine(c_source);

            //from https://stackoverflow.com/questions/1469764/run-command-prompt-commands
            string strCmdText;
            strCmdText = "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
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
