using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler2_59.Pro_3
{
    class In_Post_fix
    {
        Stack<char> mystack = new Stack<char>();

        public int priority(char ch)
        {
            int temp = 0;
            if (ch == '*' || ch == '/')
            {
                temp = 1;
            }
            else if (ch == '+' || ch == '-')
            {
                temp = 2;
            }
            else if (ch == '|' || ch == '&')
            {
                temp = 3;
            }
            return temp;
        }

        public void In_Postfix()
        {
            string infix = "a|b-c/d+f*j"; // a/b*c|d"; // "a*b+c" "a+b*c" "a+b-c" "a-b+c"  "a*b/c" "a/b*c" "a*b+c/d" "a/b+c*d" "a+b*c-d" "a+b-c/d+f*j" "a|b-c/d+f*j"

            string postfix = "";

            for (int i = 0; i < infix.Length; i++)
            {
                if(infix[i] == '|' || infix[i]=='&' || infix[i] == '+' || infix[i] == '-' || infix[i] == '*' || infix[i] == '/' )
                {
                    while(mystack.Count != 0 && priority(mystack.Peek() ) <= priority(infix[i]) )
                    {
                        
                        postfix = postfix + mystack.Peek(); 
                        mystack.Pop();
                    }

                    mystack.Push(infix[i]);
                }
                else
                {
                    postfix = postfix + infix[i];
                }
            } // end loop for 

            while(mystack.Count!=0 )
            {
                postfix = postfix + mystack.Pop();
            }
            Console.WriteLine(postfix);
        }
        
    }

}

/*
 public void In_Post()
        {
            string infix = "";
            string postfix = "";
            string tempst = "";
            string sc = "a|b-c/d+f*j";// "a/b*c|d"; // "a*b+c" "a+b*c" "a+b-c" "a-b+c"  "a*b/c" "a/b*c" "a*b+c/d" "a/b+c*d" "a+b*c-d" "a+b-c/d+f*j" "a|b-c/d+f*j"
            int itemp = 0;
            bool check = false;

            bool check2 = true;
            for (int i = 0; i < sc.Length; i++)
            {
                infix = infix + sc[i];
                switch (sc[i])
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '|':
                    case '&':
                        if (mystack.Count != 0)
                        {
                            if (priority(sc[i]) < priority(mystack.Peek()))
                            {
                                tempst = sc[i].ToString();
                                check = true;
                            }
                            else if (priority(sc[i]) == priority(sc[itemp]))
                            {
                                postfix = postfix + mystack.Pop();
                                mystack.Push(sc[i]);
                                itemp = i;
                                check = false;
                            }
                            else
                            {
                                postfix = postfix + mystack.Pop();
                                mystack.Push(sc[i]);
                                itemp = i;
                                check = false;
                            }
                        }
                        else
                        {
                            mystack.Push(sc[i]);
                            itemp = i;
                        }
                        break;
                    default:
                        postfix = postfix + sc[i];
                        if (check)
                        {
                            postfix = postfix + tempst;
                            check = false;
                        }
                        break;
                }
            } //end loop for 

            if (check)
            {
                postfix = postfix + tempst;
                postfix = postfix + mystack.Pop();
            }
            else
            {
                postfix = postfix + mystack.Pop();
            }
            Console.WriteLine("infix : " + infix);
            Console.WriteLine("postfix: " + postfix);

        }
        */


