// Author:  Luke A
// Purpouse: This file contains the CodeGeneration code
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Microsoft.VisualBasic;
using System.Linq;

/*
 issues :
doesnt generate test string 18 or 19 correctly. 

 */
namespace Eso_Lang
{
    public class Pascal2C
    {
        List<Token> Tokens;
        List<(string, int)> programVars;
        string fname;
        
        bool prependSTDIO = false;


        public Pascal2C(List<Token> tokes)
        {
            this.Tokens = tokes;
            programVars = new List<(string, int)>();

        }
        //pass the tokens if( .. here ..)

        //unused
        public string generate() {
            bool done = false;
            
        if (Tokens[0].id == (int)TOKENSPASCAL.T_PROGRAM)
        {
             this.fname = Tokens[1].stringval;
                Console.WriteLine("filename "+ Tokens[1].stringval + " is " + Tokens[1].name);
        }
            int currentToken = 0;
            StringBuilder cstring = new StringBuilder();
            bool begin_flag = false;
            List<Token> piece = new List<Token>();
            int numBlocks = 0;
            List<Token[]> statment_blocks = new List<Token[]>();
            while (Tokens[currentToken].id != (int)TOKENSPASCAL.T_PERIOD) {
                if (Tokens[currentToken].id == (int)TOKENSPASCAL.T_BEGIN) {
                    begin_flag = true;
                }
                if (begin_flag) {
                    if(Tokens[currentToken].id == (int)TOKENSPASCAL.T_END)
                    {
                        begin_flag = false;
                        piece.Add(Tokens[currentToken]);
                        statment_blocks.Add(piece.ToArray());
                        numBlocks++;
                        piece.Clear();
                    }
                    piece.Add(Tokens[currentToken]);
                }
               // if (Tokens[currentToken].id != (int)TOKENSPASCAL.T_WRITELINE) {
                 //   this.includeSTDIO = true;
                //}
                Console.WriteLine("adding okten" + Tokens[currentToken].name);
                if(Tokens[currentToken].id == (int)TOKENSPASCAL.T_STRING)
                {
                    Console.WriteLine("adding okten strval" + Tokens[currentToken].stringval);
                }
                currentToken++;
            }
            foreach (Token t in piece) {
                Console.WriteLine(t.name);
            }

           // if (includeSTDIO) {
            //    cstring.Append("#include \"stdio.h\"\n");
            //}
            Console.WriteLine(statment_blocks[0].Length);
            //will need to change, if (output, input) are used in the pascal program, main may return
            //a value to use cmd-line args respecticly.
            cstring.Append("void main()");
           
           // cstring.Append(genBlock(statment_blocks.ToArray()));
           
            return cstring.ToString(); 
        }
       

