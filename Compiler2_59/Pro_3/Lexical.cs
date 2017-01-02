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
        private bool checkedOP = true;
        public List<Token> list = new List<Token>();
        public int countCom = 1;

        public Lexical(string sc)
        {
            this.sc = sc;
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
                    wordType = "";
                    wordTemp = "";
                    //PrintShow(wordType, word[index].ToString());
                    break;
                case '+':
                case '-':
                case '*':
                case '=':

                    wordType = "OP";
                    list.Add(new Token(wordType, word[index].ToString()));
                    wordType = "";
                    wordTemp = "";
                    //PrintShow(wordType, word[index].ToString());
                    break;
                    
                case '.':
                    if (char.IsDigit(word[index + 1]))
                    {

                        Digit();
                        wordType = "";
                        wordTemp = "";
                    }
                    else
                    {
                        wordType = "OP";
                        list.Add(new Token(wordType, word[index].ToString()));
                        wordType = "";
                        wordTemp = "";
                    }
                    break;
                case '/':

                    if (word[index + 1] == '*' && checkedOP)
                    {

                        Comment();
                    }
                    else
                    {
                        wordType = "OP";
                        list.Add(new Token(wordType, word[index].ToString()));
                        wordType = "";
                        wordTemp = "";
                        //PrintShow(wordType, word[index].ToString());
                    }
                    break;
                case '"':
                    stringConst();
                    wordType = "";
                    wordTemp = "";
                    break;

                case '<':
                    EqualLessOrMore();
                    wordType = "";
                    wordTemp = "";
                    break;
                case '>':
                    EqualLessOrMore();
                    wordType = "";
                    wordTemp = "";
                    break;

                case ' ':
                case '\r':
                case '\n':
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
                    else if (word[index] == '.')
                    {
                        Digit();
                    }
                    else
                    {
                        Error(wordType, word[index].ToString());
                    }
                    wordTemp = "";
                    wordType = "";

                    break;
            }
        }

        private void stringConst()
        {
            int count = 0;
            bool test = true;
            wordTemp = wordTemp + word[index];
            index++;
            count++;
            while (word[index] != '"' && test ) 
            {
                wordTemp = wordTemp + word[index];
                
                if (word[index] == '$')
                {
                  
                    Error(wordType, word[index - count].ToString());
                    wordTemp = "";
                    wordType = "";
                    break;
                }

                index++;
                count++;
            }

            if (word[index] == '"' )
            {
                wordTemp = wordTemp + word[index];
                wordType = "string_const";
                list.Add(new Token(wordType, wordTemp));
                wordTemp = "";
                wordType = "";
            }
            else
            {
                index = index - count;
            }
        }

        private void Letter()
        {
            bool testRW = false;
            if (char.IsLetter(word[index]))
            {
                while (char.IsLetterOrDigit(word[index]))
                {
                    wordTemp = wordTemp + word[index];
                    testRW = Reservedword(wordTemp);

                    //--RW  int
                    if (testRW)
                    {
                        wordType = "Reservedword";
                        list.Add(new Token(wordType, wordTemp));
                        wordTemp = "";
                        wordType = "";
                    }

                    else
                    {
                        findID();
                    }

                    index++;
                }
                //-- END WHILE

                index--;
                // aOP
                if (wordType == "ID")
                {
                    list.Add(new Token(wordType, wordTemp));
                    wordTemp = "";
                    wordType = "";
                }
            }

        }

        private void findID()
        {
            if (wordType == "ID" && char.IsDigit(word[index]))
            {
                wordType = "ID";
            }
            // END FINAL

            else
            {
                // find int123 
                if (char.IsDigit(word[index]))
                {
                    wordTemp = "";
                    Digit();
                }
                // int123
                // END FINAL 

                // find ID A
                else
                {
                    wordType = "ID";
                }
                // ID A
                // END FINAL

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
                wordType = "int_const";
                index--;
                list.Add(new Token(wordType, wordTemp));
                wordTemp = "";
                wordType = "";
                //PrintShow(wordType, wordTemp);
            }
            else if (word[index] == '.' && tempCount == 0)
            {
                wordType = "float_const";
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
                wordTemp = "";
                wordType = "";
                //PrintShow(wordType, wordTemp);
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
                case "int":
                case "float":
                case "string":
                    return true;
                default:
                    return false;

            }

        }

        private void Comment()
        {
            
            wordTemp = wordTemp + word[index];
            index++;

            // have /*  find */
            if (word[index] == '*')
            {
                wordTemp = wordTemp + word[index];
                countCom++; 
                index++;
                endComment();
            }
            
            // -- /-
            else
            {
                Operator();
            }
        }

        private void endComment()
        {
            // find */
            if (word[index] == '*')
            {
                wordTemp = wordTemp + word[index];
                countCom++;
                index++;
                if (word[index] == '/')
                {
                    wordTemp = wordTemp + word[index];
                    countCom = 0;
                    Console.WriteLine("/*---------COMMENT----------*/");
                    wordTemp = "";
                    wordType = "";
                }
                // /******$ 
                else
                {
                    endComment();
                }
            }


            //  /*a$
            // /*xxxx$
            else
            {
                while (word[index] != '*' && word[index] != '$')
                {
                    wordTemp = wordTemp + word[index];
                    countCom++;
                    index++;
                }// end loop while

                if (word[index] == '*')
                {
                    endComment();
                }

                else if (word[index] == '$')
                {
                    index = index - countCom;
                    checkedOP = false;
                    countCom = 0;
                    Operator();
                }
                else
                {
                    index = index - countCom;
                    checkedOP = false;
                    countCom = 0;
                    Operator();
                }

            }// end else 

        }

        private void Error(string type, string word)
        {
            Console.WriteLine("=====================ERROR===========================");
            Console.WriteLine("!!!" + type + "Error:\t" + word);
            Console.WriteLine("==================END ERROR==========================");
            Console.WriteLine("\n");
        }

        public void showLexical()
        {
            Console.WriteLine("=====================================================");
            Console.WriteLine("----------------Word------------Type-----------------");
            foreach (var item in list)
            {
                Console.WriteLine("\t\t" + item.getWord() + "\t\t" + item.getType());
            }
            Console.WriteLine("=====================================================");
        }

        private void PrintShow(string type, string word)
        {
            Console.WriteLine("" + type + ":=\t" + word);
        }

        public void showIntput()
        {
            int i = 0;

            Console.WriteLine("=====================SHOWINPUT=======================");
            while (sc[i] != '$')
            {
                Console.Write("" + sc[i]);
                i++;
            }
            Console.Write("\n");
            Console.WriteLine("======================ENDSHOW=========================");
            Console.Write("\n");
        }
        
    }
}
    


