using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eso_Lang;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class Code_Gen_Tester
    {

        [TestMethod]
        public void testSimplestProgram()
        {
            //the expected c source code
            string exprectedRes = "#include \"stdio.h\"\nvoid main(){}";

            //bcus this is a tester for the Parser it shouldn't use the Lexer otherwise its not a unit test
            List<Token> Tokens = new List<Token>();

            Token t1, t2, t3, t4, t5, t6;
            t1 = new Token((int)TOKENSPASCAL.T_PROGRAM, "Program", @"^program$");
            t2 = new Token((int)TOKENSPASCAL.T_IDENT, "identifier", "[a-zA-Z]+");
            t3 = new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimiter", @";");
            t4 = new Token((int)TOKENSPASCAL.T_BEGIN, "Begin", @"^begin$");
            t5 = new Token((int)TOKENSPASCAL.T_END, "End", @"^end$");
            t6 = new Token((int)TOKENSPASCAL.T_PERIOD, "Period", @"\.");
            Tokens.Add(t1);
            Tokens.Add(t2);
            Tokens.Add(t3);
            Tokens.Add(t4);
            Tokens.Add(t5);
            Tokens.Add(t6);

            Pascal2C p2c = new Pascal2C(Tokens);
            string actual = p2c.generate();

            Assert.AreEqual(exprectedRes, actual);

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