        //pass the tokens begin .. here .. end 
        string genBlock(Token[] tokes)
        {

            Console.WriteLine("genBlock() recived " + tokes[0].name);
            Console.WriteLine("BlockCheck");
            foreach (Token t in tokes) { Console.Write(" " + t.name + " "); }



            List<Token> piece = new List<Token>();
            StringBuilder cstring = new StringBuilder("");
            List<Token> tokestmp = tokes.ToList();
            var idx = 0;
            if (tokes[0].id == (int)TOKENSPASCAL.T_BEGIN) { 
                //idx = 1;
                tokestmp = tokes.ToList();
                tokes = tokestmp.Skip(1).ToArray();
            }

                switch(tokes[idx].id)
                {
                    case (int)TOKENSPASCAL.T_IF:
                        cstring.Append(genIf(tokes));
                        break;
                    case (int)TOKENSPASCAL.T_WHILE:
                        cstring.Append(genWhile(tokes));
                        break;
                    case (int)TOKENSPASCAL.T_WRITELINE:
                        cstring.Append(genWriteLine(tokes, programVars));
                        break;
                    case (int)TOKENSPASCAL.T_ELSE:
                         tokestmp = tokes.ToList();
                         tokes = tokestmp.Skip(1).ToArray();
                         cstring.Append(genBlock(tokes));
                        break;
                    case (int)TOKENSPASCAL.T_DO:
                        tokestmp = tokes.ToList();
                        tokes = tokestmp.Skip(2).ToArray();
                        cstring.Append(genBlock(tokes));
                        break;
                case (int)TOKENSPASCAL.T_IDENT:
                    if (tokes[idx + 1].id == (int)TOKENSPASCAL.T_ASSIGN) {
                         cstring.Append(genAssign(tokes));
                    }

                    break;
              


            }
            
            //cstring.Append("}");

            /*
             bool flag = false;
             int currentToken = 0;
             int i = 0;
             foreach (Token t in tokes ) {
                 Console.WriteLine("BLOCK " + t.name);
             }
             while (currentToken < tokes.Length)
             {
                 switch (tokes[currentToken].id)
                 {
                     case (int)TOKENSPASCAL.T_ASSIGN:
                         cstring.Append(tokes[i].intval);
                         break;
                     case (int)TOKENSPASCAL.T_SCOLON:
                         cstring.Append(";");
                         break;
                     case (int)TOKENSPASCAL.T_WRITELINE:
                         this.includeSTDIO = true;
                         piece.Add(Tokens[currentToken]);
                         while (tokes[currentToken].id != (int)TOKENSPASCAL.T_RPAR)
                         {

                             Console.WriteLine("genBlock() saw a "+ Tokens[currentToken].name+ " while parsing writeln");
                             if (Tokens[currentToken].id == (int)TOKENSPASCAL.T_APOSTROPHE)
                             {
                                 flag = true;
                                // currentToken++;
                             }
                             if (flag)
                             {
                                 if (Tokens[currentToken].id == (int)TOKENSPASCAL.T_APOSTROPHE)
                                 {
                                     flag = false;
                                     piece.Clear();
                                 }
                                 Console.WriteLine("added token" + Tokens[currentToken]);
                                 piece.Add(Tokens[currentToken]);
                             }

                             Console.WriteLine("added token" + Tokens[currentToken].name);


                             piece.Add(tokes[currentToken]);
                             currentToken++;
                         }
                         Console.WriteLine(piece.ToArray().Length);
                         cstring.Append(genWriteLine(piece.ToArray(), programVars));
                         piece.Clear();
                         break;

                     case (int)TOKENSPASCAL.T_IF:
                         while (Tokens[currentToken].id != (int)TOKENSPASCAL.T_RPAR)
                         {
                             piece.Add(Tokens[currentToken]);
                             currentToken++;
                         }
                         cstring.Append(genIf(piece.ToArray()));
                         piece.Clear();

                         break;
                     case (int)TOKENSPASCAL.T_THEN:
                         while (Tokens[currentToken].id != (int)TOKENSPASCAL.T_ELSE)
                         {
                             piece.Add(Tokens[currentToken]);
                             currentToken++;
                         }
                         currentToken--;
                         cstring.Append(genBlock(piece.ToArray()));
                             break;
                     case (int)TOKENSPASCAL.T_ELSE:
                         while (Tokens[currentToken].id != (int)TOKENSPASCAL.T_END || Tokens[currentToken].id != (int)TOKENSPASCAL.T_SCOLON)
                         {
                             piece.Add(Tokens[currentToken]);
                             currentToken++;
                         }
                         currentToken--;
                         cstring.Append(genBlock(piece.ToArray()));

                         break;

                     default:
                         break;
                 }
                 //i++;
                 if (currentToken < tokes.Length)
                 {
                     currentToken++;
                 }
             }
             cstring.Append("}");
            */
            return cstring.ToString();
             
        }

