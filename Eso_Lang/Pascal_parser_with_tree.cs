using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Eso_Lang
{

     public class TreeNode<T>
{

    public T Data { get; set; }
    //public TreeNode<T> Parent { get; set; }
    public List<TreeNode<T>> Children { get; set; }

    public TreeNode(T data)
    {
        this.Data = data;
        this.Children = new List<TreeNode<T>>();
    }
    
    

    public TreeNode<T> AddChild(T child)
    {
        TreeNode<T> childNode = new TreeNode<T>(child); //{ Parent = this };
        this.Children.Append(childNode);
        return childNode;
    }
    public void  AddChild1(T child)
    {
        TreeNode<T> childNode = new TreeNode<T>(child);// { Parent = this };
        this.Children.Append(childNode);
    }
    public  List<TreeNode<T>> getChildren()
    {
        return this.Children;
    }

}
   
     class Pascal_Parser
    {
        int lookAhead = -1;
        int currentToken = 0;
        int ret;
       
        
        List<Token> Tokens;
        TreeNode<Token> rootnode;

        TreeNode<Token> childnode4;
         TreeNode<Token>  Block_node;
         TreeNode<Token> IF_node{ get; set; }

        TreeNode<Token> childnode3 { get; set; }
        TreeNode<Token> statement_list_node { get; set; }
        
        TreeNode<Token> Expression_node { get; set; }
        TreeNode<Token> statement_node { get; set; }
        TreeNode<Token> THEN_node { get; set; }
        TreeNode<Token> WHILE_node { get; set; }
        TreeNode<Token> Do_node { get; set; }
        TreeNode<Token> Parameter_node { get; set; }
        TreeNode<Token> expression_list_node { get; set; }
        TreeNode<Token> Simple_expression_node { get; set; }
        TreeNode<Token> Expression_parent { get; set; }
        public List<TreeNode<Token>> children { get; private set; }

        public Pascal_Parser(List<Token> toks)
        {
            this.Tokens = toks;
            this.rootnode=new TreeNode<Token>(toks[0]);
            this.childnode4=new TreeNode<Token>(toks[0]);
            this.Block_node=new TreeNode<Token>(toks[0]);
            this.childnode3=new TreeNode<Token>(toks[0]);
            this.statement_list_node=new TreeNode<Token>(toks[0]);
            this.statement_node=new TreeNode<Token>(toks[0]);
            this.THEN_node= new TreeNode<Token>(toks[0]);
            this.WHILE_node= new TreeNode<Token>(toks[0]);
            this.Do_node=new TreeNode<Token>(toks[0]);
            this.Parameter_node=new TreeNode<Token>(toks[0]);
            this.expression_list_node=new TreeNode<Token>(toks[0]);
            this.Simple_expression_node=new TreeNode<Token>(toks[0]);
            this.Expression_parent=new TreeNode<Token>(toks[0]);
            this.IF_node=new TreeNode<Token>(toks[0]);
            this.Expression_node= new TreeNode<Token>(toks[0]);
            this.children=new List<TreeNode<Token>>();
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
                rootnode=new TreeNode<Token>(t);
                Id(Tokens[currentToken++]);
                TreeNode<Token> rootnode_child=rootnode.AddChild(Tokens[currentToken++]);

                if(Tokens[currentToken++].id == (int)TOKENSPASCAL.T_SCOLON){
                    TreeNode<Token> childnode1=rootnode.AddChild(Tokens[currentToken]);
                    Block(Tokens[currentToken++]);
                    TreeNode<Token> childnode2 = childnode1.AddChild(Tokens[currentToken]);
                }else if(t.id == (int)TOKENSPASCAL.T_LPAR){
                    TreeNode<Token> childnode1=rootnode.AddChild(Tokens[currentToken]);
                    Id_List(Tokens[currentToken++]);
                    TreeNode<Token> childnode2 = childnode1.AddChild(Tokens[currentToken]);
                    if(t.id == (int)TOKENSPASCAL.T_RPAR){
                     TreeNode<Token> childnode3 = childnode2.AddChild(Tokens[currentToken]);
                        if(t.id == (int)TOKENSPASCAL.T_SCOLON){
                            Block(Tokens[currentToken++]);
                            childnode4 = childnode3.AddChild(Tokens[currentToken]);
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
                if (childnode4.Data==t){
                   TreeNode<Token> Block_node = childnode4.AddChild(Tokens[currentToken]); 
                }
                else{
                  TreeNode<Token>  Block_node = childnode3.AddChild(Tokens[currentToken]);
                }
                Statement_list(Tokens[currentToken++]);
                 TreeNode<Token> Statement_list_node = Block_node.AddChild(Tokens[currentToken]);
                if(t.id == (int)TOKENSPASCAL.T_END){
                  TreeNode<Token> End_node = Block_node .AddChild(Tokens[currentToken]);
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

        private void Statement_list(Token t) { 
           statement(Tokens[currentToken]);
           statement_node = statement_list_node.AddChild(Tokens[currentToken]);
           while (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_SCOLON)
           {
            statement(Tokens[currentToken]);
           }
           
        }
        private void Expression_list(Token t) { 
           Expression(Tokens[currentToken]);
           expression_list_node.AddChild1(Tokens[currentToken]);
           while (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_COMMA)
           {
            expression_list_node.AddChild1(Tokens[currentToken]);
            Expression(Tokens[currentToken++]);
            
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

        private void Expression(Token t) 
        {  
           children = expression_list_node.Children;
           for(int i = 0; i < children.Count; i++){
            if (children[i].Data==t){
              Expression_parent = children[i];
            }
           }
           if(t.id == (int)TOKENSPASCAL.T_DIGIT) 
            {
                Expression_parent.AddChild1(t);
                term(Tokens[currentToken]);
                Simple_Expression(Tokens[currentToken++]);
                Simple_expression_node=Expression_parent.AddChild(Tokens[currentToken]);

            }
            //handles variables
            else if(t.id == (int)TOKENSPASCAL.T_VAR)
            {
                 Expression_parent.AddChild1(t);
                if (Tokens[currentToken++].id==(int)TOKENSPASCAL.T_ASSIGN){
                   Expression_parent.AddChild1(Tokens[currentToken]);
                   Simple_Expression(Tokens[currentToken++]);
                   Expression_parent.AddChild1(Tokens[currentToken]);
                }
                else if(Tokens[currentToken++].id==(int)TOKENSPASCAL.T_PLUS){
                   var check = Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
                   else{
                    Expression_parent.AddChild1(Tokens[currentToken]);
                   }
                }
                else if(Tokens[currentToken++].id==(int)TOKENSPASCAL.T_MINUS){
                   var check = Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
                   else{
                    Expression_parent.AddChild1(Tokens[currentToken]);
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
                term(Tokens[currentToken]);
                Simple_expression_node.AddChild1(t);
                simple_expression_p(Tokens[currentToken++]);

            }
        if(t.id == (int)TOKENSPASCAL.T_PLUS) 
            {
                term(Tokens[currentToken++]);
                Simple_expression_node.AddChild1(t);
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                Expression_p(Tokens[currentToken++]);
            }
        else if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                term(Tokens[currentToken++]);
                Simple_expression_node.AddChild1(t);
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                 simple_expression_p(Tokens[currentToken++]);
            }
            //handling variables
        else if(t.id == (int)TOKENSPASCAL.T_VAR)
            {
                Simple_expression_node.AddChild1(t);
                if (Tokens[currentToken++].id==(int)TOKENSPASCAL.T_ASSIGN){
                   Simple_expression_node.AddChild1(t);
                   Simple_Expression(Tokens[currentToken++]);
                   Simple_expression_node.AddChild1(Tokens[currentToken]);
                }
                else if(Tokens[currentToken++].id==(int)TOKENSPASCAL.T_PLUS){
                   Simple_expression_node.AddChild1(Tokens[currentToken]);
                   var check = Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
                   else{
                     Simple_expression_node.AddChild1(Tokens[currentToken]);
                   }
                }
                else if(Tokens[currentToken++].id==(int)TOKENSPASCAL.T_MINUS){
                   Simple_expression_node.AddChild1(Tokens[currentToken]);
                   var check = Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
                   else{
                    Simple_expression_node.AddChild1(Tokens[currentToken]);
                   }
                }

                }
            }
        
       
       private void simple_expression_p(Token t){
        if(t.id == (int)TOKENSPASCAL.T_PLUS) 
            {
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                term(Tokens[currentToken++]);
                Expression_p(Tokens[currentToken++]);
                Simple_expression_node.AddChild1(Tokens[currentToken]);
            }
        else if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                term(Tokens[currentToken++]);
                simple_expression_p(Tokens[currentToken++]);
                Simple_expression_node.AddChild1(Tokens[currentToken]);
            }
        //handles variables
        else if(t.id == (int)TOKENSPASCAL.T_VAR)
        {
          Simple_expression_node.AddChild1(Tokens[currentToken]);
          var check = Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                   }
          Simple_expression_node.AddChild1(Tokens[currentToken]); 
        }
        
       }

        private void Expression_p(Token t) 
        {  
           if(t.id == (int)TOKENSPASCAL.T_PLUS) 
            {
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                term(Tokens[currentToken++]);
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                simple_expression_p(Tokens[currentToken++]);
                Simple_expression_node.AddChild1(Tokens[currentToken]);

            }
          else if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                term(Tokens[currentToken++]);
                simple_expression_p(Tokens[currentToken++]);
                Simple_expression_node.AddChild1(Tokens[currentToken]);
            }
          else if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                term(Tokens[currentToken++]);
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                simple_expression_p(Tokens[currentToken++]);
                Simple_expression_node.AddChild1(Tokens[currentToken]);
            }
          else if(t.id == (int)TOKENSPASCAL.T_VAR){
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_ASSIGN) 
                {
                   Simple_expression_node.AddChild1(Tokens[currentToken]);
                   Expression(Tokens[currentToken++]);
                   Simple_expression_node.AddChild1(Tokens[currentToken]);
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
        private void statement(Token t){
            if(t.id == (int)TOKENSPASCAL.T_IF) 
            {
                IF_node = statement_node.AddChild(Tokens[currentToken]);
                Expression(Tokens[currentToken++]);
                Expression_node = statement_node.AddChild(Tokens[currentToken]);
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_THEN) 
                {
                   THEN_node = statement_node.AddChild(Tokens[currentToken]);
                   Expression(Tokens[currentToken++]);
                   Expression_node = statement_node.AddChild(Tokens[currentToken]);
                }
                else{
                    Console.WriteLine("Error, if statement is incomplete");
                }

            }
            else if(t.id == (int)TOKENSPASCAL.T_WHILE) 
            {
                WHILE_node = statement_node.AddChild(Tokens[currentToken]);
                Expression(Tokens[currentToken++]);
                Expression_node = statement_node.AddChild(Tokens[currentToken]);

                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_DO) 
                {
                   Do_node = statement_node.AddChild(Tokens[currentToken]);
                   Expression(Tokens[currentToken++]);
                }
                else{
                    Console.WriteLine("Error, while statement is incomplete");
                }

            }
            else if(t.id == (int)TOKENSPASCAL.T_BEGIN){
                statement_list(Tokens[currentToken++]);
                IF_node = statement_node.AddChild(Tokens[currentToken]);
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_DO) 
                {
                   
                   Expression(Tokens[currentToken++]);
                   statement_node.AddChild1(Tokens[currentToken]);
                }
                else if((Tokens[currentToken++].id != (int)TOKENSPASCAL.T_SCOLON || Tokens[currentToken++].id != (int)TOKENSPASCAL.T_END)){
                    Console.WriteLine("Error, begin statement is incomplete");
                }
            }
            else if(t.id == (int)TOKENSPASCAL.T_VAR){
                 statement_node.AddChild1(Tokens[currentToken]);
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_ASSIGN) 
                {   statement_node.AddChild1(Tokens[currentToken]);
                   Expression(Tokens[currentToken++]);
                    statement_node.AddChild1(Tokens[currentToken]);
                }
                else {
                    Console.WriteLine("Error, assigning is incomplete");
                }
            }
            else if(t.id == (int)TOKENSPASCAL.T_WRITELINE){
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_LPAR) 
                {  statement_node.AddChild1(Tokens[currentToken]);
                   write_parameter_list(Tokens[currentToken++]);
                   Parameter_node = statement_node.AddChild(Tokens[currentToken]);
                   if ((Tokens[currentToken++].id != (int)TOKENSPASCAL.T_RPAR) || (Tokens[currentToken++].id != (int)TOKENSPASCAL.T_SCOLON)){
                      Console.WriteLine("Error, list incomplete");
                   }
                   statement_node.AddChild(Tokens[currentToken]);
                   statement_node.AddChild(Tokens[currentToken++]);
                }
                else {
                    Console.WriteLine("Error, statement is incomplete");
                }
            }

        }

        private void statement_list(Token token)
        {
            throw new NotImplementedException();
        }

        private void write_parameter_list(Token t){
         if (t.id == (int)TOKENSPASCAL.T_LPAR)
         {
            Parameter_node.AddChild1(t);
            Expression_list(Tokens[currentToken++]);
            expression_list_node= Parameter_node.AddChild(t);
            if (Tokens[currentToken++].id != (int)TOKENSPASCAL.T_RPAR){
               Console.WriteLine("Error, list incomplete"); 
            }
            else{
                Parameter_node.AddChild1(t);
            }
         }

        }
     }
       
    }
