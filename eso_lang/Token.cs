using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace eso_lang
{
    public class Token
    {
        int id;
        string name;
        Regex tokenRE;

        public Token(int id, string name, string restr)
        {
            this.id = id;
            this.name = name;
            this.tokenRE = new Regex(restr);
        }

        public Token(int id, string name, Regex tok_re)
        {
            this.id = id;
            this.name = name;
            this.tokenRE = tok_re;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Regex RE { get; set; }

        public bool match(string lexeme) {
        MatchCollection matches =this.tokenRE.Matches(lexeme);
        List<string> match_list = new List<string>();
        foreach(Match match in matches)
        {
         match_list.Add(match.Value);
        }
        if(match_list.Count > 1){
            Console.WriteLine("more than on match for regex {0}" , this.tokenRE);
            return false;
        }else if(match_list.Count == 0){
            return false;
        }else{
            return true;
        }
            
        }

    }
}
