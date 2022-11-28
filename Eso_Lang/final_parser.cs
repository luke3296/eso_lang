using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eso_Lang;
namespace Eso_Lang
{
    class Pascal_Parser
    {
        int lookAhead = -1;
        int currentToken = 0;
        int ret;
       
        
        public  List<Token> Tokens{ get; set; }
        public List<Token> parsed_tokens{ get; set; }

        public Pascal_Parser(List<Token> toks)
        {
            this.Tokens = toks;
            this.parsed_tokens=new List<Token>();
            
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

            ret = Program(Tokens[currentToken]);
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
                parsed_tokens.Add(t);
                Id(Tokens[currentToken++]);
                
                if(Tokens[currentToken++].id == (int)TOKENSPASCAL.T_SCOLON){

                    parsed_tokens.Add(Tokens[currentToken]);
                    Block(Tokens[currentToken++]);
                    
                }else if(Tokens[currentToken++].id == (int)TOKENSPASCAL.T_LPAR){
                    parsed_tokens.Add(Tokens[currentToken]);
                    
                    Id_List(Tokens[currentToken++]);
                    
                    if(Tokens[currentToken++].id == (int)TOKENSPASCAL.T_RPAR){
                      parsed_tokens.Add(Tokens[currentToken]);   
                      
                        if(Tokens[currentToken++].id == (int)TOKENSPASCAL.T_SCOLON){
                            parsed_tokens.Add(Tokens[currentToken]);   
                            Block(Tokens[currentToken++]);
                            
                        }
                    }
                }
            }

            if(Tokens[currentToken++].id == (int)TOKENSPASCAL.T_PERIOD){
                parsed_tokens.Add(Tokens[currentToken]);
                return 0;

            }else{
                return 1;
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
           

        }

        private void Id(Token t) { 
            if (t.id == (int)TOKENSPASCAL.T_IDENT) 
            {
                
                Console.WriteLine("got id");
                
                parsed_tokens.Add(t);
            }
        }
        
        private void Block(Token t) { 
             if(t.id == (int)TOKENSPASCAL.T_BEGIN){
                parsed_tokens.Add(t);
                if(Tokens[currentToken++].id !=(int)TOKENSPASCAL.T_END){
                   Statement_list(Tokens[currentToken]);
                   parsed_tokens.Add(Tokens[currentToken]);  
                    if(Tokens[currentToken].id == (int)TOKENSPASCAL.T_END){
                        parsed_tokens.Add(t);
                       }else{
                         Console.WriteLine("no end seen after block");
                }
                }
               
                else if(Tokens[currentToken].id == (int)TOKENSPASCAL.T_END){
                  parsed_tokens.Add(t);
                }else{
                    Console.WriteLine("no end seen after block");
                }
             }
         }

        private void Id_List(Token t){

            if(t.id == (int)TOKENSPASCAL.T_IDENT){
                Id(Tokens[currentToken++]);
                parsed_tokens.Add(Tokens[currentToken]);
                if(Tokens[currentToken++].id == (int)TOKENSPASCAL.T_COMMA){
                    parsed_tokens.Add(Tokens[currentToken]);
                    Id_List(Tokens[currentToken++]);
                }else{
               
                }
            }
        }

