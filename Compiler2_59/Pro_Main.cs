using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compiler2_59.Pro_3; //*


namespace Compiler2_59
{
    class Pro_Main
    {
        static void Main(string[] args)
       {
            
            System.IO.StreamReader s = new System.IO.StreamReader("C:/Users/CHAMPHAHA/Documents/Visual Studio 2010/Projects/Compiler2_59/Compiler2_59/Pro_3/Test.txt");
            string sc = s.ReadToEnd();
            Lexical lex = new Lexical(sc+'$');
            lex.showIntput();
            lex.Word();
            lex.showLexical();

            
        }
    }
}
