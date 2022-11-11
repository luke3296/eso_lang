using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Eso_Lang;

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

        public  TreeNode<T> AddChild(T child)
        {
            TreeNode<T> childNode = new TreeNode<T>(child) { Parent = this };
            this.Children.Add(childNode);
            return childNode;
        }
        public void AddChild1(T child)
        {
            TreeNode<T> childNode = new TreeNode<T>(child) { Parent = this };
            this.Children.Add(childNode);
        }
        //needs a return type 
        /*
        public  getChildren()
        {
            return this.Children;
        }
        */

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
        public TreeNode(Token token_)
        {

            this.token = token_;
        }
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
        foreach (Token tok in this.Tokens)
        {
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

    private int Program(Token t)
    {

        if (t.id == (int)TOKENSPASCAL.T_PROGRAM)
        {
            Id(Tokens[currentToken++]);
            //cound't get this working 
            // rootnode=new TreeNode(t);
            //adding var = childnode automatically determins type
            if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_SCOLON)
            {
                var childnode1 = rootnode.AddChild(Tokens[currentToken]);
                Block(Tokens[currentToken++]);
                var childnode2 = childnode1.AddChild(Tokens[currentToken]);
            }
            else if (t.id == (int)TOKENSPASCAL.T_LPAR)
            {
                var childnode1 = rootnode.AddChild(Tokens[currentToken]);
                Id_List(Tokens[currentToken++]);
                var childnode2 = childnode1.AddChild(Tokens[currentToken]);
                if (t.id == (int)TOKENSPASCAL.T_RPAR)
                {
                    var childnode3 = childnode2.AddChild(Tokens[currentToken]);
                    if (t.id == (int)TOKENSPASCAL.T_SCOLON)
                    {
                        Block(Tokens[currentToken++]);
                        var childnode4 = childnode3.AddChild(Tokens[currentToken]);
                    }
                }
            }
        }

        if (t.id == (int)TOKENSPASCAL.T_PERIOD)
        {
            return 1;

        }
        else
        {
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

    private void Id(Token t)
    {
        if (t.id == (int)TOKENSPASCAL.T_IDENT)
        {
            Console.WriteLine("got id");
            Id(Tokens[currentToken++]);

        }
    }

    private void Block(Token t)
    {
        if (t.id == (int)TOKENSPASCAL.T_BEGIN)
        {

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

    private void Id_List(Token t)
    {
        if (t.id == (int)TOKENSPASCAL.T_IDENT)
        {
            Id(Tokens[currentToken++]);
            if (t.id == (int)TOKENSPASCAL.T_COMMA)
            {
                Id_List(Tokens[currentToken++]);
            }
            else
            {
                Id(Tokens[currentToken++]);
            }
        }
    }

    private void Statement_list(Token t)
    {
        Statement(Tokens[currentToken]);
        //Statement_node = statement_list_node.AddChild(Tokens[currentToken]);
        while (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_SCOLON)
        {
            Statement(Tokens[currentToken]);
            //merged
            //Expression_list_node.AddChild1(Tokens[currentToken]);
            Expression(Tokens[currentToken++]);
        }

    }

    //not implmented here, added to make things build
    private void Statement(Token t) { }
    private void Expression_list(Token t)
    {
        Expression(Tokens[currentToken]);
        while (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_COMMA)
        {
            //Expression_list_node doesnt exsist
            //Expression_list_node.AddChild1(Tokens[currentToken]);
            Expression(Tokens[currentToken++]);

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

    //mot implmented yet
    public void factor_p(Token t) { }
    public void simple_expression_p(Token t) { }
    private void Expression(Token t)
    {
        //neither of these exsist in this context
        //children = Expression_list_node.Children;
        //values and children dont exsist 
        /*
        for (int i = 0; i < values.Count; i++)
        {
            if (children[i] == t)
            {
                Expression_parent = children[i];
            }
        }
        */
        if (t.id == (int)TOKENSPASCAL.T_DIGIT)
        {
            //Expression+parent deosnt exsist
            //Expression_parent.AddChild1(t);
            term(Tokens[currentToken]);
            Simple_Expression(Tokens[currentToken++]);
            //Simple_expression_node = Expression_parent.AddChild(Tokens[currentToken]);

        }
        //handles variables
        else if (t.id == (int)TOKENSPASCAL.T_VAR)
        {
            //Expression_parent.AddChild1(t);
            if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_ASSIGN)
            {
               // Expression_parent.AddChild1(Tokens[currentToken]);

                Simple_Expression(Tokens[currentToken++]);
                //Expression_parent.AddChild1(Tokens[currentToken]);

            }
            else if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_PLUS)
            {
                var check = Tokens.Contains(t);
                if (check == false)
                {
                    Console.WriteLine("variable not defined");
                }
                else
                {
                   // Expression_parent.AddChild1(Tokens[currentToken]);
                }
            }
            else if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_MINUS)
            {
                var check = Tokens.Contains(t);
                if (check == false)
                {
                    Console.WriteLine("variable not defined");
                }
                else
                {
                   // Expression_parent.AddChild1(Tokens[currentToken]);
                }
            }



        }

        else
        {
            Console.WriteLine("not a number");
        }
    }
    private void Simple_Expression(Token t)
    {
        if (t.id == (int)TOKENSPASCAL.T_DIGIT)
        {
            term(Tokens[currentToken]);
            //does not exsist here
           // Simple_Expression_P(Tokens[currentToken++]);
        }
        //handling variables
        else if (t.id == (int)TOKENSPASCAL.T_VAR)
            if (t.id == (int)TOKENSPASCAL.T_PLUS)
            {
                term(Tokens[currentToken++]);
                //doesnt esist
                //Simple_expression_node.AddChild1(t);
                //Simple_expression_node.AddChild1(Tokens[currentToken]);
                //Expression_P(Tokens[currentToken++]);
            }
            else if (t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                term(Tokens[currentToken++]);
                //deosnt exsist
                //Simple_expression_node.AddChild1(t);
                //Simple_expression_node.AddChild1(Tokens[currentToken]);
                //Simple_Expression_P(Tokens[currentToken++]);
            }
            //handling variables
            else if (t.id == (int)TOKENSPASCAL.T_VAR)
            {
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_ASSIGN)
                {
                    //Simple_expression_node doesnt esist
                    Simple_Expression(Tokens[currentToken++]);
                    //Simple_expression_node.AddChild1(t);
                    Simple_Expression(Tokens[currentToken++]);
                    //Simple_expression_node.AddChild1(Tokens[currentToken]);
                }
                else if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_PLUS)
                {
                    //Simple_expression_node doesnt exsist
                   // Simple_expression_node.AddChild1(Tokens[currentToken]);
                    var check =Tokens.Contains(t);
                    if (check == false)
                    {
                        Console.WriteLine("variable not defined");
                    }
                    else
                    {
                        //Simple_expression_node.AddChild1(Tokens[currentToken]);
                    }
                }
                else if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_MINUS)
                {
                    //Simple_expression_node.AddChild1(Tokens[currentToken]);
                    var check =Tokens.Contains(t);
                    if (check == false)
                    {
                        Console.WriteLine("variable not defined");
                    }
                    else
                    {
                        //Simple_expression_node.AddChild1(Tokens[currentToken]);
                    }
                }

            }
    }


    /*
       void simple_expression_p(Token t){
        if(t.id == (int)TOKENSPASCAL.T_PLUS) 
            {
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                term(Tokens[currentToken++]);
                Expression_P(Tokens[currentToken++]);
            }
        else if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
            {
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                term(Tokens[currentToken++]);
                Simple_Expression_P(Tokens[currentToken++]);
            }
        //handles variables
        else if(t.id == (int)TOKENSPASCAL.T_VAR)
        else if(t.id == (int)TOKENSPASCAL.T_VAR)
        {
          var check =Tokens.Contains(t);
                   if (check == false)
                   {
                     Console.WriteLine("variable not defined");
                     Console.WriteLine("variable not defined");
                   }
          Simple_expression_node.AddChild1(Tokens[currentToken]); 
        }
        
       }

         void Expression_p(Token t) 
        {
            if (t.id == (int)TOKENSPASCAL.T_PLUS)
            {
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                term(Tokens[currentToken++]);
                Simple_Expression_P(Tokens[currentToken++]);
                Simple_expression_node.AddChild1(Tokens[currentToken]);

            }
            }
          else if(t.id == (int)TOKENSPASCAL.T_MINUS)
            {
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                term(Tokens[currentToken++]);
                Simple_Expression_P(Tokens[currentToken++]);
            }
            else if (t.id == (int)TOKENSPASCAL.T_MINUS)
            {
            {
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                term(Tokens[currentToken++]);
                Simple_Expression_P(Tokens[currentToken++]);
                Simple_expression_node.AddChild1(Tokens[currentToken]);
            }
          else if(t.id == (int)TOKENSPASCAL.T_VAR){
          else if(t.id == (int)TOKENSPASCAL.T_VAR){
                Simple_expression_node.AddChild1(Tokens[currentToken]);
                {
                   Expression(Tokens[currentToken++]);
                   Simple_expression_node.AddChild1(Tokens[currentToken]);
                   Expression(Tokens[currentToken++]);
                   Simple_expression_node.AddChild1(Tokens[currentToken]);
                else {
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

         void statement(Token t){
            if(t.id == (int)TOKENSPASCAL.T_IF) 
            {
                IF_node = statement_node.AddChild(Tokens[currentToken]);
                Expression(Tokens[currentToken++]);
                Expression_node = statement_node.AddChild(Tokens[currentToken]);
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_THEN) 
                {
                   Expression(Tokens[currentToken++])
                }
                else{
                    Console.WriteLine("Error, if statement is incomplete")
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
                    Console.WriteLine("Error, while statement is incomplete")
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
                else if((Tokens[currentToken++].id != (int)TOKENSPASCAL.T_SCOLON or Tokens[currentToken++] != (int)TOKENSPASCAL.T_END)){
                    Console.WriteLine("Error, begin statement is incomplete")
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
                    Console.WriteLine("Error, assigning is incomplete")
                }
            }
            else if(t.id == (int)TOKENSPASCAL.T_WRITE){
                if (Tokens[currentToken++].id == (int)TOKENSPASCAL.T_LPAR) 
                {  statement_node.AddChild1(Tokens[currentToken]);
                   write_parameter_list(Tokens[currentToken++]);
                   if ((Tokens[currentToken++] != (int)TOKENSPASCAL.T_RPAR) or (Tokens[currentToken++] != (int)TOKENSPASCAL.SCOLON)){
                      Console.WriteLine("Error, list incomplete") 
                   }
                   statement_node.AddChild(Tokens[currentToken]);
                   statement_node.AddChild(Tokens[currentToken]);
                   statement_node.AddChild(Tokens[currentToken++]);
                }
                else {
                    Console.WriteLine("Error, statement is incomplete");
                }
            }

        }
        private void write_parameter_list(Token t){
         if (t.id == (int)TOKENSPASCAL.T_LPAR)
         {
            Parameter_node.AddChild1(t);
            expression_list(Tokens[currentToken++]);
            if (Tokens[currentToken++].id != (int)TOKENSPASCAL.R_LPAR){
               Console.WriteLine("Error, list incomplete"); 
            }
            else{
        }
    */
}


/*
        }
           
    }
}
*/