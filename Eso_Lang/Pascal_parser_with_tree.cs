using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Eso_Lang
{
    
    public class TreeNode<T> : IEnumerable<TreeNode<TreeNode>>
{

    public T Data { get; set; }
    public TreeNode<T> Parent { get; set; }
    public ICollection<TreeNode<T>> Children { get; set; }

    public TreeNode(T data)
    {
        this.Data = data;
        this.Children = new LinkedList<TreeNode<T>>();
    }

    public TreeNode<T> AddChild(T child)
    {
        TreeNode<T> childNode = new TreeNode<T>(child) { Parent = this };
        this.Children.Add(childNode);
        return childNode;
    }

        public IEnumerator<TreeNode<TreeNode>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
    //added 
    public class TreeNode
    {
        int id;
        Token token;
        public TreeNode(Token token_) {
         
            this.token = token_;
        }
    }

    class Pascal_Parser
    {
        int lookAhead = -1;
        int currentToken = 0;
        int ret;
        List<Token> Tokens;
        TreeNode<Token> rootnode;
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
                //cound't get this working 
               // rootnode=new TreeNode(t);
                //adding var = childnode automatically determins type
                if(Tokens[currentToken++].id == (int)TOKENSPASCAL.T_SCOLON){
                   var  childnode1=rootnode.AddChild(Tokens[currentToken]);
                    Block(Tokens[currentToken++]);
                    var childnode2 = childnode1.AddChild(Tokens[currentToken]);
                }else if(t.id == (int)TOKENSPASCAL.T_LPAR){
                    var childnode1=rootnode.AddChild(Tokens[currentToken]);
                    Id_List(Tokens[currentToken++]);
                    var childnode2 = childnode1.AddChild(Tokens[currentToken]);
                    if(t.id == (int)TOKENSPASCAL.T_RPAR){
                     var childnode3 = childnode2.AddChild(Tokens[currentToken]);
                        if(t.id == (int)TOKENSPASCAL.T_SCOLON){
                            Block(Tokens[currentToken++]);
                            var childnode4 = childnode3.AddChild(Tokens[currentToken]);
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

                /* errors about what childnode4 is
                if (childnode4.Data==t){
                   Block_node = childnode4.AddChild(Tokens[currentToken]); 
                }
                else{
                    Block_node = childnode3.AddChild(Tokens[currentToken]);
                }
                Statement_list(Tokens[currentToken++]);
                Statement_list_node = Block_node.AddChild(Tokens[currentToken]);
                if(t.id == (int)TOKENSPASCAL.T_END){
                    End_node = End_node.AddChild(Tokens[currentToken]);
                }else{
                    Console.WriteLine("no end seen after block");
                }
                */
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

        private void Statement_list(Token t) {
            Statement(Tokens[currentToken]);
           while (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_SCOLON)
           {
                Statement(Tokens[currentToken]);
           }
           
        }
        private void Expression_list(Token t) {
            Statement(Tokens[currentToken]);
           while (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_COMMA)
           {
            Expression(Tokens[currentToken]);
           }
           
        }

        private void Statement(Token t) {  
           if (t.id == (int)TOKENSPASCAL.T_IF) 
            {
                Console.WriteLine("got id");
                Id(Tokens[currentToken++]);
            }
        }
        private void term(Token t)
        {
             if(t.id == (int)TOKENSPASCAL.T_DIGIT) 
            {
                factor_p(Tokens[currentToken++]);
            }
            else
            {
                Console.WriteLine("not a number");
            }
               
        }

        //mot implmented yet
        public void factor_p(Token t) { }
        public void simple_expression_p(Token t) { }
        private void Expression(Token t) 
        {  
           if(t.id == (int)TOKENSPASCAL.T_DIGIT) 
            {
                term(Tokens[currentToken]);
                Simple_Expression(Tokens[currentToken++]);
            }
            //handles variables
            else if(t.id == (int)TOKENSPASCAL.T_VAR)
            {
                if (Tokens[currentToken++].id==(int)TOKENSPASCAL.T_ASSIGN){
                    Simple_Expression(Tokens[currentToken++]);
                }
                else if(Tokens[currentToken++].id==(int)TOKENSPASCAL.T_PLUS){
                   var check =Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
                }
                else if(Tokens[currentToken++].id==(int)TOKENSPASCAL.T_MINUS){
                   var check =Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
                }
                
                
                
            }

            else{
                Console.WriteLine("not a number");
            }
        }
       private void Simple_Expression(Token t){
            if (t.id == (int)TOKENSPASCAL.T_DIGIT)
            {
                term(Tokens[currentToken]);
                simple_expression_p(Tokens[currentToken++]);
            }
            //handling variables
            else if (t.id == (int)TOKENSPASCAL.T_VAR)
            {
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_ASSIGN)
                {
                    Simple_Expression(Tokens[currentToken++]);
                }
                else if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_PLUS)
                {
                    var check = Tokens.Contains(t);
                    if (check == false)
                    {
                        Console.WriteLine("variable not defined");
                    }
                }
                else if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_MINUS)
                {
                    var check = Tokens.Contains(t);
                    if (check == false)
                    {
                        Console.WriteLine("variable not defined");
                    }
                }

                }
            }
       }

    /*
       void simple_expression_p(Token t){
        if(t.id == (int)TOKENSPASCAL.T_PLUS) 
            {
                term(Tokens[currentToken++]);
            Expression_p(Tokens[currentToken++]);
            }
        else if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                term(Tokens[currentToken++]);
                simple_expression_p(Tokens[currentToken++]);
            }
        //handles variables
        else if(t.id == (int)TOKENSPASCAL.T_VAR)
        {
          var check =Tokens.Contains(t);
                
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }  
        }
        
       }

         void Expression_p(Token t) 
        {
            if (t.id == (int)TOKENSPASCAL.T_PLUS)
            {
                term(Tokens[currentToken++]);
                simple_expression_p(Tokens[currentToken++]);
            }
            else if (t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                term(Tokens[currentToken++]);
                simple_expression_p(Tokens[currentToken++]);
            }
            else if (t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                term(Tokens[currentToken++]);
                simple_expression_p(Tokens[currentToken++]);
            }
            else if (t.id == (int)TOKENSPASCAL.T_VAR)
            {
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_ASSIGN)
                {
                    Expression(Tokens[currentToken++]);
                }
                else
                {
                    Console.WriteLine("Error, assigning is incomplete");
                }
            }
        }
         void factor_p(Token t){
            if(t.id == (int)TOKENSPASCAL.T_DIVIDE) 
            {
                term(Tokens[currentToken++]);
                    factor_p(Tokens[currentToken++]);
            }
            else if(t.id == (int)TOKENSPASCAL.T_MULTIPLY) 
            {
                term(Tokens[currentToken++]);
                    factor_p(Tokens[currentToken++]);
            }
            else if(t.id == (int)TOKENSPASCAL.T_INTDIV) 
            {
                term(Tokens[currentToken++]);
                    factor_p(Tokens[currentToken++]);
            }
            else if(t.id == (int)TOKENSPASCAL.T_INTMOD) 
            {
                term(Tokens[currentToken++]);
                    factor_p(Tokens[currentToken++]);
            }
        }

         void statement(Token t){
            if(t.id == (int)TOKENSPASCAL.T_IF) 
            {
                
                Expression(Tokens[currentToken++]);
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_THEN) 
                {
                        Expression(Tokens[currentToken++]);
                }
                else{
                        Console.WriteLine("Error, if statement is incomplete");
                }

            }
            else if(t.id == (int)TOKENSPASCAL.T_WHILE) 
            {
                
                Expression(Tokens[currentToken++]);
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_DO) 
                {
                   Expression(Tokens[currentToken++]);
                }
                else{
                        Console.WriteLine("Error, while statement is incomplete");
                }

            }
            else if(t.id == (int)TOKENSPASCAL.T_BEGIN){
                statement_list(Tokens[currentToken++]);
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_DO) 
                {
                   Expression(Tokens[currentToken++]);
                }
                else if((Tokens[currentToken++].id != (int)TOKENSPASCAL.T_SCOLON || Tokens[currentToken++].id != (int)TOKENSPASCAL.T_END)){
                Console.WriteLine("Error, begin statement is incomplete");
                }
            }
            else if(t.id == (int)TOKENSPASCAL.T_VAR){
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_ASSIGN) 
                {
                   Expression(Tokens[currentToken++]);
                }
                else {
                        Console.WriteLine("Error, assigning is incomplete");
                }
            }
            else if(t.id == (int)TOKENSPASCAL.T_WRITE){
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_LPAR) 
                {
                   write_parameter_list(Tokens[currentToken++]);
                   if ((Tokens[currentToken++].id != (int)TOKENSPASCAL.T_RPAR) || (Tokens[currentToken++].id != (int)TOKENSPASCAL.T_SCOLON)){
                Console.WriteLine("Error, list incomplete");
                   }
                }
                else {
                    Console.WriteLine("Error, statement is incomplete")
                }
            }

        }
        private void parameter_list(Token t){
         if (t.id == (int)TOKENSPASCAL.T_LPAR)
         {
            expression_list(Tokens[currentToken++]);
            if (Tokens[currentToken++].id != (int)TOKENSPASCAL.T_LPAR){
               Console.WriteLine("Error, list incomplete");
            }
         }
        }
    */
       
}