        private void Statement_list(Token t) { 
           statement(t);
           parsed_tokens.Add(t);
           while (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_SCOLON)
           {
             parsed_tokens.Add(Tokens[currentToken]);
             statement(Tokens[currentToken++]);
   
           }
           
        }
        private void Expression_list(Token t) { 
           Expression(t);
           parsed_tokens.Add(t);
           while (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_COMMA)
           {
            parsed_tokens.Add(Tokens[currentToken]);
            Expression(Tokens[currentToken++]);
            
            
           }
           
        }

        
        private void term(Token t)
        {
             if(t.id == (int)TOKENSPASCAL.T_DIGIT) 
            {
                factor_p(Tokens[currentToken++]);
                parsed_tokens.Add(t);
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
                parsed_tokens.Add(t);
                term(t);
                Simple_Expression(Tokens[currentToken++]);
               
            }
            //handles variables
            else if(t.id == (int)TOKENSPASCAL.T_VAR)
            {
                 parsed_tokens.Add(t);
                
                if (Tokens[currentToken++].id==(int)TOKENSPASCAL.T_ASSIGN){
                   parsed_tokens.Add(Tokens[currentToken]); 
                   Simple_Expression(Tokens[currentToken++]);
                   
                }
                else if(Tokens[currentToken++].id==(int)TOKENSPASCAL.T_PLUS){

                   var check = Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
                   else{
                    parsed_tokens.Add(Tokens[currentToken]);
                    Simple_Expression(Tokens[currentToken++]);
                   }
                }
                else if(Tokens[currentToken++].id==(int)TOKENSPASCAL.T_MINUS){
                   var check = Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
                   else{
                    parsed_tokens.Add(Tokens[currentToken]);
                    Simple_Expression(Tokens[currentToken++]);
                   }
                }
                
                
                
            }

            else{
                Console.WriteLine("not a number");
            }
        }
       private void Simple_Expression(Token t){
        if(t.id == (int)TOKENSPASCAL.T_DIGIT) 
            {
                
                term(t);
                Expression_p(Tokens[currentToken++]);


            }
        if(t.id == (int)TOKENSPASCAL.T_PLUS) 
            {
                parsed_tokens.Add(t);
                term(Tokens[currentToken++]);
                Expression_p(Tokens[currentToken++]);
            }
        else if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                parsed_tokens.Add(t);
                term(Tokens[currentToken++]);

                simple_expression_p(Tokens[currentToken++]);
            }
            //handling variables
        else if(t.id == (int)TOKENSPASCAL.T_VAR)
            {
                parsed_tokens.Add(t);
                if (Tokens[currentToken++].id==(int)TOKENSPASCAL.T_ASSIGN){
                   parsed_tokens.Add(Tokens[currentToken]);
                   Simple_Expression(Tokens[currentToken++]);
                }
                else if(Tokens[currentToken++].id==(int)TOKENSPASCAL.T_PLUS){
                   parsed_tokens.Add(Tokens[currentToken]);
                   var check = Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
                   else{
                     Simple_Expression(Tokens[currentToken++]);
                   }
                }
                else if(Tokens[currentToken++].id==(int)TOKENSPASCAL.T_MINUS){
                   parsed_tokens.Add(Tokens[currentToken]);
                   var check = Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
                   else{
                    Simple_Expression(Tokens[currentToken++]);
                   }
                }

                }
        else{

                }
            }
        
       
       private void simple_expression_p(Token t){
        if(t.id == (int)TOKENSPASCAL.T_PLUS) 
            {
                parsed_tokens.Add(t);
                term(Tokens[currentToken++]);
                Simple_Expression(Tokens[currentToken++]);
            }
        else if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                parsed_tokens.Add(t);
                simple_expression_p(Tokens[currentToken++]);
            }
                
