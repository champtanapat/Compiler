using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler2_59.Pro_3
{
    class Token
    {
        public string type;
        public string wordToken;

        public Token(string Type,string wordToken)
        {
            this.type = Type;
            this.wordToken = wordToken; 
        }

        public void setType(string type)
        {
            this.type = type ;
        }

        public string getType()
        {
            return type;        
        }

        public void setWord(string wordToken)
        {
            this.wordToken = wordToken;
        }

        public string getWord()
        {
            return wordToken;
        }
    }
}
