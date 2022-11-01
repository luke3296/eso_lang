using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eso_Lang
{
    class Pascal_Parser
    {
        int lookAhead = -1;
        int currentToken = 0;
        int ret;
        List<Token> Tokens;

        public Pascal_Parser(List<Token> toks)
        {
            this.Tokens = toks;
        }

        public int Parse()
        {
            currentToken = 0;
            lookAhead = -1; // Initialise to non-existing token ID
            int ret = 1;
            Console.Write("check the input\n");
            foreach(Token tok in this.Tokens){
                   Console.Write(tok.name + " ");
             }
            Console.Write("\n");

            ret = Program(Tokens[currentToken++]);
            return ret;
        }

        public bool match(int token)
        {
            bool result;
            if (lookAhead == -1)
            {
                lookAhead = Tokens[currentToken].id;
            }

            result = (token == lookAhead);

            if ((token == lookAhead))
            {
                Console.WriteLine("Token[%d] =  %d matched", currentToken, token);
            }
            else
            {
                Console.WriteLine("Token[%d] = %d NOT matched", currentToken, token);
            }
            return result;
        }

        //probably need to change how advance works
        public void advance(Token level)
        {
            lookAhead = Tokens[++currentToken].id;
            Console.WriteLine("advance() called at level %d with next token %d", level, lookAhead);
        }

        private int Program(Token t) {

            if(t.id == (int)TOKENSPASCAL.T_PROGRAM){
                Id(Tokens[currentToken++]);
                if(t.id == (int)TOKENSPASCAL.T_SCOLON){
                    Block(Tokens[currentToken++]);
                }else if(t.id == (int)TOKENSPASCAL.T_LPAR){
                    Id_List(Tokens[currentToken++]);
                    if(t.id == (int)TOKENSPASCAL.T_RPAR){
                        if(t.id == (int)TOKENSPASCAL.T_SCOLON){
                            Block(Tokens[currentToken++]);
                        }
                    }
                }
            }

            if(t.id == (int)TOKENSPASCAL.T_PERIOD){
                return 1;

            }else{
                return 0;
            }
            /*
            if(t.id == (int)TOKENSPASCAL.T_PROGRAM){
                Console.WriteLine("got program");
                Id(Tokens[currentToken++]);
            }else{
                Console.WriteLine("no program found");
                return 1;
            }

            if (t.id == (int)TOKENSPASCAL.T_LPAR)
            {
                Console.WriteLine("got lpar");
                Id_List(Tokens[currentToken++]);
                if(t.id ==(int)TOKENSPASCAL.T_RPAR){
                 Console.WriteLine("got rpar");
                }
            }else if(t.id == (int)TOKENSPASCAL.T_SCOLON){
                Console.WriteLine("got spar");
            }else{
                return 1;
            }
            

            if (t.id == (int)TOKENSPASCAL.T_BLOCK)
            {
                Console.WriteLine("got block");
                Block(Tokens[currentToken++]);
            }
            else
            {
                Console.WriteLine("no block found");
            }


            if(t.id == (int)TOKENSPASCAL.T_PERIOD)
            {
                Console.WriteLine("got .");
                Id(Tokens[currentToken++]);
            }else{
                Console.WriteLine("no . found ");
                return 1;
            }
*/
            return 0;

        }

        private void Id(Token t) { 
            if (t.id == (int)TOKENSPASCAL.T_IDENT) 
            {
                Console.WriteLine("got id");
                Id(Tokens[currentToken++]);
            }
        }
        private void Block(Token t) { 
             if(t.id == (int)TOKENSPASCAL.T_BEGIN){
                Statement_list(Tokens[currentToken++]);
                if(t.id == (int)TOKENSPASCAL.T_END){

                }else{
                    Console.WriteLine("no end seen after block");
                }
             }
         }

        private void Id_List(Token t){
            if(t.id == (int)TOKENSPASCAL.T_IDENT){
                Id(Tokens[currentToken++]);
                if(t.id == (int)TOKENSPASCAL.T_COMMA){
                    Id_List(Tokens[currentToken++]);
                }else{
                Id(Tokens[currentToken++]);
                }
            }
        }
        private void term(Token t)
        {
            if (t.id == (int)TOKENSPASCAL.T_DIGIT)
            {
                factor_p(Tokens[currentToken++]);
            }
            else
            {
                Console.WriteLine("not a number");
            }
        }
        private void Expression(Token t) 
        {  
           if(t.id == (int)TOKENSPASCAL.T_DIGIT) 
            {
                term(Tokens[currentToken]);
                simple_expression_p(Tokens[currentToken++]);
            }
            else
            {
                Console.WriteLine("not a number");
            }
        }
        private void Simple_Expression(Token t){
         if(t.id == (int)TOKENSPASCAL.T_DIGIT) 
            {
                term(Tokens[currentToken]);
                Simple_Expression_P(Tokens[currentToken++]);
            }
       }
       private void simple_expression_p(Token t){
        if(t.id == (int)TOKENSPASCAL.T_PLUS) 
            {
                term(Tokens[currentToken++]);
                Simple_Expression_P(Tokens[currentToken++]);
            }
        if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                term(Tokens[currentToken++]);
                Simple_Expression_P(Tokens[currentToken++]);
            }
       }
       

        private void factor_p(Token t){
            if(t.id == (int)TOKENSPASCAL.T_DIVIDE) 
            {
                term(Tokens[currentToken++]);
                factor_P(Tokens[currentToken++]);
            }
            else if(t.id == (int)TOKENSPASCAL.T_MULTIPLY) 
            {
                term(Tokens[currentToken++]);
                factor_P(Tokens[currentToken++]);
            }
            else if(t.id == (int)TOKENSPASCAL.T_INTDIV) 
            {
                term(Tokens[currentToken++]);
                factor_P(Tokens[currentToken++]);
            }
            else if(t.id == (int)TOKENSPASCAL.T_INTMOD) 
            {
                term(Tokens[currentToken++]);
                factor_P(Tokens[currentToken++]);
            }
        }
        
        
           private void statement(Token t){
            if(t.id == (int)TOKENSPASCAL.T_IF) 
            {
                
                Expression(Tokens[currentToken++]);
                if (Tokens[currentToken++] == (int)TOKENSPASCAL.T_THEN) 
                {
                   Expression(Tokens[currentToken++])
                }

            }
        }
        

        
        private void Expression_p(Token t) {  }

    }
}
