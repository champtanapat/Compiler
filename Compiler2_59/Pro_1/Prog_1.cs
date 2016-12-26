using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler2_59
{
    class Prog_1
    {   
        public static char c;
        public static string word = "<=$";
        public static string word2;
        public static int index = 0;
        

        public char GetChar()
        {
            c = word[index];
            index++;
            return c;
        }

        public void UnGetChar()
        {
            index--;
        }

        public void DFA_2_12()
        {
                c = GetChar();
                if (c != '$')
                {
                    Action("S0",c);
                    switch (c)
                    {
                        case '<':
                            c = GetChar();
                            if (c == '=')
                            {
                               // c = GetChar();
                                Action("S2",c);
                            }
                            else
                            {
                                Action("S1",c);
                                UnGetChar();
                            }
                            break;
                        case '>':
                            Action("S3",c);
                            break;

                        default:
                            if (!char.IsLetter(c))
                            {
                                Console.WriteLine("Eject");
                            }
                            while(char.IsLetter(c))
                            {
                                Action("S4", c);
                                c = GetChar();

                            }
                                Action("S4_Final", c);
                            UnGetChar();
                            break;
                    }
                }
                
        }

        public void Action(string state,char ch)
        {
            switch (state)
            {
                case "S0": word2 = ch.ToString();
                           break;
                case "S1": Console.WriteLine(word2+"\t:= word");
                           break;
                case "S2": word2 = word2 + ch.ToString();
                           Console.WriteLine(word2+"\t:= word");
                           break;
                case "S3": Console.WriteLine(word2+"\t:= word");
                           break;
                case "S4": word2 = word2 + ch.ToString();
                           Console.WriteLine(word2+"\t:= word");
                          break;
                case "S4_Final":
                          Console.WriteLine(word2+"\t:= word");
                          break;
                default:
                    break;
 
            
            }
        
        }
    }
}
