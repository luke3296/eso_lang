namespace eso_lang_tests;

    public class LexerTests
    {
        [Fact] //the tests should have discriptive names eg CheckSubStringRegexOverlap or something
        public void LexTest1()
        {
            string test_string = "i am some example source code,might contain potentialy with errors";
            var lexer =  new eso_lang.Lexer();
            List<eso_lang.Token> token_list;
            List<string> symbol_list;
            (token_list, symbol_list)= lexer.Lex(test_string);
            Assert.Equal(1, token_list.Count);

        }
    }
