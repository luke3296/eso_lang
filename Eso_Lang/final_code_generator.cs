using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eso_Lang;
using static Eso_Lang.Token;
using static Eso_Lang.Program;
using System.IO;  // include the System.IO namespace
namespace Eso_Lang
{
    public class  Code_generator{
    
       public static List<Token> tokenRegexsC;

      
       
        public  string output_string { get; private set; }
        

        public Code_generator(){
        this.output_string= "";
        tokenRegexsC = new List<Token>();
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_PROGRAM, "Program", @"program"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_SCOLON, "Block-Delimiter", @";"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_BEGIN, "Begin", @"begin"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_END, "End", @"end"));
        // tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_PERIOD, "Period", @"\."));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_WRITELINE, "write Line", @"writeln"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_COMMENT, "CommentL", @"\(\*"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_COMMENT, "CommentR", @"\*\)"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_GTHANEQ, "Greater-or-Equal-Operator", @"\>="));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_LTHANEQ, "Less-or-Equal-Operator", @"\<="));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_INTDIV, "Integer-Division", @"div"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_INTMOD, "Modulous", @"mod"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_VAR, "Modulous", @"var"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_CONST, "Modulous", @"const"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_IF, "IF", @"if"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_THEN, "THEN", @"then"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_ELSE, "ElSE", @"else"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_FOR, "FOR", @"for"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_WHILE, "WHILE", @"while"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_DO, "DO", @"do"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_REPEAT, "REPEAT", @"repeat"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_UNTIL, "UNTIL", @"until"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_LPAR, "LPAR", @"\("));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_RPAR, "RPAR", @"\)"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_COMMA, "COMMA", @"\,"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_APOSTROPHE, "APOSTROPHE", "\'"));
            //checked last
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_IDENT, "Indetifier", @"\b[a-zA-Z]\b"));
            // ^ don't change    v can change
            
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_PLUS, "Addition-Operator", @"\+\s"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_MINUS, "Subtraction-Operator", @"\-\s"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_DIVIDE, "Division-Operator", @"/\s"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_MULTIPLY, "Multiplication-Operator", @"\*\s"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_SLEFT, "Shift-Left-Operator", @"<<\s"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_SRIGHT, "Shift-Right-Operator", @">>\s"));
            //tokensRegexPascal.Add(new Token((int)TOKENSPASCAL.T_ASSIGN, "Assignment-Operator", @":=\s"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_ASSIGN, "Assignment-Operator", @"=\s"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_GTHAN, "Greater-Than-Operator", @">\s"));
        tokenRegexsC.Add(new Token((int)TOKENSPASCAL.T_LTHAN, "Less-Than-Operator", @"<\s"));
       


       

    }
    void create_file(string filename){
      string class_name = filename;
      string file = filename + ".txt";
      File.Create(file);
    }
    public void convert_to_c(Token t){
      Console.WriteLine(t.id);
      foreach(Token s in tokenRegexsC){
        if(t.id==s.id){
         this.output_string = this.output_string + " "  + s.tokenRE;
      }
      }
    
    }
    public void output(){
      Console.WriteLine(this.output_string);
     
    }
    void write_to_file(){
     File.WriteAllText("filename.txt", this.output_string); 
    }
    public void create_fie (List<Token> parsed_tokens) {
     string file_name = parsed_tokens[1].stringval;
    }
    public void generate_code(List<Token> parsed_tokens){
    
      
      //create_file(token1.name);
      int i =0;
      for(i=2; i < parsed_tokens.Count; i++){
         convert_to_c(parsed_tokens[i]);
      }
    }

        //adding main here as it must be wwithin a class, renameing to Main 1 so that only 1 Main
        //exsists call Code_generator.Main1 from Program.Main to call
       
    }
  }

