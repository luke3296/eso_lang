using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Eso_Lang
{
    public class Token
    {
        public int id
        { get; set; }
        public string name
        { get; set; }
        public Regex tokenRE
        { get; set; }
        public int intval
        { get; set; }
        public string stringval
        { get; set; }

        public Token(int id_, string name, string restr)
        {
            this.id = id_;
            this.name = name;
            this.tokenRE = new Regex(restr);
        }

        public Token(int id_, string name, Regex tok_re)
        {
            this.id = id_;
            this.name = name;
            this.tokenRE = tok_re;
        }
        public Token(int id_, string name, Regex tok_re, int intval_)
        {
            this.id = id_;
            this.name = name;
            this.tokenRE = tok_re;
            this.intval = intval_;
        }
        public Token(int id_, string name, Regex tok_re, string strval)
        {
            this.id = id_;
            this.name = name;
            this.tokenRE = tok_re;
            this.stringval = strval;
        }
        public Token(int id_, string name, string restr, int intval_)
        {
            this.id = id_;
            this.name = name;
            this.tokenRE = new Regex(restr);
            this.intval = intval_;
        }
        public Token(int id_, string name, string restr, string strval)
        {
            this.id = id_;
            this.name = name;
            this.tokenRE = new Regex(restr);
            this.stringval = strval;
        }

        public bool match(string lexeme)
        {
            MatchCollection matches = this.tokenRE.Matches(lexeme);
            List<string> match_list = new List<string>();
            foreach (Match match in matches)
            {
                match_list.Add(match.Value);
            }
            if (match_list.Count > 1)
            {
                Console.WriteLine("more than on match for regex {0}", this.tokenRE);
                return false;
            }
            else if (match_list.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public string match_str(string lexeme)
        {
            MatchCollection matches = this.tokenRE.Matches(lexeme);
            List<string> match_list = new List<string>();
            foreach (Match match in matches)
            {
                match_list.Add(match.Value);
            }
            if (match_list.Count > 1)
            {
                Console.WriteLine("more than on match for regex {0}", this.tokenRE);
                return "";
            }
            else if (match_list.Count == 0)
            {
                return "";
            }
            else
            {
                return match_list[0];
            }
        }

    }

}
