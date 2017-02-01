using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler2_59.Pro_3
{
    public class In_Post_fix
    {

        Stack<string> mystack = new Stack<string>();
        ArrayList arrayList1 = new ArrayList();
        ArrayList arrayPost = new ArrayList();
        private string postfix = "";
        private string infix = "";
        private int counttemp = 0;
        private string T = "";

        public int priority(string ch)
        {
            int temp = 0;
            if (ch == "*" || ch == "/")
            {
                temp = 1;
            }
            else if (ch == "+" || ch == "-")
            {
                temp = 2;
            }
            else if (ch == ">" || ch == "<" || ch == "<=" || ch == ">=")
            {
                temp = 3;
            }
            else if(ch =="=")
            {
                temp = 4;
            }
            else if (ch == "&&" || ch == "&")
            {
                temp = 5;
            }
            else if (ch == "||" || ch == "|")
            {
                temp = 6;
            }
            return temp;
        }

        public void In_Postfix(ArrayList arraylistTemp)
        {
             postfix = "";
             infix = "";
             int count_temp = 0 ;
            foreach (string i in arraylistTemp)
            {
                    count_temp++;
                    if (i == "||" || i == "&&" || i == "+" || i == "-" || i == "*" || i == "/" || i == ">" || i == "<" || i == "<=" || i == ">=" || i == "=")
                    {
                        infix = infix + i;
                        while (mystack.Count != 0 && priority(mystack.Peek().ToString()) <= priority(i))
                        {
                            postfix = postfix + mystack.Peek();
                            arrayPost.Add(mystack.Peek());
                            mystack.Pop();
                        }
                        mystack.Push(i);
                    }
                    
                    else if(i ==";" || i==")")
                    {
                        printIn_Postfix();/*
                        counttemp = 0;
                        postfix_Tree(arrayPost);
                        arrayPost.Clear();
                        mystack.Clear();
                        */
                        arrayPost.Add(i);
                        postfix = "";
                        infix   = "";
                    }
                    else
                    {
                        infix = infix + i;
                        postfix = postfix + i;
                        arrayPost.Add(i);                    
                    }
            }// end loop for  
        }

        public void printIn_Postfix()
        {
            string temp; 

            while (mystack.Count != 0)
            {
                temp = mystack.Pop();
                postfix = postfix + temp;
                arrayPost.Add(temp);
            }
            Console.WriteLine("============Infix_To_Postfix============");
            Console.WriteLine("infix    := " + infix);
            Console.WriteLine("postfix  := " + postfix);
            Console.WriteLine("========================================");
            
        }

        public void postfix_Tree()
        {
            string opcode = "";
            string R = "";
            string L = "";
            string temp = ""; 
            foreach (string i in arrayPost)
            {   
                if (i == "||" || i == "&&" || i == "+" || i == "-" || i == "*" || i == "/" || i == ">" || i == "<" || i == "<=" || i == ">=" || i == "=")
                {
                    if (i == "=")
                    {
                        R = mystack.Pop();
                        L = mystack.Pop();
                        //Console.WriteLine("=" + "\t" + L + "\t_" + "\t" +R); // +"\t"+mystack.Pop());
                        printTree("=",L,"_",R);
                    }
                    else
                    {
                        opcode = i;
                        R = mystack.Pop();
                        L = mystack.Pop();
                        temp = tempT();
                        //Console.WriteLine("" + opcode + "\t" + L + "\t" + R + "\t" + temp);
                        printTree(opcode,L,R,temp);
                        mystack.Push(temp);
                    }
                        
                    
                }
                else if(i == ";" || i == ")")
                {
                    Console.WriteLine("========================================");
                    mystack.Clear();
                    counttemp = 0;
                }
                else
                {
                    mystack.Push(i);
                }
               
            }// end loop for  
           
        }

        public string tempT()
        {
            counttemp++;
            return "T"+counttemp;
        }

        public void printTree(string op,string operand1,string operand2,string result)
        {
            Console.WriteLine("" +op+ "\t" + operand1 + "\t" +operand2+ "\t" + result);
           
        } 
            
    }
}

