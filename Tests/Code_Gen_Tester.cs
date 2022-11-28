using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eso_Lang;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class Code_Gen_Tester
    {

        [TestMethod]
        //string pascal_test_string = "program donothing; begin end.";
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

        //string pascal_test_string_2 = "program writealine; begin writeln('Hello World') end.";
        public void testWriteLineOutput()
        {

            string expectedRes = "#include \"stdio.h\"\nvoid main(){printf('Hello World');}";
            List<Token> Tokens= new List<Token>();

            Token t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12;
            t1 = new Token((int)TOKENSPASCAL.T_PROGRAM, "Program", @"^program$");
            t2 = new Token((int)TOKENSPASCAL.T_IDENT, "identifier", "[a-zA-Z]+");
            t3 = new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimeter", @";");
            t4 = new Token((int)TOKENSPASCAL.T_BEGIN, "Begin", @"^begin$");
            t5 = new Token((int)TOKENSPASCAL.T_WRITELINE, "write line", @"^writeln$");
            t6 = new Token((int)TOKENSPASCAL.T_LPAR, "LPAR", @"\(");
            t7 = new Token((int)TOKENSPASCAL.T_APOSTROPHE, "Apostrophe", @"\'");
            t8 = new Token((int)TOKENSPASCAL.T_STRING, "character String", @"[a-zA-Z\s]");
            t9 = new Token((int)TOKENSPASCAL.T_APOSTROPHE, "Apostrophe", @"\'");
            t10 = new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @"\)");
            t11 = new Token((int)TOKENSPASCAL.T_END, "End", @"^end$");
            t12 = new Token((int)TOKENSPASCAL.T_PERIOD, "Period", @"\.");
            Tokens.Add(t1);
            Tokens.Add(t2);
            Tokens.Add(t3);
            Tokens.Add(t4);
            Tokens.Add(t5);
            Tokens.Add(t6);
            Tokens.Add(t7);
            Tokens.Add(t8);
            Tokens.Add(t9);
            Tokens.Add(t10);
            Tokens.Add(t11);
            Tokens.Add(t12);
            
            Pascal2C p2c = new Pascal2C(Tokens);
            string actual = p2c.generate();

            Assert.AreEqual(expectedRes, actual);
        }

        //add more methods like above for:
        
        
        //string pascal_test_string_3 = "program writeManyLines; \n begin  writeln('abc'); \n writeln('def'); \n end.";
        public void testSimpleIfElse()
        {

            string expectedRes = "#include \"stdio.h\"\nvoid main(){ printf('abc'); \n writeln('def'); \n}";
            List<Token> Tokens = new List<Token>();

            Token t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19, t20;
            t1 = new Token((int)TOKENSPASCAL.T_PROGRAM, "Program", @"^program$");
            t2 = new Token((int)TOKENSPASCAL.T_IDENT, "identifier", "[a-zA-Z]+");
            t3 = new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimeter", @";");
            t4 = new Token((int)TOKENSPASCAL.T_BEGIN, "Begin", @"^begin$");
            t5 = new Token((int)TOKENSPASCAL.T_WRITELINE, "write line", @"^writeln$");
            t6 = new Token((int)TOKENSPASCAL.T_LPAR, "LPAR", @"\(");
            t7 = new Token((int)TOKENSPASCAL.T_APOSTROPHE, "Apostrophe", @"\'");
            t8 = new Token((int)TOKENSPASCAL.T_STRING, "character String", @"[a-zA-Z\s]");
            t9 = new Token((int)TOKENSPASCAL.T_APOSTROPHE, "Apostrophe", @"\'");
            t10 = new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @"\)");
            t11 = new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimeter", @";");
            t12 = new Token((int)TOKENSPASCAL.T_WRITELINE, "write line", @"^writeln$");
            t13 = new Token((int)TOKENSPASCAL.T_LPAR, "LPAR", @"\(");
            t14 = new Token((int)TOKENSPASCAL.T_APOSTROPHE, "Apostrophe", @"\'");
            t15 = new Token((int)TOKENSPASCAL.T_STRING, "character String", @"[a-zA-Z\s]");
            t16 = new Token((int)TOKENSPASCAL.T_APOSTROPHE, "Apostrophe", @"\'");
            t17 = new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @"\)");
            t18 = new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimeter", @";");
            t19 = new Token((int)TOKENSPASCAL.T_END, "End", @"^end$");
            t20 = new Token((int)TOKENSPASCAL.T_PERIOD, "Period", @"\.");
            Tokens.Add(t1);
            Tokens.Add(t2);
            Tokens.Add(t3);
            Tokens.Add(t4);
            Tokens.Add(t5);
            Tokens.Add(t6);
            Tokens.Add(t7);
            Tokens.Add(t8);
            Tokens.Add(t9);
            Tokens.Add(t10);
            Tokens.Add(t11);
            Tokens.Add(t12);
            Tokens.Add(t13);
            Tokens.Add(t14);
            Tokens.Add(t15);
            Tokens.Add(t16);
            Tokens.Add(t17);
            Tokens.Add(t18);
            Tokens.Add(t19);
            Tokens.Add(t20);

            Pascal2C p2c = new Pascal2C(Tokens);
            string actual = p2c.generate();

            Assert.AreEqual(expectedRes, actual);
        }

        
        //string pascal_test_string_4 = "program simpleIfElse ; begin  \n if( 10 + 10 - 10 ) then \n writeln('a is less than 20' ) \n else writeln('an error occured' ); \n end.";

        //string pascal_test_string_4 = "program simpleIfElse ; begin  \n if( 10  20 ) then \n writeln('a is less than 20' ) \n else writeln('an error occured' ); \n end.";
        //passes and it shoudnt
        //string pascal_test_string_5 = "program simpleifelse;begin\nif(10+10-10)then\nwriteln('a is less than 20' )\nelsewriteln('an error occured' );\nend.";
        //and any more you can think of.
    }
}
