using System;
using System.Collections.Generic;
using System.Text;

namespace Eso_Lang
{
    
    public class Parser_Pascal
    {
        List<int> tokens;
        List<Token> Tokens;
        public Parser_Pascal(List<Token> tokes)
        {
            this.Tokens = tokes;
            List<int> pascal_ints = new List<int>();
            foreach (Token tok in tokes)
            {
                pascal_ints.Add(tok.id);
            }
            this.tokens = pascal_ints;

        }
        public int Parse()
        {
            int ret = 1;
           
            ret = Program(0);
            return ret;
        }
        // when calling tokens[currentToken] the tokens look up will be done at index 0 and
        // then the currentToken valuse passed will be incremneted


        //returns 0 if the sequence of tokens is derivable from the LRpascal_bnf or 1 if it isn't
        int Program(int currentToken)
        {
            Console.WriteLine("Program() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_PROGRAM)
            {
                currentToken = Id(++currentToken);
                //here we are getting ident again, why is it not incrementing
               
            }
            Console.WriteLine("saw program name currentToken is " + currentToken + " which is " + tokens[currentToken]);

            //use an else if if you have 2 token types to check that would be in the same posistion
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_SCOLON)
            {
             //Console.WriteLine("saw scolon currentToken is " + currentToken + " which is " + tokens[currentToken]);

                currentToken = Block(++currentToken);
                Console.WriteLine("Program() returned from simple block current index " + currentToken + " token at index is " + tokens[currentToken]);
            }
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_LPAR)
            {
                Console.WriteLine("Program() saw an lpar");
                currentToken = Id_List(++currentToken);
                Console.WriteLine("back from id list  token index  is "+ currentToken +" which is " + tokens[currentToken] + " which is " + Tokens[currentToken].name);
                
                if (tokens[currentToken] == (int)TOKENSPASCAL.T_RPAR) {
                    currentToken = advance(++currentToken);
                    if (tokens[currentToken] == (int)TOKENSPASCAL.T_SCOLON) {
                        Console.WriteLine("Program() not empty calling Block with index" + currentToken );
                        currentToken = Block(++currentToken);
                        Console.WriteLine("Program() finshed Block, look for period");
                    }
                }
            }
            Console.WriteLine("the next token should be . and the final token in the list. the next token is " + tokens[currentToken]);
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_PERIOD)
            {
                return 0;
            }
            return 1;
        }

        //prints the token given to it
        private int advance(int v)
        {
            Console.WriteLine("called advance with token " +  v);
            return v;
        }
        //returns the next token index if the token passed to it was a T_IDENT
        int Id(int currentToken)
        {
            Console.WriteLine("Id() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_IDENT)
            {
                currentToken = currentToken + 1;
                //Console.WriteLine("Id()token was id incemeting token to" + currentToken);
            }
            //should probably fail here, instead it returns the Token it got that wasnt a T_IDENT
            return currentToken;
            
        }


        //returns the next token index after a list of id's has been parsed e.g abc, mdjf, jwhr...
        private int Id_List(int currentToken)
        {
            Console.WriteLine("Id_List() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );

            if (tokens[currentToken] == (int)TOKENSPASCAL.T_IDENT)
            {
                //id will do the incrememnt if currentToken was an id so no need to increment here
                currentToken = Id(currentToken);
                if (tokens[currentToken] == (int)TOKENSPASCAL.T_COMMA)
                {
                    currentToken= Id_List(++currentToken);
                }
                else
                { 
                    // at the end of the list
                }
            }
            //equvilent to | empty, returns the next Token after the Id_List
            return currentToken;
        }

        //returns next token index ater the end token of a block. tokens up to end are pared to statement_list
        int Block(int currentToken)
        {
            Console.WriteLine("Block() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );

            if (tokens[currentToken] == (int)TOKENSPASCAL.T_BEGIN)
            {
                Console.WriteLine("Block() calling statment list with token index " + currentToken + " tok id " + tokens[currentToken]);
                currentToken = Statement_List(++currentToken);
                if (tokens[currentToken] == (int)TOKENSPASCAL.T_END)
                {
                    //should return here
                    currentToken = advance( ++currentToken);
                    //return currentToken;
                }
            }
            Console.WriteLine(" return from Block() Current token index " + currentToken + " , token id " + tokens[currentToken] + "  is " + Tokens[currentToken].name);

            //should error here
           return currentToken;
        }


        // token index parsed should be a statment token e.g: if, for, then, while, else, writeline mabey var and const assignments
        // it parses the statement tokens paramaters to check then e.g If If shold see an LPAR EXPRESSION RPAR next, If WRITLINE should
        // see a LPAR APOSTROPHE CHARACTER-STRING APOSTROPHE.. ||   LPAR T_NR || LPAR EXPRESSION 
        int Statement(int currentToken)
        {
            Console.WriteLine("Statement() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );

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
                                if (tokens[currentToken] == (int)TOKENSPASCAL.T_RPAR)
                                {
                                    Console.WriteLine("parsed a writeline statment");
                                    //incrementting here so Statment returns the next token after the closeing ) of the writeline
                                    //this currently breaks string 4
                                    currentToken = advance(++currentToken);
                                }
                            }
                        }
                    }

                }
            }
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_IF)
            {
                Console.WriteLine("Statement() found an IF at index " + currentToken);
                // handle antoher statement
                //advance the current token to see if the next toke is an (
                currentToken = advance(++currentToken);
                if (tokens[currentToken] == (int)TOKENSPASCAL.T_LPAR)
                {
                    //call expression on the terms inside the if() brackets
                    currentToken = Expression(++currentToken);
                    Console.WriteLine("Statement() if passed if expression current token is " +Tokens[currentToken].name);
                    if (tokens[currentToken] == (int)TOKENSPASCAL.T_RPAR)
                    {
                        Console.WriteLine("passed if expression");
                        currentToken = advance(++currentToken);
                        if (tokens[currentToken] == (int)TOKENSPASCAL.T_THEN)
                        {
                            currentToken = Statement_List(++currentToken);
                            Console.WriteLine("passed then statements   current token is a " + Tokens[currentToken].name);
                            //currentToken = advance(++currentToken);
                            if (tokens[currentToken] == (int)TOKENSPASCAL.T_ELSE)
                            {
                                currentToken = Statement_List(++currentToken);
                                Console.WriteLine("passed else statements");
                                currentToken = advance(++currentToken);
                            }
                        }

                    }
                }
                else 
                {
                    Console.WriteLine("did't find a lpar after if");
                }
            }else if (tokens[currentToken] == (int)TOKENSPASCAL.T_ELSE)
            {
                // handle antoher statement
                Console.WriteLine("Statement() foud a else after an if");
                currentToken = currentToken + 1;
            }
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_THEN)
            {
                // handle antoher statement
                Console.WriteLine("Statement() foud a then after an if");
                currentToken = currentToken + 1;
            }// add while var const etc other statments 
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_END)
            {
                Console.WriteLine("staement was empty not incrementing  and returning");
            }
            else
            {
                Console.WriteLine("Statement() Current token index " + currentToken + " , token id " + tokens[currentToken] + "  is " + Tokens[currentToken].name);

                Console.WriteLine("staement not found, not incrementing  and returning");

            }
            //incremenet here as Statement is an end case
            Console.WriteLine(" return from Statement() Current token index " + currentToken + " , token id " + tokens[currentToken] + "  is " + Tokens[currentToken].name);

            return currentToken;
        }

        // same as ID_list but for statments, input is the id of the first statement, output is the the next Token index after the
        //final statment or ; 
        int  Statement_List(int currentToken)
        {
            Console.WriteLine("Statement_List() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );

            currentToken = Statement(currentToken);
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_SCOLON)
            {
                currentToken = Statement_List(++currentToken);
            }
            else
            {
                // Id(currentToken);
            }
            return currentToken;
        }

        //changed from the bnf Term -> Factror
        int Term(int currentToken)
        {
            Console.WriteLine("Term() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );
            // calling term here as the bnf states casues a left recursion issue
            currentToken = Factor(currentToken);
            //not calling factor prime, shhould check to * / div mod tokens to choose between Factor and Factor_p
            return currentToken;
        }

        //should check if the current token is a unsignedConstant, Expression between () or a varible 
        int Factor(int currentToken) {
            Console.WriteLine("Factor() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );

            if (tokens[currentToken] == (int)TOKENSPASCAL.T_STRING) {
                Console.WriteLine("factor is string");
            }
            else if ((tokens[currentToken] == (int)TOKENSPASCAL.T_NR)) {
                Console.WriteLine("factor is number");
                currentToken = UnsignedConstant(currentToken);
            }
            else {
                Console.WriteLine("No factor found returning without increment");
            }
            return currentToken;
        }

        int Factor_p(int currentToken) {
            Console.WriteLine("Factor_p() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );
            return currentToken;

        }

        int  Expression(int currentToken) {
            Console.WriteLine("Expression() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );
            //not rncrementing becuase we with to pass the number to SimpleExpression
            currentToken = SimpleExpression(currentToken);
            currentToken = Expression_p(currentToken);

            return currentToken;
        }

        int Expression_p(int currentToken) {
            Console.WriteLine("Expression_p() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_EQUAL){
                currentToken = SimpleExpression(++currentToken);
                currentToken = Expression_p(currentToken);
            } else if (tokens[currentToken] == (int)TOKENSPASCAL.T_LTHAN) {
                Console.WriteLine("found an Expression_p lthan ");
                currentToken = SimpleExpression(++currentToken);
                currentToken = Expression_p(currentToken);
            }
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_GTHAN){
                currentToken = SimpleExpression(++currentToken);
                currentToken = Expression_p(currentToken);
            }
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_LTHANEQ) {
                Console.WriteLine("found an Expression_p lthan-eq ");
                currentToken = SimpleExpression(++currentToken);
                currentToken = Expression_p(currentToken);
            }
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_GTHANEQ){
                currentToken = SimpleExpression(++currentToken);
                currentToken = Expression_p(currentToken);
            }
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_GTHANEQ){
                currentToken = SimpleExpression(++currentToken);
                currentToken = Expression_p(currentToken);
            }
            else if (tokens[currentToken] == (int)TOKENSPASCAL.T_NOTEQUAL){
                currentToken = SimpleExpression(++currentToken);
                currentToken = Expression_p(currentToken);
            }
            else {
                Console.WriteLine("no statement returned w/o incremnting");
            }
            return currentToken;
            }

        int SimpleExpression(int currentToken) {
                Console.WriteLine("SimpleExpression() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );
            //not incrementing here as we want to pass term a number token
                currentToken = Term(currentToken);
            //not incrementing as Term will increment if a term was found
                currentToken = SimpleExpression_p(currentToken);
            Console.WriteLine("SimpleExpression() - returning Current token index " + currentToken + " , token id " + tokens[currentToken] + "  is " + Tokens[currentToken].name);
            return currentToken;
            }

        int SimpleExpression_p(int currentToken) {
            Console.WriteLine("SimpleExpression_p() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_PLUS)
                {
                    currentToken = Term(++currentToken);
                    currentToken = SimpleExpression_p(++currentToken);
                }
                else if (tokens[currentToken] == (int)TOKENSPASCAL.T_MINUS)
                {
                    currentToken = Term(++currentToken);
                    currentToken = SimpleExpression_p(++currentToken);
                }
                else if (tokens[currentToken] == (int)TOKENSPASCAL.T_NR)
                {
                //not incmrementing becuase Simeple_Expression_P doesnt handle numbers 
                currentToken = Term(currentToken);
                //currentToken = SimpleExpression_p(++currentToken);
                }else{
                    Console.WriteLine("no SimpleExpression returned w/o incremnting");
                }
                return currentToken;
            }

        // should check if the current Token in a unsinged in, character-string or unsigned float
        //we don't increment in UnsignedConstant because we only increment if A) we matched the currentToken with a terminal
        //                                                B) we need to check the next token (increment it with advance vv) 
        //                                                currentToken = advance(++currentToken);
        int UnsignedConstant(int currentToken) {
            Console.WriteLine("UnsignedConstant() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );

            if (tokens[currentToken] == (int)TOKENSPASCAL.T_NR) {
                //dont increment cos we want to pass the number to unsinged int
                currentToken = UnsignedInterger(currentToken);
            } else if (tokens[currentToken] == (int)TOKENSPASCAL.T_STRING) {
                currentToken = CharacterString(currentToken);
            }else {
                Console.WriteLine("no unsinged constant found");
            }
            return currentToken;
        }
        // if currentToken is an interger increment the currentToken and return, otherwise return the token that wasnt an int 
        int UnsignedInterger(int currentToken) {
            Console.WriteLine("UnsignedInterger() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );
            //increment because we're at the end of a chain
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_NR)
            {
                return ++currentToken;
            }
            //should probably fail here
            return currentToken;
        }
        //same as unsigned int
        int CharacterString(int currentToken) {
            Console.WriteLine("CharacterString() Current token index " + currentToken + " , token id " + tokens[currentToken] +"  is "+Tokens[currentToken].name );
            if (tokens[currentToken] == (int)TOKENSPASCAL.T_STRING) {
                return ++currentToken;
            }
            return currentToken;
        }
    }
}
