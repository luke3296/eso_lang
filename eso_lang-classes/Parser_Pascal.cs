using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eso_lang_classes;

namespace eso_lang_classes
{
    public class Parser_Pascal
    {
        int lookAhead = -1;
        int currentToken = 0;
        int ret;
        List<Token>  Tokens;

        public Parser_Pascal(List<Token> toks){
            this.Tokens = toks;
        }

        public int Parse(){
            	currentToken = 0;
	            lookAhead = -1; // Initialise to non-existing token ID
	            int ret = 1;
                return ret;
        }

        public bool match(int token){
            bool result;
            if (lookAhead == -1){
		        lookAhead = Tokens[currentToken].id;
            }

            result = (token == lookAhead);

            if((token == lookAhead)){
                Console.WriteLine( "Token[%d] =  %d matched",currentToken,token);   
	        }else{
                Console.WriteLine( "Token[%d] = %d NOT matched",currentToken,token);
            }
            return result;
        }

        public void advance(int level){
            lookAhead = Tokens[++currentToken].id;
            	Console.WriteLine( "advance() called at level %d with next token %d", level, lookAhead);
        }   
    }
}