        //handles variables
        else if(t.id == (int)TOKENSPASCAL.T_VAR)
        {
          parsed_tokens.Add(t);
          var check = Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
         simple_expression_p(Tokens[currentToken++]);
        }
        
       }
       


        private void Expression_p(Token t) 
        {  
           if(t.id == (int)TOKENSPASCAL.T_PLUS) 
            {
                parsed_tokens.Add(t);
                term(Tokens[currentToken++]);
                Simple_Expression(Tokens[currentToken++]);
                

            }
          else if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                parsed_tokens.Add(t);   
                term(Tokens[currentToken++]);
                Simple_Expression(Tokens[currentToken++]);
               
            }
          else if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
               
                parsed_tokens.Add(t);
                term(Tokens[currentToken++]);
                Simple_Expression(Tokens[currentToken++]);
                
            }
          else if(t.id == (int)TOKENSPASCAL.T_VAR){
                parsed_tokens.Add(t);
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_ASSIGN) 
                {
                   parsed_tokens.Add(Tokens[currentToken]);
                   Expression(Tokens[currentToken++]);
                  
                }
                else {
                    Console.WriteLine("Error, assigning is incomplete");
                }

          }
          else{

          }

        }
       private void factor_p(Token t){
            if(t.id == (int)TOKENSPASCAL.T_DIVIDE) 
            {
                parsed_tokens.Add(t);
                term(Tokens[currentToken++]);
                factor_p(Tokens[currentToken++]);
            }
            else if(t.id == (int)TOKENSPASCAL.T_MULTIPLY) 
            {
                parsed_tokens.Add(t);
                term(Tokens[currentToken++]);
                factor_p(Tokens[currentToken++]);
            }
            else if(t.id == (int)TOKENSPASCAL.T_INTDIV) 
            {
                parsed_tokens.Add(t);
                term(Tokens[currentToken++]);
                factor_p(Tokens[currentToken++]);
            }
            else if(t.id == (int)TOKENSPASCAL.T_INTMOD) 
            {
                parsed_tokens.Add(t);
                term(Tokens[currentToken++]);
                factor_p(Tokens[currentToken++]);
            }
        }
        private void statement(Token t){
            if(t.id == (int)TOKENSPASCAL.T_IF) 
            {
                parsed_tokens.Add(t);
                Expression(Tokens[currentToken++]);
 
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_THEN) 
                {
                   parsed_tokens.Add(Tokens[currentToken]);
                   Expression(Tokens[currentToken++]);
                   
                }
                else{
                    Console.WriteLine("Error, if statement is incomplete");
                }

            }
            else if(t.id == (int)TOKENSPASCAL.T_WHILE) 
            {
                   parsed_tokens.Add(t);
                   Expression(Tokens[currentToken++]);
               

                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_DO) 
                {
                  
                   parsed_tokens.Add(t);
                   Expression(Tokens[currentToken++]);
                }
                else{
                    Console.WriteLine("Error, while statement is incomplete");
                }

            }
            else if(t.id == (int)TOKENSPASCAL.T_BEGIN){
                parsed_tokens.Add(t);
                statement_list(Tokens[currentToken++]);
            
                if((Tokens[currentToken++].id == (int)TOKENSPASCAL.T_SCOLON || Tokens[currentToken++].id != (int)TOKENSPASCAL.T_END)){

                    Console.WriteLine("Error, begin statement is incomplete");
                }
                if((Tokens[currentToken++].id != (int)TOKENSPASCAL.T_END)){

                }
            }
            else if(t.id == (int)TOKENSPASCAL.T_VAR){
                parsed_tokens.Add(t);
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_ASSIGN) 
                {   
                   parsed_tokens.Add(Tokens[currentToken]);
                   Expression(Tokens[currentToken++]);

                }
                else {
                    Console.WriteLine("Error, assigning is incomplete");
                }
            }
            else if(t.id == (int)TOKENSPASCAL.T_WRITELINE){
                 parsed_tokens.Add(t);
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_LPAR) 
                {  
                   parsed_tokens.Add(Tokens[currentToken]);
                   write_parameter_list(Tokens[currentToken++]);
                   
                   if ((Tokens[currentToken++].id != (int)TOKENSPASCAL.T_RPAR) ){
                      Console.WriteLine("Error, list incomplete");
                   }
                   else{
                    parsed_tokens.Add(Tokens[currentToken]);
                   }
                   if(Tokens[currentToken++].id != (int)TOKENSPASCAL.T_SCOLON){
                       Console.WriteLine("Error, statement is incomplete");
                   }
                   else{
                     parsed_tokens.Add(Tokens[currentToken]);
                   }

                }
                else {
                    Console.WriteLine("Error, statement is incomplete");
                }
            }

        }

        private void statement_list(Token token)
        {
            statement(token);
            if(Tokens[currentToken++].id != (int)TOKENSPASCAL.T_SCOLON){
                parsed_tokens.Add(Tokens[currentToken]);
            }
            else{
                Console.WriteLine("list not completed");
            }
            
        }

        private void write_parameter_list(Token t){
         if (t.id == (int)TOKENSPASCAL.T_LPAR)
         {
            parsed_tokens.Add(t);
            Expression_list(Tokens[currentToken++]);

            if (Tokens[currentToken++].id != (int)TOKENSPASCAL.T_RPAR){
               Console.WriteLine("Error, list incomplete"); 
            }
            else{
              parsed_tokens.Add(Tokens[currentToken]);
            }
         }

        }
     }
       
    }


