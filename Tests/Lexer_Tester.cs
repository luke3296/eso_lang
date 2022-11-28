using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eso_Lang;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class Lexer_Tester
    {
        
        [TestMethod]
        public void testSimplestProgram()
        {
            
            string test_str_1 = "program donothing; begin end.";
            List<Token> expectedResult = new List<Token>();
            
            Token t1, t2, t3, t4, t5, t6; 
             t1 = new Token((int)TOKENSPASCAL.T_PROGRAM, "Program", @"^program$");
             t2 = new Token((int)TOKENSPASCAL.T_IDENT, "identifier", "[a-zA-Z]+");
             t3 = new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimiter", @";");
             t4 = new Token((int)TOKENSPASCAL.T_BEGIN, "Begin", @"^begin$");
             t5 = new Token((int)TOKENSPASCAL.T_END, "End", @"^end$");
             t6 = new Token((int)TOKENSPASCAL.T_PERIOD, "Period", @"\.");
            expectedResult.Add(t1);
            expectedResult.Add(t2);
            expectedResult.Add(t3);
            expectedResult.Add(t4);
            expectedResult.Add(t5);
            expectedResult.Add(t6);

            Lexer_Pascal l = new Lexer_Pascal();
            List<Token> res = l.LexPascal(test_str_1);

            CollectionAssert.AreEqual(expectedResult, res);

         }

        //add more methods like above for:
        //string pascal_test_string = "program donothing; begin end.";
        //string pascal_test_string_2 = "program writealine(output); begin writeln('Hello World') end.";
        //string pascal_test_string_3 = "program writeManyLines(output); \n begin  writeln('abc'); \n writeln('def'); \n end.";
        //string pascal_test_string_4 = "program simpleIfElse ; begin  \n if( 10 + 10 - 10 ) then \n writeln('a is less than 20' ) \n else writeln('an error occured' ); \n end.";

        //string pascal_test_string_4 = "program simpleIfElse ; begin  \n if( 10  20 ) then \n writeln('a is less than 20' ) \n else writeln('an error occured' ); \n end.";
        //passes and it shoudnt
        //string pascal_test_string_5 = "program simpleifelse;begin\nif(10+10-10)then\nwriteln('a is less than 20' )\nelsewriteln('an error occured' );\nend.";
        //and any more you can think of.
    }
}
