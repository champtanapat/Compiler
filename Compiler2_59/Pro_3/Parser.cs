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
        private int index = 0;
        
        public Parser(List<Token> list)
        {
            this.list = list;
        }

        public Token getToken()
        {
            return list[index++];
        }

        public void printshow(string stword)
        {
            Console.WriteLine("" + stword);
        }

        private void syntax(string terminal, string word)
        {
            Console.WriteLine("Syntax error at Non Terminal \t" + terminal + ":\t" + word);
        }

        public void S()
        {
            //word.getWord();
            current = getToken();
            if (current.wordToken == "Program")
            {
                printshow(current.wordToken);
                current = getToken();
                if (current.wordToken == ";")
                {
                    printshow(current.wordToken);
                    current = getToken();
                    T();
                    //current = getToken();
                    F();
                }
                else
                {
                    syntax("S()", current.wordToken);
                }

            }
            else if (current.wordToken == "$")
            {
                
            }
            else
            {
                syntax("S()", current.wordToken);
            }

            if (current.wordToken == "$")
            {
                Console.WriteLine("Syntax True");
            }
        }

        private void T()
        {
            if (current.wordToken == "int" || current.wordToken == "float" || current.wordToken == "string" || current.wordToken == "bool")
            {
                A();
                ID();
                B();
                
                if (current.wordToken == ";")
                {
                    current = getToken();
                    T();
                }
                else
                {
                    syntax("T()", current.wordToken);
                }
            }
            else if (current.wordToken == "Func" || current.wordToken == "call" || current.type == "ID" || current.wordToken == "while"
                || current.wordToken == "end" || current.wordToken == "$")
            {
                   
            }
            else
            {
                syntax("T()", current.wordToken);

            }
        }

        private void A()
        {
            if (current.getWord() == "int" | current.getWord() == "float" | current.getWord() == "string" | current.getWord() == "bool")
            {
                printshow(current.wordToken);
                current = getToken();
            }
            else
            {
                syntax("A()", current.wordToken);
            }
        }

        private void  B()
        {
            if (current.wordToken == ",")
            {
                printshow(current.wordToken);
                current = getToken();
                ID();
                //current = getToken();
                B();
            }
            else if (current.wordToken == ";")
            {
                printshow(current.wordToken);
                //current = getToken();
            }
            else 
            {

                syntax("B()", current.wordToken);
            }
        }

        private void F()
        {
            if (current.getWord() == "Func")
            {
                printshow(current.wordToken);
                current = getToken();
                ID();
                if (current.getWord() == ";")
                {
                    printshow(current.wordToken);
                    current = getToken();
                    if (current.getWord() == "begin")
                    {
                        printshow(current.wordToken);
                        current = getToken();
                        T();
                        C();
                        if (current.getWord() == "end")
                        {
                            printshow(current.wordToken);
                            current = getToken();
                            if (current.getWord() == ";")
                            {
                                printshow(current.wordToken);
                                current = getToken();
                                F();
                            }
                            else
                            {
                                syntax("F()", current.wordToken);
                            }
                        }
                        else
                        {
                            syntax("F()", current.wordToken);
                        }
                    }
                    else
                    {
                        syntax("F()", current.wordToken);
                    }
                }
                else
                {
                    syntax("F()", current.wordToken);
                }
            }
            else if(current.wordToken == "$" )
            {

            }
            else
            {
                syntax("F()",current.wordToken);
            }
        }

        private void C()
        {
            if (current.getWord() == "call")
            {
                printshow(current.wordToken);
                current = getToken();
                ID();
                if (current.getWord() == "(")
                {
                    printshow(current.wordToken);
                    current = getToken();
                    if (current.getWord() == ")")
                    {
                        printshow(current.wordToken);
                        current = getToken();
                        if (current.getWord() == ";")
                        {
                            printshow(current.wordToken);
                            current = getToken();
                            C();
                        }
                        else
                        {
                            syntax("C()", current.wordToken);
                        }
                    }
                    else
                    {
                        syntax("C()", current.wordToken);
                    }
                }
                else
                {
                    syntax("C()", current.wordToken);
                }

            }
            else if (current.type == "ID")
            {
                D();
                C();

            }
            else if (current.wordToken == "while")
            {
                W();
                C();
            }
            else if (current.wordToken == "end")
            {
               
            }
            else
            {
                syntax("C()", current.wordToken);
            }
        }

        private void ID()
        {
            //if(current.type == "int_const" || current.type == "float_const" || current.type == "string_const" )
            if (current.type == "ID")
            {
                printshow(current.wordToken);
                current = getToken();
            }
            else
            {
                syntax("ID()", current.wordToken);
            }
        }

        private void D()
        {
            if (current.type == "ID")
            {
                printshow(current.wordToken);
                current = getToken();
                if (current.wordToken == "=")
                {
                    printshow(current.wordToken);
                    current = getToken();
                    I();
                    E();
                    if (current.wordToken == ";")
                    {
                        printshow(current.wordToken);
                        current = getToken();
                    }
                    else
                    {
                        syntax("D()", current.wordToken);
                    }
                }
                else
                {
                    syntax("D()", current.wordToken);
                }

            }
            else
            {
                syntax("D()", current.wordToken);
            }
        }

        private void E()
        {
            if (current.wordToken == "*" || current.wordToken == "/" || current.wordToken == "-" || current.wordToken == "+"
                || current.wordToken == "and" || current.wordToken == "or" || current.wordToken == ">" || current.wordToken == ">=" || current.wordToken == "<" || current.wordToken == ">")
            {
                printshow(current.wordToken);
                current = getToken();
                
                I();
                E();
            }
            else if(current.wordToken == ";" || current.wordToken == ")" )
            {

            }
            else
            {
                syntax("E()", current.wordToken);
            }

        }

        private void I()
        {
            if(current.type=="ID")
            {
                ID();
            }
            else if(current.type == "int_const" || current.type == "float_const" || current.type == "string_const")
            {
                printshow(current.wordToken);
                current = getToken();
            }
            else
            {
                syntax("I()", current.wordToken);
            }
        }

        private void W()
        {
            if (current.wordToken == "while")
            {
                printshow(current.wordToken);
                current = getToken();
                if(current.wordToken == "(")
                {
                    printshow(current.wordToken);
                    current = getToken();
                    I();
                    //current = getToken();
                    E();
                    current = getToken();
                    if (current.wordToken == "begin")
                    {
                        current = getToken();
                        C();
                        if (current.wordToken == "end")
                        {
                            printshow(current.wordToken);
                            current = getToken();
                            if(current.wordToken == ";")
                            {
                                printshow(current.wordToken);
                                current = getToken();
                            }
                            else
                            {
                                syntax("W()", current.wordToken);
                            }
                        }
                        else
                        {
                            syntax("W()", current.wordToken);
                        }
                    }
                    else
                    {
                        syntax("W()", current.wordToken);
                    }

                }
                else
                {
                    syntax("W()", current.wordToken);
                }

            }
            else
            {
                syntax("W()", current.wordToken);
            }
        }
    }
        
}
