using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eso_Lang;
using System.IO;  // include the System.IO namespace
//needs to be inside something
//File.SomeFileMethod();

namespace Eso_Lang
{
  class Alternative_code_generator{
    String output_string;
    public static List<Token> tokenRegexsC;

        //if these are methods they need return typesa
        /*
    create_file(string filename){
      string class_name = filename;
      string file = filename + ".txt";
      Create(file);
    }
        */

/*
    convert_to_c(Token t){
      foreach(token s in tokenRegexsC){
        if(t.id==s.id){
        output_string = output_string+ " "  + s.stringval;
      }
      }
    }

    */

        /*
    write_to_file(String output_string){
     File.WriteAllText("filename.txt", writeText); 
    }
        */
        /*
    node_traversal(TreeNode<Token> node){
      foreach(TreeNode<Token> child in node.getChildren()){
        Token=child.Data;
        convert_to_c(Token);
        if (child.getChildren() != null){
          child_traversal(child);

        }
      }
    }
        */

        /*
    public void data(TreeNode<Token> rootnode){
      var m=rootnode.Children[0];
      Token = m.data;
      create_file(m.name);
    }
        */

    int main(){
    // need to give parser list of Tokens
   // var parser = new Pascal_Parser_test();
    Console.WriteLine("cccccjjj");
            return 0;
  }
   
  }

  
}
