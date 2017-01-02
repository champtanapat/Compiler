using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler2_59.Pro_3
{
    class Parser
    {
        private List<Token> list;
        private Token current;
        private string word ;
        private int index = 0;
        public Parser(List<Token> list)
        {
            this.list = list;
        }

        public Token getToken()
        {
            return list[index++];
        }

        
        public void S()
        {
            //word.getWord();
            current = getToken();
            if (current.wordToken == "Program")
            {
                Console.WriteLine("" + current.getWord());

                current = getToken();
                if (current.wordToken == ";")
                {
                    Console.WriteLine("" + current.getWord());
                    
                    T();
                    current = getToken();
                    F();
                    
                }
                else
                {
                    Console.WriteLine("Syntax error at Non Terminal S(): " + current.getWord());
                }

            }
            else
            {
                Console.WriteLine("Syntax error at Non Terminal S(): "+current.getWord());
            }
        }

        private void T()
        {
            current = getToken();
            A();
            current = getToken();
            ID();
            current = getToken();
            B();
            current = getToken();
            if (current.wordToken == ";")
            {
                T();
                //--EM
            }
        }
        
        private void A()
        {
            if (current.getWord() == "int" | current.getWord() == "float" | current.getWord() == "string" | current.getWord() == "bool")
            {
                Console.WriteLine("" + current.getWord());
            }
        }

        private void B()
        {
            ID();
            B();
            //--EM
        }

        private void F()
        {
            if(current.getWord() == "Func")
            {
                ID();
                if(current.getWord() == ";")
                {

                    if(current.getWord()=="begin")
                    {
                        T();
                        C();
                        if(current.getWord()=="end")
                        {
                            if (current.getWord() ==";")
                            {
                                F();
                                //--EM
                            }

                        }
                    }
                }

            }
        }

        private void C()
        {
            if(current.getWord()=="call" || current.getWord() == "int_const" || current.getWord() == "while" || current.getWord() =="end") 
            {
                ID();
                if(current.getWord() == "(")
                {
                    if(current.getWord()==")")
                    {
                        if (current.getWord() == ";")
                        {
                            C();
                        }
                    }
                }

            }
        }

        private void ID()
        {
            if(current.type == "int_const" || current.type == "float_const" || current.type == "string_const" )
            {
                Console.WriteLine("" + current.getWord() ); 
            }
        }
    }
        
}