        string genWriteLine(Token[] tokes, List<(string, int)> vars)
        {
            /*
            foreach (Token t in tokes) {
                Console.WriteLine("genWriteLine() " + t.name);
            }
            Console.WriteLine("genWriteLine() " +tokes.Length);
            StringBuilder cstring = new StringBuilder("printf(");
            foreach (Token t in tokes)
            {
                switch (t.id)
                {
                    case (int)TOKENSPASCAL.T_STRING:
                        cstring.Append('\"');
                        cstring.Append(t.stringval);
                        cstring.Append('\"');
                        break;
                    case (int)TOKENSPASCAL.T_NR:
                        cstring.Append(t.intval);
                        break;
                    case (int)TOKENSPASCAL.T_PLUS:
                        cstring.Append("+");
                        break;
                    default:
                        break;
                }
            }
            cstring.Append(");");
            return cstring.ToString();
            */
            Console.WriteLine("Writeline check");
            foreach (Token t in tokes) { Console.Write(" " + t.name + " "); }

            StringBuilder ret_str = new StringBuilder();
            StringBuilder formatStr = new StringBuilder();
            List<string> printfVars = new List<string>();
            prependSTDIO = true;
            for (int i = 0; i < tokes.Length; i++)
            {
                if (tokes[i].id == (int)TOKENSPASCAL.T_IDENT)
                {
                    Console.WriteLine("writing an indent in a writln");
                    //vars contains the name of the token and its type for the var block
                    foreach ((string, int) t in vars)
                    {
                        Console.WriteLine(t.Item1 + " " + t.Item2 + " " + tokes[i].stringval);
                        if (t.Item1 == tokes[i].stringval)
                        {
                            Console.WriteLine(t.Item1 + " " + t.Item2 + " " + tokes[i].name);
                            switch (t.Item2)
                            { // doesnt check bool types so cant print bools
                                case (int)TOKENSPASCAL.T_INT_TYPE:
                                    //var str = "int " + t.Item1 + ";\n";

                                    formatStr.Append("%d");
                                    printfVars.Add(tokes[i].stringval.ToString());
                                    break;
                                case (int)TOKENSPASCAL.T_STRING_TYPE:
                                    formatStr.Append("%s");
                                    printfVars.Add(tokes[i].stringval.ToString());
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                if (tokes[i].id == (int)TOKENSPASCAL.T_NR)
                {
                    formatStr.Append("%d");
                    printfVars.Add(tokes[i].intval.ToString());
                }
                if (tokes[i].id == (int)TOKENSPASCAL.T_STRING)
                {
                    Console.WriteLine("APPENDED");
                    formatStr.Append("%s");
                    printfVars.Add("\"" + tokes[i].stringval.ToString() + "\"");
                }
            }

            ret_str.Append("printf(");
            formatStr.Insert(0, "\"");
            formatStr.Append("\"");
            ret_str.Append(formatStr.ToString());
            ret_str.Append(",");
            for (var j = 0; j < printfVars.Count; j++)
            {
                if (j != printfVars.Count - 1)
                {
                    ret_str.Append(printfVars[j] + ",");
                }
                else
                {
                    ret_str.Append(printfVars[j]);
                }
            }

            ret_str.Append(");\n");
            Console.WriteLine("GenWriteline() " + ret_str.ToString());
            return ret_str.ToString();
        }

        string genIf(Token[] tokes)
        {
            StringBuilder cstring = new StringBuilder("if(");
            Console.WriteLine("Ifcheck");
            foreach (Token t in tokes) { Console.Write(" " + t.name + " "); }
            int i = 0;
            List<Token> expressionToks = new List<Token>();
            List<Token> blockToks = new List<Token>();
            while (tokes[i].id != (int)TOKENSPASCAL.T_THEN) {
                expressionToks.Add(tokes[i]);
                i++;
            }
            Console.WriteLine("genIf(): expression length " + expressionToks.Count);
            foreach (Token t in expressionToks) { Console.Write(" " + t.name + " "); }
            cstring.Append(genExpression(expressionToks.ToArray()));
            if (tokes[i].id == (int)TOKENSPASCAL.T_THEN) {
                Console.WriteLine("advance past then");
                i++;
            }
            cstring.Append("){");
            while (i < tokes.Length) {
                blockToks.Add(tokes[i]);
                i++;
            }
            
            Console.WriteLine("genIf(): block");
            foreach (Token t in blockToks) { Console.Write(" " + t.name + " "); }
            cstring.Append(genBlock(blockToks.ToArray()));
            cstring.Append("}");
            return cstring.ToString();
        }

        string genWhile(Token[] tokes)
        {
            StringBuilder cstring = new StringBuilder("while(");
            Console.WriteLine("WhileCheck");
            foreach (Token t in tokes) { Console.Write(" " + t.name + " "); }
            int i = 0;
            List<Token> expressionToks = new List<Token>();
            List<Token> blockToks = new List<Token>();
            while (tokes[i].id != (int)TOKENSPASCAL.T_DO)
            {
                expressionToks.Add(tokes[i]);
                i++;
            }
            cstring.Append(genExpression(expressionToks.ToArray()));
            cstring.Append("){");
            while (i < tokes.Length)
            {
                blockToks.Add(tokes[i]);
                i++;
            }
            cstring.Append(genBlock(blockToks.ToArray()));
            cstring.Append("}");
            return cstring.ToString();
        }

        string genElse(Token[] tokes)
        {
            StringBuilder cstring = new StringBuilder("else{");
            Console.WriteLine("ElseCheck");
            foreach (Token t in tokes) { Console.Write(" " + t.name + " "); }
            int i = 1;
            List<Token> expressionToks = new List<Token>();
            List<Token> blockToks = new List<Token>();
            // while (tokes[i].id != (int)TOKENSPASCAL.T_DO)
            //{
            //    expressionToks.Add(tokes[i]);
            //    i++;
            // }
            //cstring.Append(genExpression(expressionToks.ToArray()));
            //cstring.Append("){");
            while (tokes[i].id != (int)TOKENSPASCAL.T_BEGIN) {
                i++;
            }
            while (i < tokes.Length)
            {
                blockToks.Add(tokes[i]);
                i++;
            }
            cstring.Append(genBlock(blockToks.ToArray()));
            cstring.Append('}');
            return cstring.ToString();
        }

   

        public string Write_file()
        {
            string path = Directory.GetCurrentDirectory();
            string fname_ = "\\" + Tokens[1].stringval + ".c";
            string c_source = generate();
            using (StreamWriter writer = new StreamWriter(path + fname_))
            {
                writer.WriteLine(c_source);
            }
            return (path + fname_);
        }

        //the entry point fucntion, recusivly calls the other methods do build up the c source string 
        public string genProgram()
        {
            StringBuilder cstring = new StringBuilder("");
            int i = 0;
            int varCount = 0;
            List<(string, int)> vars = new List<(string, int)>();
            while (i < Tokens.Count)
            {
                if (i == 1)
                {
                    this.fname = Tokens[1].stringval;
                }
                if (Tokens[i].id == (int)TOKENSPASCAL.T_PROGRAM) { cstring.Append("void main(){\n"); }

                if (Tokens[i].id == (int)TOKENSPASCAL.T_VAR)
                {
                    //The first begin will appear after the var definiton block
                    while (Tokens[i].id != (int)TOKENSPASCAL.T_BEGIN)
                    {
                        // int_var : interger 
                        if (Tokens[i].id == (int)TOKENSPASCAL.T_COLON)
                        {
                            //get the type for the id list thats just been iterated over
                            var type = Tokens[i + 1].id;
                            //itterate backwards from the colon to the var block 
                            for (int j = i - 1; Tokens[j].id != (int)TOKENSPASCAL.T_VAR && Tokens[j].id != (int)TOKENSPASCAL.T_COLON; j--)
                            {
                                //if theres another : redefine the typy
                                if (Tokens[j].id == (int)TOKENSPASCAL.T_COLON)
                                {
                                    type = Tokens[j + 1].id;
                                }
                                //if any tokens are identifiers add them to a global list of program vars that are strings
                                if (Tokens[j].id == (int)TOKENSPASCAL.T_IDENT)
                                {
                                    vars.Add((Tokens[j].stringval, type));
                                }
                            }
                        }
                        i++;
                    }
                    Console.WriteLine(" vars are ");
                    foreach ((string, int) item in vars) { Console.WriteLine(item.Item1); }
                    this.programVars = vars;
                    foreach ((string, int) t in vars)
                    {
                        switch (t.Item2)
                        {
                            case (int)TOKENSPASCAL.T_INT_TYPE:
                                var str = "int " + t.Item1 + ";\n";
                                cstring.Append(str);
                                break;
                            case (int)TOKENSPASCAL.T_BOOL_TYPE:
                                var str1 = "bool " + t.Item1 + ";\n";
                                cstring.Append(str1);
                                break;
                            case (int)TOKENSPASCAL.T_STRING_TYPE:
                                //check if token with strval string is assigned and set length appropriatly
                                var idx = 0;
                                var flg = false;
                                foreach (Token tok in Tokens)
                                {

                                    if (tok.stringval == t.Item1)
                                    {
                                        if (Tokens[idx + 1].id == (int)TOKENSPASCAL.T_ASSIGN)
                                        {
                                            if (Tokens[idx + 2].id == (int)TOKENSPASCAL.T_STRING)
                                            {
                                                var len = Tokens[idx + 2].stringval.Length;
                                                var str2 = "char " + t.Item1 + "[" + len + "]" + ";\n";
                                                cstring.Append(str2);
                                                flg = true;
                                            }
                                        }
                                        else
                                        {


                                            /*   if (t.Item2 == (int)TOKENSPASCAL.T_STRING_TYPE)
                                               {
                                                   var str3 = "char " + t.Item1 + "[]" + ";\n";
                                                   cstring.Append(str3);
                                               }

                                           break;
                                            */
                                        }

                                    }
                                    idx += 1;
                                }
                                if (!flg)
                                {
                                    var str3 = "char " + t.Item1 + "[]" + ";\n";
                                    cstring.Append(str3);
                                }
                                break;
                        }
                    }

                    //append main in the case where there was a var block
                    //cstring.Append("void main()");

                }
                else
                {
                    //append main
                    //cstring.Append("void main()");
                }
                // if (Tokens[i].id == (int)TOKENSPASCAL.T_BEGIN) { cstring.Append("{"); }
                //if (Tokens[i].id == (int)TOKENSPASCAL.T_END) { cstring.Append("}"); }
                // if (Tokens[i].id == (int)TOKENSPASCAL.T_SCOLON) { cstring.Append(";"); }
                // if (Tokens[i].id == (int)TOKENSPASCAL.T_LPAR) { cstring.Append("("); }
                //if (Tokens[i].id == (int)TOKENSPASCAL.T_RPAR) { cstring.Append(")"); }
                if (Tokens[i].id == (int)TOKENSPASCAL.T_WRITELINE) {
                    prependSTDIO = true;
                    
                    Console.WriteLine("saw a writeline");
                    StringBuilder formatStr= new StringBuilder();
                    List<string> printfVars = new List<string>();
                    List<Token> writelineTokens = new List<Token>();
                    while (Tokens[i].id != (int)TOKENSPASCAL.T_RPAR)
                    {
                        writelineTokens.Add(Tokens[i]);
                        //Console.WriteLine("writing an int in a writln");
                        /*
                        if (Tokens[i].id == (int)TOKENSPASCAL.T_IDENT) {
                            Console.WriteLine("writing an indent in a writln");
                            //vars contains the name of the token and its type for the var block
                            foreach ((string, int) t in vars) {
                                Console.WriteLine(t.Item1 + " " + t.Item2 + " " + Tokens[i].stringval);
                                if (t.Item1 == Tokens[i].stringval) {
                                    Console.WriteLine(t.Item1 + " " + t.Item2 + " " + Tokens[i].name);
                                    switch (t.Item2)
                                    { // doesnt check bool types so cant print bools
                                        case (int)TOKENSPASCAL.T_INT_TYPE:
                                            //var str = "int " + t.Item1 + ";\n";
                                            
                                            formatStr.Append("%d");
                                            printfVars.Add(Tokens[i].stringval.ToString());
                                            break;
                                        case (int)TOKENSPASCAL.T_STRING_TYPE:
                                            formatStr.Append("%s");
                                            printfVars.Add(Tokens[i].stringval.ToString());
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                        
                        if (Tokens[i].id == (int)TOKENSPASCAL.T_NR) {
                            formatStr.Append("%d");
                            printfVars.Add(Tokens[i].intval.ToString());
                        }
                        if (Tokens[i].id == (int)TOKENSPASCAL.T_STRING)
                        {
                            formatStr.Append("%s");
                            printfVars.Add( "\""+Tokens[i].stringval.ToString()+"\"");
                        }
                       


                        */
                        i++;
                    }
                    //add the rpar aswell
                    
                    cstring.Append(genWriteLine(writelineTokens.ToArray(), vars));
                    /*
                    cstring.Append("printf(");
                    formatStr.Insert(0, "\"");
                    formatStr.Append( "\"");
                    cstring.Append(formatStr.ToString());
                    cstring.Append(",");
                    for (var j =0; j< printfVars.Count;j++) {
                        if (j != printfVars.Count - 1)
                        {
                            cstring.Append(printfVars[j] + ",");
                        }
                        else
                        {
                            cstring.Append(printfVars[j]);
                        }
                    }
                   
                    cstring.Append(");");
                    */

                }
                if (Tokens[i].id == (int)TOKENSPASCAL.T_WHILE)
                {
                    List<Token> stmBlock = new List<Token>();
                  //  cstring.Append("while(");  

                while (Tokens[i].id != (int)TOKENSPASCAL.T_END)
                        {
                            stmBlock.Add(Tokens[i]);
                            i++;
                        }
                    
                    //cstring.Append("){\n");
                    cstring.Append(genBlock(stmBlock.ToArray()));
                    //cstring.Append("\n}");

                }

                if (Tokens[i].id == (int)TOKENSPASCAL.T_IF)
                {
                    List<Token> ifBlock = new List<Token>();
                    while (Tokens[i].id != (int)TOKENSPASCAL.T_END)
                    {
                        //parse expression; pass statements
                        ifBlock.Add(Tokens[i]);

                        i++;

                    }
                    cstring.Append(genIf(ifBlock.ToArray()));
                    
                  //  cstring.Append("){\n");
                 //   cstring.Append(genBlock(stmBlock.ToArray()));
                  //  cstring.Append("\n}");

                }
                if (Tokens[i].id == (int)TOKENSPASCAL.T_ELSE ) {
                    if (Tokens[i + 1].id == (int)TOKENSPASCAL.T_IF)
                    {
                        List<Token> ifBlock = new List<Token>();
                        while (Tokens[i].id != (int)TOKENSPASCAL.T_END)
                        {
                            //parse expression; pass statements
                            ifBlock.Add(Tokens[i]);

                            i++;

                        }
                        cstring.Append(" else ");
                        cstring.Append(genIf(ifBlock.ToArray()));
                    }
                    else
                    {
                        List<Token> ifBlock = new List<Token>();

                        while (Tokens[i].id != (int)TOKENSPASCAL.T_END)
                        {
                            //parse expression; pass statements
                            ifBlock.Add(Tokens[i]);

                            i++;

                        }
                        cstring.Append(genElse(ifBlock.ToArray()));
                    }
                    
                }
                if (Tokens[i].id == (int)TOKENSPASCAL.T_PERIOD) { cstring.Append("\n}"); }
                if (Tokens[i].id == (int)TOKENSPASCAL.T_ASSIGN) {
                    List<Token> assignBlock = new List<Token>();
                    assignBlock.Add(Tokens[i - 1]);
                    while (Tokens[i].id != (int)TOKENSPASCAL.T_SCOLON) {

                        assignBlock.Add(Tokens[i]);
                        i++;
                    } 
                 
                    cstring.Append(genAssign(assignBlock.ToArray()));
                }

                i++;
            }

            Console.WriteLine("end of genProgram() " + prependSTDIO );
            if (prependSTDIO)
            {
                cstring.Insert(0, "#include <stdio.h>\n");
            }
            return cstring.ToString();

        }
        string genExpression(Token[] tokes)
        {
            StringBuilder cstring = new StringBuilder();

            //parse expression; pass statements
            for (int i = 0; i < tokes.Length; i++)
            {
                switch (tokes[i].id)
                {
                    case (int)TOKENSPASCAL.T_NR:
                        cstring.Append(tokes[i].intval);
                        break;
                    case (int)TOKENSPASCAL.T_PLUS:
                        cstring.Append('+');
                        break;
                    case (int)TOKENSPASCAL.T_MULTIPLY:
                        cstring.Append('*');
                        break;
                    case (int)TOKENSPASCAL.T_MINUS:
                        cstring.Append("-");
                        break;
                    case (int)TOKENSPASCAL.T_LPAR:
                        cstring.Append('(');
                        break;
                    case (int)TOKENSPASCAL.T_RPAR:
                        cstring.Append(")");
                        break;
                    case (int)TOKENSPASCAL.T_IDENT:
                        cstring.Append(tokes[i].stringval);
                        break;
                    case (int)TOKENSPASCAL.T_GTHAN:
                        cstring.Append(">");
                        break;
                    case (int)TOKENSPASCAL.T_LTHAN:
                        cstring.Append("<");
                        break;
                    case (int)TOKENSPASCAL.T_GTHANEQ:
                        cstring.Append(">=");
                        break;
                    case (int)TOKENSPASCAL.T_LTHANEQ:
                        cstring.Append("<=");
                        break;
                    case (int)TOKENSPASCAL.T_EQUAL:
                        cstring.Append("==");
                        break;
                    case (int)TOKENSPASCAL.T_NOTEQUAL:
                        cstring.Append("!=");
                        break;
                    case (int)TOKENSPASCAL.T_INTDIV:
                        cstring.Append("/");
                        break;
                    case (int)TOKENSPASCAL.T_INTMOD:
                        cstring.Append("%");
                        break;

                }
            }
            return cstring.ToString();
        }

        string genAssign(Token[] tokes) {
            List<Token> tokestmp = tokes.ToList();
           
            StringBuilder res = new StringBuilder(tokes[0].stringval);
            res.Append("=");
            // res.Append(tokes[tokes.Length - 1]);
            tokes = tokestmp.Skip(2).ToArray();
            res.Append(genExpression(tokes));
            res.Append(";");
            return res.ToString();
        }
        //should accept tokens from var token to first begin token. sets a static varible 
        public string genVar(Token[] tokes) {
            return "hello";
        }

    }
}
