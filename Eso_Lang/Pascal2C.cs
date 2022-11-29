using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/*
 issues :
the stdio.h is inserted even when theres no writeline
 */
namespace Eso_Lang
{
    public class Pascal2C
    {
        List<Token> Tokens;
        string fname;
        bool includeSTDIO;
        public Pascal2C(List<Token> tokes)
        {
            this.Tokens = tokes;
        }
        //pass the tokens if( .. here ..)

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
                if (Tokens[currentToken].id != (int)TOKENSPASCAL.T_WRITELINE) {
                    this.includeSTDIO = true;
                }
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

            if (includeSTDIO) {
                cstring.Append("#include \"stdio.h\"\n");
            }
            Console.WriteLine(statment_blocks[0].Length);
            //will need to change, if (output, input) are used in the pascal program, main may return
            //a value to use cmd-line args respecticly.
            cstring.Append("void main()");
           
            cstring.Append(genBlock(statment_blocks[0]));
           
            return cstring.ToString(); 
        }
       

        //pass the tokens begin .. here .. end 
        string genBlock(Token[] tokes)
        {
            List<Token> piece = new List<Token>();
            StringBuilder cstring = new StringBuilder("{");
           
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
                        cstring.Append(genWriteLine(piece.ToArray()));
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
            return cstring.ToString();
        }

        string genWriteLine(Token[] tokes)
        {
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
        }

        string genIf(Token[] tokes)
        {
            StringBuilder cstring = new StringBuilder("if(");
            foreach (Token t in tokes)
            {
                switch (t.id)
                {
                    case (int)TOKENSPASCAL.T_NR:
                        cstring.Append(t.intval);
                        break;
                    case (int)TOKENSPASCAL.T_LTHAN:
                        cstring.Append("<");
                        break;
                    case (int)TOKENSPASCAL.T_GTHAN:
                        cstring.Append(">");
                        break;
                    case (int)TOKENSPASCAL.T_LTHANEQ:
                        cstring.Append("<=");
                        break;
                    case (int)TOKENSPASCAL.T_GTHANEQ:
                        cstring.Append(">=");
                        break;
                    case (int)TOKENSPASCAL.T_EQUAL:
                        cstring.Append("==");
                        break;
                    case (int)TOKENSPASCAL.T_NOTEQUAL:
                        cstring.Append("!=");
                        break;
                    case (int)TOKENSPASCAL.T_OR:
                        cstring.Append("||");
                        break;
                    case (int)TOKENSPASCAL.T_AND:
                        cstring.Append("&&");
                        break;
                    case (int)TOKENSPASCAL.T_IDENT:
                        cstring.Append(t.stringval);
                        break;
                    default:
                        Console.WriteLine("code gen if found a token that was'nt matched " + t.name);
                        break;
                }
            }
            cstring.Append(")");
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
        
    }
}
