﻿// Author:  Luke A
// Purpouse: This file contains the main tests for the Lexer/Parser/CodeGenerator
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

        static void printCodeGen(string test_str, List<Token> toks)
        {
            Console.WriteLine("##");
            Console.WriteLine(test_str);
            Pascal2C p2c = new Pascal2C(toks);
            string c_source = p2c.genProgram();
            Console.WriteLine(c_source);
            Console.WriteLine();


        }

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
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_IDENT, "Identifier", @"_[a-zA-Z]*\s"));


            //Lexer_Pascal Eso_lang_lexer = new Lexer();
            Lexer_Pascal Pascal_lexer = new Lexer_Pascal();


            string pascal_test_string_1   = "program donothing; begin end.";
            string pascal_test_string_2 = "program writealine ; \n  begin writeln( ' Hello World ' ); \n end.";
            //updated lexer, avoid ') and (' use ' ) and ( ' instead
            string pascal_test_string_3 = "program writeManyLines ; \n begin  writeln( ' abc ' ); \n writeln( ' def ' ); \n end.";
            // when the writeln's are ended with a ; it doesnt , lexer breaks in infinte loop at ('
            string pascal_test_string_4 = "program simpleIfElse ; begin  \n if( 10 * 10 / 10 < 10 ) then \n begin \n  writeln( 'a is less than 20' )   \n end \n else  \n begin writeln( 'an error occured' )  \n end \n end.";
            string pascal_test_string_5 = "program test ; begin if( 10 > 20 ) then writeln( 'oof' ) end .";
            string pascal_test_string_6 = "program simplevar ; var \n firstint, secondInt, theirdInt : integer ; begin  \n if( 10 + 10 - 10 ) then \n writeln( 'a is less than 20' ); \n else writeln( 'an error occured' ); \n end.";
            //note the code generator needs the statments section of aan if block to be between begin and end
            string pascal_test_string_7 = "program simplevar ; var \n firstint, secondInt, theirdInt : integer ; \n str2, str2 : string ;  begin  \n if( 10 + 10 - 10 ) then \n begin \n writeln( 'a is less than 20' ) \n end\n else \n begin \n writeln( 'an error occured' ); \nend \n end.";
            string pascal_test_string_8 = "program assign; \n var \n a : integer; \n begin \n a := 100; \n if( a < 20 ) then \n begin \n writeln( 'a is less than 20' ); \n writeln( ' a second msg'); \n end \n else \nbegin\n writeln( 'a' ); \n writeln( ' ' , a); \n end\n end.";
            string pascal_test_string_9 = "program whiledo ; var \n firstint, secondInt, theirdInt : integer ; \n str2, str2 : string ;  begin  \n if( 10 + 10 - 10 ) then \n writeln( 'a is less than 20' ) \n else writeln( 'an error occured' ); \n end.";
            string pascal_test_string_10 = "program writealine ; var anid : integer ; begin writeln( 'Hello World' , anid) end.";
            string pascal_test_string_11 = "program simplevar ; var \n firstint, secondInt, theirdInt : integer ; begin  \n if( 10 + 10 - 10 ) then \n" +
                "begin \n writeln( 'a is less than 20' ) \n end \n else begin \n writeln( 'an error occured' ); \n end \n end.";
            string pascal_test_string_12 = "program testwhile ; \n var \n a : integer ; \n begin \n a := 0 ; \n while a < 10  do \n begin \n a := a + 1; \n end \n end .";
            string pascal_test_string_13 = "program ifladder ; \n begin  \n if(10 > 2) then \n begin \n writeln( 'yo' ); \n end \n else if(10 > 3) then \n begin \n writeln( 'oof' ); \n end \n else if(10 > 4) then  \n begin \n writeln( 'hm' ); \n end \n else  \n begin \n writeln( 'oof' ); \n end \n end.";
            string test_error_string = " gfnskvskd.nvs nmlkmvs vmsldlk  4j '34;5 ";
            string pascal_test_string_14 = "program testwhile ; \n var \n a : integer ; \n begin \n a := 0 ; \n while ( a < 10 )  do \n begin \n a := a + 1; \n end \n end .";
            string pascal_test_string_15 = "program test; var a : integer; begin a := 3 * (4 + 3); end.";
            string pascal_test_string_16 = "program test; var a : integer; begin a := 3 * (4 + 3 * ( 3 / 4 ) ); end.";
            string pascal_test_string_17 = "program test; var a : integer; begin a := 3 * (4 + 3 * ( 3 / ( 3 + 2) +3 ) ); end.";
            // note nested if's and whiles need to be ; terminted if there not the only statement in a block
            //code gen doent work for string 18
            string pascal_test_string_18 = "program test; var  a : integer; begin a := 0; if (a < 10 ) then begin while a > 4 do begin  writeln( 'oof'); if( a > 2 ) then begin writeln( 'oof') end ; writeln( 'oof'); end  end end.";
            //bug in the lexer )) not acounted for . space seperating them fixes it
            //code gen doent work for string 19. printf's wrong
            string pascal_test_string_19 = "program expressions; var a,b,c,d,e,f : integer; begin  a := 2+4; b := 3*6; c := 4/2; d := 6-2; e := 6 * (3+4); f := 6 * ( 2 + ( 1 + 2 ) );  writeln( ' aaaa ' , a , ' bbbbb ' , b , ' c ' , c , ' d ' , d , ' e ' , e , ' f ' , f );  end.";
            string pascal_test_string_20 = "program nestedifassign; var a : integer ;  begin if( 2 > 1 ) then begin if( 2 < 3 ) then begin while a<3 do begin a := a+1; end end end end.";


            List<Token> pascal_tokens_1 = Pascal_lexer.LexPascal(pascal_test_string_1);
            List<Token> pascal_tokens_2 = Pascal_lexer.LexPascal(pascal_test_string_2);
            List<Token> pascal_tokens_3 = Pascal_lexer.LexPascal(pascal_test_string_3);
            List<Token> pascal_tokens_4 = Pascal_lexer.LexPascal(pascal_test_string_4);
            List<Token> pascal_tokens_5 = Pascal_lexer.LexPascal(pascal_test_string_5);
            List<Token> pascal_tokens_6 = Pascal_lexer.LexPascal(pascal_test_string_6);
            List<Token> pascal_tokens_7 = Pascal_lexer.LexPascal(pascal_test_string_7);
            List<Token> pascal_tokens_8 = Pascal_lexer.LexPascal(pascal_test_string_8);
            List<Token> pascal_tokens_9 = Pascal_lexer.LexPascal(pascal_test_string_9);
            List<Token> pascal_tokens_10 = Pascal_lexer.LexPascal(pascal_test_string_10);
            List<Token> pascal_tokens_11 = Pascal_lexer.LexPascal(pascal_test_string_11);
            List<Token> pascal_tokens_12 = Pascal_lexer.LexPascal(pascal_test_string_12);
            List<Token> pascal_tokens_13 = Pascal_lexer.LexPascal(pascal_test_string_13);
            List<Token> pascal_tokens_14 = Pascal_lexer.LexPascal(pascal_test_string_14);
            List<Token> pascal_tokens_15 = Pascal_lexer.LexPascal(pascal_test_string_15);
            List<Token> pascal_tokens_16 = Pascal_lexer.LexPascal(pascal_test_string_16);
            List<Token> pascal_tokens_17 = Pascal_lexer.LexPascal(pascal_test_string_17);
            List<Token> pascal_tokens_18 = Pascal_lexer.LexPascal(pascal_test_string_18);
            List<Token> pascal_tokens_19 = Pascal_lexer.LexPascal(pascal_test_string_19);
            List<Token> pascal_tokens_20 = Pascal_lexer.LexPascal(pascal_test_string_20);

            List<Token> test_error_string_toks = Pascal_lexer.LexPascal(test_error_string);

           // Console.Write(test_error_string_toks.Count);
           // Parser_Pascal ptest = new Parser_Pascal(test_error_string_toks);
          //  Pascal2C pc2 = new Pascal2C(test_error_string_toks);
          //  pc2.genProgram();
            //Console.WriteLine(ptest.Parse());
           
            Console.Write("\n");

           
            Parser_Pascal p1 = new Parser_Pascal(pascal_tokens_1);
            Parser_Pascal p2 = new Parser_Pascal(pascal_tokens_2);
            Parser_Pascal p3 = new Parser_Pascal(pascal_tokens_3);
            Parser_Pascal p4 = new Parser_Pascal(pascal_tokens_4);
            Parser_Pascal p5 = new Parser_Pascal(pascal_tokens_5);
            Parser_Pascal p6 = new Parser_Pascal(pascal_tokens_6);
            Parser_Pascal p7 = new Parser_Pascal(pascal_tokens_7);
            Parser_Pascal p8 = new Parser_Pascal(pascal_tokens_8);
            Parser_Pascal p9 = new Parser_Pascal(pascal_tokens_9);
            Parser_Pascal p10 = new Parser_Pascal(pascal_tokens_10);
            Parser_Pascal p11 = new Parser_Pascal(pascal_tokens_11);
            Parser_Pascal p12 = new Parser_Pascal(pascal_tokens_12);
            Parser_Pascal p13 = new Parser_Pascal(pascal_tokens_13);
            Parser_Pascal p14 = new Parser_Pascal(pascal_tokens_14);
            Parser_Pascal p15 = new Parser_Pascal(pascal_tokens_15);
            Parser_Pascal p16 = new Parser_Pascal(pascal_tokens_16);
            Parser_Pascal p17 = new Parser_Pascal(pascal_tokens_17);
            Parser_Pascal p18 = new Parser_Pascal(pascal_tokens_18);
          
            Parser_Pascal p19 = new Parser_Pascal(pascal_tokens_19);
            Parser_Pascal p20 = new Parser_Pascal(pascal_tokens_20);

            //Pascal_parser_with_tree ppwt = new Pascal_parser_with_tree(pascal_tokens_2);

            var p1res = p1.Parse();
            var p2res = p2.Parse();
            var p3res = p3.Parse();
            var p4res = p4.Parse();
            var p5res = p5.Parse();
            var p6res = p6.Parse();
            var p7res = p7.Parse();
            var p8res = p8.Parse();
            var p9res = p9.Parse();
            var p10res = p10.Parse();
            var p11res = p11.Parse();
            var p12res = p12.Parse();
            var p13res = p13.Parse();
            var p14res = p14.Parse();
            var p15res = p15.Parse();
            var p16res = p16.Parse();
            var p17res = p17.Parse();
            var p18res = p18.Parse();

            var p19res = p19.Parse();
            var p20res = p20.Parse();
          //  Console.WriteLine(p19res);
            Console.WriteLine("1:" + p1res + " 2:" + p2res + " 3:" + p3res + " 4:" + p4res + " 5:" + p5res + " 6: " + p6res + " 7: " + p7res + " 8: " + p8res + " 9: " + p9res + " 10: " + p10res + " 11: " + p11res + " 12: " + p12res + " 13: " + p13res + " 14: " + p14res + " 15: " + p15res + " 16: " + p16res + " 17: " + p17res + " 18: " + p18res + " 19: " + p19res + " 20: " + p20res);
           
            for (int i = 0; i < pascal_tokens_20.Count; i++) {
                Console.Write("  " + pascal_tokens_20[i].name);
            }
            /*  
                      printCodeGen(pascal_test_string_1, pascal_tokens_1);
                      printCodeGen(pascal_test_string_2, pascal_tokens_2);
                      printCodeGen(pascal_test_string_3, pascal_tokens_3);
                      printCodeGen(pascal_test_string_4, pascal_tokens_4);
                      printCodeGen(pascal_test_string_5, pascal_tokens_5);
                      printCodeGen(pascal_test_string_6, pascal_tokens_6);
                      printCodeGen(pascal_test_string_7, pascal_tokens_7);
                      printCodeGen(pascal_test_string_8, pascal_tokens_8);
                      printCodeGen(pascal_test_string_9, pascal_tokens_9);
                      printCodeGen(pascal_test_string_10, pascal_tokens_10);
                      printCodeGen(pascal_test_string_11, pascal_tokens_11);
                      printCodeGen(pascal_test_string_12, pascal_tokens_12);
                      printCodeGen(pascal_test_string_13, pascal_tokens_13);
                      printCodeGen(pascal_test_string_14, pascal_tokens_14);
                      printCodeGen(pascal_test_string_15, pascal_tokens_15);
                      printCodeGen(pascal_test_string_16, pascal_tokens_16);
                      printCodeGen(pascal_test_string_17, pascal_tokens_17);
                      printCodeGen(pascal_test_string_18, pascal_tokens_18);

                      printCodeGen(pascal_test_string_19, pascal_tokens_19);
                     */
                        printCodeGen(pascal_test_string_19, pascal_tokens_19);

            //string path = p2c.Write_file();

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
