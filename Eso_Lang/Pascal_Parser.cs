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
                Console.WriteLine("got program");
                Id(Tokens[currentToken++]);
            }else{
                Console.WriteLine("no program found");
            }


            if(t.id == (int)TOKENSPASCAL.T_IDENT)
            {
                Console.WriteLine("got id");
                Id(Tokens[currentToken++]);
            }else{
                Console.WriteLine("no id found");
             }


            if (t.id == (int)TOKENSPASCAL.T_LPAR)
            {
                Console.WriteLine("got lpar");
                Id(Tokens[currentToken++]);
              
            }

            if (t.id == (int)TOKENSPASCAL.T_RPAR)
            {

            }

            if (t.id == (int)TOKENSPASCAL.T_SCOLON)
            {
                Console.WriteLine("got ;");
                Id(Tokens[currentToken++]);
            }
            else
            {
                Console.WriteLine("no ; found");
            }

            if(t.id == (int)TOKENSPASCAL.T_PERIOD)
            {
                Console.WriteLine("got .");
                Id(Tokens[currentToken++]);
            }else{
                Console.WriteLine("no . found ");
            }

            return 0;

        }

        private void Id(Token t) { 
            if (t.id == (int)TOKENSPASCAL.T_PROGRAM) 
            {
                Console.WriteLine("got id");
                Id(Tokens[currentToken++]);
            }
        }
        private void Block(Token t) {  }


        private void Statement_list(Token t) { }
        private void Statement(Token t) {  }

        private void Expression(Token t) {  }
        private void Expression_p(Token t) {  }

    }
}
