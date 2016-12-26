using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler2_59.Pro_3
{
    class Lexical
    {
        private string word;
        private string wordTemp = "";
        private string wordType = "";
        private int index = 0;
        private int tempCount = 0;
        private string sc;
        private string errorTemp;

        public List<Token> list = new List<Token>();


        public Lexical(string sc)
        {
            this.sc = sc;
        }

        private void PrintShow(string p, string st)
        {
            Console.WriteLine("" + p + ":=\t" + st);
        }

        public void Word()
        {
            word = sc;
            while (word[index] != '$' && index < word.Length)
            {
                Operator();
                index++;
            }
        }

        private void Operator()
        {
            switch (word[index])
            {
                case ':':
                case ';':
                case '(':
                case ')':
                case ',':
                    wordType = "SP";
                    list.Add(new Token(wordType, word[index].ToString()));
                    //PrintShow(wordType, word[index].ToString());
                    break;
                case '+':
                case '-':
                case '*':
                case '=':
                    wordType = "OP";
                    list.Add(new Token(wordType, word[index].ToString()));
                    //PrintShow(wordType, word[index].ToString());
                    break;
                case '/':
                    if (word[index + 1] == '*')
                    {
                        wordTemp = wordTemp + word[index];
                        Comment();
                    }
                    else
                    {
                        wordType = "OP";
                        list.Add(new Token(wordType, word[index].ToString()));
                        //PrintShow(wordType, word[index].ToString());
                    }
                    break;

                case '<':
                    EqualLessOrMore();
                    break;
                case '>':
                    EqualLessOrMore();
                    break;

                default:
                    if (char.IsDigit(word[index]))
                    {
                        Digit();
                    }
                    else if (char.IsLetterOrDigit(word[index]))
                    {
                        Letter();
                    }
                    wordTemp = "";
                    wordType = "";
                    break;
            }
        }

        private void EqualLessOrMore()
        {
            wordTemp = word[index].ToString();
            if (word[index + 1] == '=')
            {
                index++;
                wordTemp = wordTemp + word[index].ToString();
                wordType = "OP";
                list.Add(new Token(wordType, wordTemp));
                //PrintShow(wordType, wordTemp);
            }
            else
            {
                wordType = "OP";
                list.Add(new Token(wordType, wordTemp));
                //PrintShow(wordType, wordTemp);
            }
        }

        private void Letter()
        {
            if (char.IsLetter(word[index]))
            {
                //--Find Reservedword and ID
                while (char.IsLetterOrDigit(word[index]) || word[index] == '_')
                {
                    wordTemp = wordTemp + word[index].ToString();
                    if (Reservedword(wordTemp))
                    {
                        wordType = "Reservedword";
                    }
                    else
                    {
                        wordType = "ID";
                    }

                    index++;
                }
                //--End While
                // have Reservedword or ID 

                //--check Next Reservedword and ID  have SP( , ; : )
                if (!char.IsLetterOrDigit(word[index]))
                {

                    list.Add(new Token(wordType, wordTemp));
                    //PrintShow(wordType, wordTemp);
                    Operator();
                }
            }
        }

        private void Digit()
        {

            while (char.IsDigit(word[index]))
            {
                wordTemp = wordTemp + word[index].ToString();
                index++;
            }

            if (word[index] != '.' && tempCount == 0)
            {
                wordType = "Int";
                index--;
                list.Add(new Token(wordType, wordTemp));
                //PrintShow(wordType, wordTemp);
            }
            else if (word[index] == '.' && tempCount == 0)
            {
                wordType = "Float";
                wordTemp = wordTemp + word[index].ToString();
                index++;
                tempCount++;
                Digit();
            }
            else
            {
                tempCount--;
                index--;
                list.Add(new Token(wordType, wordTemp));
                //PrintShow(wordType, wordTemp);
            }
        }

        private bool Reservedword(string st)
        {
            switch (st)
            {
                case "Program":
                case "Func":
                case "bool":
                case "begin":
                case "call":
                case "end":
                case "while":
                case "int_const":
                case "float_const":
                case "string_const":
                case "or":
                case "and":
                    return true;
                case "int":
                case "float":
                case "string":

                    if (word[index + 1] == '_')
                    {
                        return false;
                    }
                    return true;
                default:
                    return false;

            }

        }

        private void Comment()
        {

            if (word[index] == '/' && word[index + 1] == '*')
            {
                index++;
                wordTemp = wordTemp + word[index];

                while (word[index] != '*')
                {
                    wordTemp = wordTemp + word[index];
                    index++;
                }

                if (word[index] == '*' && word[index + 1] == '/')
                {
                    index++;
                    wordTemp = wordTemp + word[index];
                    if (word[index] == '*' && word[index + 1] == '/')
                    {
                        Console.WriteLine("/*------Comment------*/");
                    }
                    else
                    {
                        Comment();
                    }

                }
                else
                {
                    index++;
                    wordTemp = wordTemp + word[index];
                    Comment();
                }

            }
            //--End if

            else if (word[index] == '*' && word[index + 1] == '/')
            {
                index++;
                wordTemp = wordTemp + word[index];
                Console.WriteLine("/*------Comment------*/");
            }
            //--End else if

            else
            {
                index++;
                wordTemp = wordTemp + word[index];
                if (word[index] == '*' && word[index + 1] == '/')
                {
                    index++;
                    wordTemp = wordTemp + word[index];
                    Console.WriteLine("/*------Comment------*/");
                }
                else
                {
                    if (word[index] == '$')
                    {
                        errorTemp = "Comment";
                        Error(errorTemp, wordTemp);

                    }
                    // *vvv*$
                    else if (word[index + 1] == '$')
                    {
                        errorTemp = "Comment";
                        Error(errorTemp, wordTemp);
                    }
                    // *vvv* $ว่างตัวสุดท้าย
                    else if (word[index + 1] == ' ')
                    {
                        while (word[index + 1] == ' ' || word[index + 1] == '\r' || word[index + 1] == '\n')
                        {
                            index++;
                            wordTemp = wordTemp + word[index];
                        }
                        if (char.IsLetterOrDigit(word[index + 1]))
                        {
                            wordTemp = wordTemp + word[index];
                            Comment();
                        }
                        else if (word[index + 1] == ';' || word[index + 1] == '=' || word[index + 1] == '+' || word[index + 1] == '-'
                            || word[index + 1] == '*' || word[index + 1] == '.' || word[index + 1] == '(' || word[index + 1] == ')'
                            || word[index + 1] == '>' || word[index + 1] == '<')
                        {
                            //if (word[index + 1])
                            wordTemp = wordTemp + word[index];
                            Comment();
                        }
                        else
                        {
                            errorTemp = "Comment";
                            Error(errorTemp, wordTemp);
                        }

                    }
                    else
                    {
                        index++;
                        wordTemp = wordTemp + word[index];
                        Comment();
                    }
                }
            }
            //--End else 
        }

        private void Error(String st, string st2)
        {
            Console.WriteLine("!!!" + st + "Error:\t" + st2);
        }

        public void showLexical()
        {
            Console.WriteLine("=====================================================");
            Console.WriteLine("----------------Word------------Type----------------");
            foreach (var item in list)
            {
                Console.WriteLine("\t\t"+item.getWord() + "\t\t" + item.getType());
            }
            Console.WriteLine("=====================================================");
        }
    }
}

// Comment 
/*
            if (word[index] == '/' && word[index+1] == '*')
            {
                index++;
                wordTemp = wordTemp + word[index];
            }

            while (word[index] != '*')
            {
                index++;
                wordTemp = wordTemp + word[index];
            }

            index++;
            if (word[index] == '*' && word[index + 1] == '/')
            {
                wordTemp = wordTemp + word[index];
                index++;
                wordTemp = wordTemp + word[index];
                Console.WriteLine("------Comment------");
            }

            else
            {
                wordTemp = wordTemp + word[index];
                index++; 
                if (word[index] == '*' && word[index + 1] == '/')
                {
                    wordTemp = wordTemp + word[index];
                    index++;
                    wordTemp = wordTemp + word[index];
                    Console.WriteLine("------Comment------");
                }
                else 
                {
                    wordTemp = wordTemp + word[index];
                    Comment();
                }

            }
            */
