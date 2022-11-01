using System;
using System.Collections.Generic;
using System.Text;

namespace Eso_Lang
{
    
    class Pascal_Parser_test
    {
        List<int> tokens;
        public Pascal_Parser_test(List<int> toks)
        {
            this.tokens = toks;
        }
        public int Parse()
        {
            int ret = 1;
           
            ret = Program(0);
            return ret;
        }
        // when calling tokens[currentToken] the tokens look up will be done at index 0 and
        // then the currentToken valuse passed will be incremneted

        int Program(int currentToken)
        {
            Console.WriteLine("Program() Current token index " + currentToken + " , token id " + tokens[currentToken]);
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_PROGRAM)
            {
                currentToken = Id(++currentToken);
                //here we are getting ident again, why is it not incrementing
               
            }
            Console.WriteLine("saw program name currentToken is " + currentToken + " which is " + tokens[currentToken]);

            //use an else if if you have 2 token types to check that would be in the same posistion
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_SCOLON)
            {
             Console.WriteLine("saw scolon currentToken is " + currentToken + " which is " + tokens[currentToken]);

                currentToken = Block(++currentToken);
                Console.WriteLine("Program() returned from simple block current index " + currentToken + " token at index is " + tokens[currentToken]);
            }
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_LPAR)
            {
                Console.WriteLine("Program() saw an lpar");
                currentToken = Id_List(++currentToken);
                Console.WriteLine("back from id list  token index  is "+ currentToken +" which is " + tokens[currentToken]);
                
                if (tokens[currentToken] == (int)TOKENSPASCAL.T_RPAR) {
                    currentToken = advance(++currentToken);
                    if (tokens[currentToken] == (int)TOKENSPASCAL.T_SCOLON) {
                        Console.WriteLine("Program() not empty calling Block with index" + currentToken );
                        currentToken = Block(++currentToken);
                        Console.WriteLine("Program() finshed Block, look for period");
                    }
                }
            }
            Console.WriteLine("the next token should be . and the final token in the list");
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_PERIOD)
            {
                return 0;
            }
            return 1;
        }

        private int advance(int v)
        {
            Console.WriteLine("called advance with token " +  v);
            return v;
        }

        int Id(int currentToken)
        {
            Console.WriteLine("Id() Current token index " + currentToken + " , token id " + tokens[currentToken]);
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_IDENT)
            {
                currentToken = currentToken + 1;
                Console.WriteLine("Id()token was id incemeting token to" + currentToken);
            }
            return currentToken;
        }

        private int Id_List(int currentToken)
        {
            Console.WriteLine("Id_List() Current token index " + currentToken + " , token id " + tokens[currentToken]);

            if (tokens[currentToken] == (int)TOKENSPASCAL.T_IDENT)
            {
                currentToken = Id(currentToken);
                if (tokens[currentToken] == (int)TOKENSPASCAL.T_COMMA)
                {
                    currentToken= Id_List(++currentToken);
                }
                else
                {
                    //there are no more id's in the list
                    // Id(currentToken);
                }
            }
            return currentToken;
        }

        int Block(int currentToken)
        {
            Console.WriteLine("Block() Current token index " + currentToken + " , token id " + tokens[currentToken]);

            if (tokens[currentToken] == (int)TOKENSPASCAL.T_BEGIN)
            {
                Console.WriteLine("Block() calling statment list with token index " + currentToken + " tok id " + tokens[currentToken]);
                currentToken = Statement_List(++currentToken);
                if (tokens[currentToken] == (int)TOKENSPASCAL.T_END)
                {
                    return ++currentToken;
                }
            }
            return currentToken;
        }

        int Statement(int currentToken)
        {
            Console.WriteLine("Statement() Current token index " + currentToken + " , token id " + tokens[currentToken]);

            if (tokens[currentToken] == (int)TOKENSPASCAL.T_WRITELINE)
            {
                // handle a writeline
                Console.WriteLine("found a writeline");
                currentToken = advance(++currentToken);
                if (tokens[currentToken] == (int)TOKENSPASCAL.T_LPAR)
                {
                    currentToken = advance(++currentToken);
                    if (tokens[currentToken] == (int)TOKENSPASCAL.T_APOSTROPHE)
                    {
                        currentToken = advance(++currentToken);
                        if (tokens[currentToken] == (int)TOKENSPASCAL.T_STRING)
                        {
                            currentToken = advance(++currentToken);
                            if (tokens[currentToken] == (int)TOKENSPASCAL.T_APOSTROPHE)
                            {
                                currentToken = advance(++currentToken);
                                if (tokens[currentToken] == (int)TOKENSPASCAL.T_STRING)
                                {
                                    Console.WriteLine("parsed a writeline statment");
                                }
                            }
                        }
                    }

                }
            }
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_Another_statement)
            {
                // handle antoher statement
                currentToken = currentToken + 1;
            }
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_END)
            {
                Console.WriteLine("staement was empty not incrementing  and returning");
            }
            else
            {
                Console.WriteLine("staement not found, not incrementing  and returning");

            }

            return currentToken;
        }

        int  Statement_List(int currentToken)
        {
            Console.WriteLine("Statement_List() Current token index " + currentToken + " , token id " + tokens[currentToken]);

            currentToken = Statement(currentToken);
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_SCOLON)
            {
                currentToken = Statement_List(currentToken);
            }
            else
            {
                // Id(currentToken);
            }
            return currentToken;
        }

        int Term(int currentToken)
        {
            Console.WriteLine("Term() Current token index " + currentToken + " , token id " + tokens[currentToken]);
            return currentToken;
        }

        void Factor(int currentToken) { }

        void Factor_p(int currentToken) { }

        void Expression(int currentToken) { }

        void Expression_p(int currentToken) { }

        void SimpleExpression(int currentToken) { }

        void SimpleExpression_p(int currentToken) { }


    }
}
