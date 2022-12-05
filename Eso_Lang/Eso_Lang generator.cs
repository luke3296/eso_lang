namespace Eso_Lang2 {
    
    public class generator{
      List<Token> tokens = new List<Token>();
      public void lexEso(string Eso_Lang){
             List<Token> tokenRegexsEso = new List<Token>();

            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Naught\s", 0));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Boots\s", 1));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Goats\s", 2));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Powder\s", 3));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Rum\s", 4));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Meats\s", 5));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Wines\s", 6));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Cloth\s", 7));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Ropes\s", 8));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_NR, "Int", @"Food\s", 9));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_PLUS, "Addition-Operator", @"Put\swith\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_MINUS, "Subtraction-Operator", @"Take\sfrom\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_DIVIDE, "Division-Operator", @"By\scount\sof\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_MULTIPLY, "Multiplication-Operator", @"By\scount\sper\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_ASSIGN, "Asignment-Operator", @"Let\sthe\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_ASSIGN, "Asignment-Operator", @"Have\s"));
            tokenRegexsEso.Add(new Token((int)TOKENSESO.T_IDENT 
            , "Lable", @"_[a-zA-Z]*\s"));
      }

      public String Match_tokens(string str){
        Char delimiter = ' ';
        String Output_String=" ";
        String[] result = str.Split(delimiter);
        for (int i = 0; i < result.Length; i++) {
            
            if (result[i]=="Put"){
              Output_String= Output_String+ Match_numbers(result[i+1]);
               if (result[i+2]=="with"){
                  
                  Output_String= Output_String+ "+";
                 
               }
               else {Console.WriteLine("error");}
            
              Output_String= Output_String+ Match_numbers(result[i+3]);
          }
          if (result[i]=="Take"){
               Output_String= Match_numbers(result[i+1]);
               if (result[i+2]=="from"){
                  Output_String= "-" + Output_String;
               }
               else {Console.WriteLine("error");}
            
              Output_String= Match_numbers(result[i+3]) + Output_String ;
          }
         
        }
        
        return Output_String;
         
      }
      public string Match_numbers(String token){
       
         if (token=="Naught"){
             return "0";
          }
          if (token=="Boots"){
            return "1";
          }

           if (token=="Goats"){
            return "2";
          }
           if (token=="Powder"){
            return "3";
          }
           if (token=="Rum"){
            return "4";
          }
           if (token=="Meats"){
            return "5";
          }
           if (token=="Wines"){
            return "6";
          }
          if (token=="Cloth"){
            return "7";
          }
          if (token=="Ropes"){
            return "8";
          }
          if (token=="Foods"){
            return "9";
          }
        return "error";
      }

     

 static void Main(){
  String input_string = "Put Goats with Goats";
  generator generator1=new generator();
  String output_string = generator1.Match_tokens(input_string);
  String input_string1="Take Ropes from Foods";
  String output_string1 = generator1.Match_tokens( input_string1);
  Console.WriteLine(output_string);
  Console.WriteLine(output_string1);

 

}
}
}

