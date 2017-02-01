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
            
            Lexical lex = new Lexical(sc + '$');
            lex.showIntput();
            lex.Word();
            lex.showLexical();
            Parser parser = new Parser(lex.list);
            parser.S();

            In_Post_fix test= new In_Post_fix();
            test.In_Postfix(parser.myAL);
            test.postfix_Tree();


        }
        
    }
}
