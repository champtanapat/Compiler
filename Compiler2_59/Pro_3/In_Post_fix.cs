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
        ArrayList arrayPostfix = new ArrayList(); 
        ArrayList arrayInfix = new ArrayList();
        ArrayList arrayPost_Tree = new ArrayList(); 
        ArrayList arrayList1 = new ArrayList();

        private string postfix = "";
        private string s_infix = "";
        private int counttemp = 0;
        

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

        public void Postfix(ArrayList arrayListTemp)
        {
             arrayInfix = arrayListTemp;
             postfix = "";
             s_infix = "";
             int count_temp = 0 ;
             string temp = "";
            foreach (string infix in arrayInfix)
            {
                    count_temp++;
                    if (infix == "||" || infix == "&&" || infix == "+" || infix == "-" || infix == "*" || infix == "/" || infix == ">" || infix == "<" || infix == "<=" || infix == ">=" || infix == "=")
                    {
                        this.s_infix = this.s_infix + infix;
                        while (mystack.Count != 0 && priority(mystack.Peek().ToString()) <= priority(infix))
                        {
                            postfix = postfix + mystack.Peek();
                            arrayPostfix.Add(mystack.Peek()); //-- update
                            arrayPost_Tree.Add(mystack.Peek());
                            mystack.Pop();
                        }
                        mystack.Push(infix);
                        
                    }
                    
                    else if(infix ==";" || infix==")")
                    {
                    
                        while (mystack.Count != 0)
                        {
                            temp = mystack.Pop();
                            postfix = postfix + temp;
                            arrayPostfix.Add(temp);
                            arrayPost_Tree.Add(temp);

                        }
                        arrayPostfix.Add(";");
                        arrayPost_Tree.Add(";");
                        postfix = "";
                        this.s_infix   = "";
                    }
                    else
                    {
                        this.s_infix = this.s_infix + infix;
                        postfix = postfix + infix;
                        arrayPostfix.Add(infix); //-- update
                        arrayPost_Tree.Add(infix);
                }
            }// end loop for  

       
        }
        
        public void Tree()
        {
            string opcode = "";
            string R = "";
            string L = "";
            string temp = "";
            string infix = "";
            arrayList1.Clear();
            foreach (string i in arrayPost_Tree)
            {
                infix = infix + i;
                if (i == "||" || i == "&&" || i == "+" || i == "-" || i == "*" || i == "/" || i == ">" || i == "<" || i == "<=" || i == ">=" || i == "=")
                {
                    if (i == "=")
                    {
                        opcode = "=";
                        R = mystack.Pop();
                        L = mystack.Pop();
                        //Console.WriteLine("=" + "\t" + L + "\t_" + "\t" +R); // +"\t"+mystack.Pop()); //printTree("=",L,"_",R);
                        addListTree(opcode, L,"_",R);
                    }
                    else
                    {
                        opcode = i;
                        R = mystack.Pop();
                        L = mystack.Pop();
                        temp = tempT();
                        //Console.WriteLine("" + opcode + "\t" + L + "\t" + R + "\t" + temp);//printTree(opcode,L,R,temp);
                        addListTree(opcode,L,R,temp);
                        mystack.Push(temp);
                    }
                }
                else if(i == ";" || i == ")")
                {
                    arrayList1.Add(i);
                    mystack.Clear();
                    counttemp = 0;
                }
                else
                {
                    mystack.Push(i);
                }
            }// end loop for  
           
        }

        public void addListTree(string opcode,string L,string R,string temp)
        {
            arrayList1.Add(opcode);
            arrayList1.Add(L);
            arrayList1.Add(R);
            arrayList1.Add(temp);

        }

        public void printInfix()
        {
            string infix_ = "";
            Console.WriteLine("=================Infix==================");
            foreach (string infix in arrayInfix)
            {

                if (infix == ";" || infix == ")")
                {
                    Console.WriteLine("infix:=\t" + infix_);
                    infix_ = "";
                }
                else
                {
                    infix_ = infix_ + infix;
                }
            }
        }
        
        // version 2 prin ArrayList Postfix 
        public void printPostfix_()
        {
            string postfix = "";
            Console.WriteLine("=================Postfix================");
            foreach (string i in arrayPostfix)
            {
                if (i == ";" || i == ")")
                {
                    Console.WriteLine("postfix:= " + postfix);
                    postfix = "";
                }
                else
                {
                    postfix = postfix + i;
                }
            }//end loop
            Console.WriteLine("========================================");
        }
        
        //version 2  print ArrayList Postfix tree
        public void printTree_()
        {
            int count = 1;
            
            foreach (string i in arrayList1)
            {

                if (count == 5)
                {
                    if (i==";")
                    {
                        count = 1;
                        Console.WriteLine();
                        Console.WriteLine("========================================");

                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("" + i + "\t");
                        count = 1;
                        count++;
                    }
                }
                else
                {
                    Console.Write("" + i + "\t");
                    count++;
                }

            }// end loop 
            Console.WriteLine();
        }

        public string tempT()
        {
            counttemp++;
            return "T" + counttemp;
        }
    }
}

/*public void printPostfix()
        {
            string temp;

            while (mystack.Count != 0)
            {
                temp = mystack.Pop();
                postfix = postfix + temp;
                arrayPost_Tree.Add(temp);
                arrayPostfix.Add(temp);
            }
            Console.WriteLine("============Infix_To_Postfix============");
            Console.WriteLine("infix    := " + infix);
            Console.WriteLine("postfix  := " + postfix);
            Console.WriteLine("========================================");

        }
*/
 

/* version 1 print Postfix  tree
    public void printTree(string op, string operand1, string operand2, string result)
    {
        Console.WriteLine("" + op + "\t" + operand1 + "\t" + operand2 + "\t" + result);

    }
*/