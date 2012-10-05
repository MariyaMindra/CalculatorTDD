using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace StringCalculatorTDD
{

    public class Value
    {
    }

    public class OperatorValue: Value
    {
        public OperatorValue()
        {
        }

        public OperatorValue(char value)
        {
            Value = value;
        }

        public char Value { get; set; } // priority 1: +, - ; priority 2: * , / ; priority 3: ^
        public int Priority
        {
            get {
                return GetPriority(Value);
            }
        }

        private static int GetPriority(char value)
        {
            switch (value)
            {
                    //case '(':
                    //case ')':
                    //    return 1;
                case '-':
                case '+':
                    return 2;
                case '*':
                case '/':
                    return 3;
                case '^':
                    return 4;
            }
            return 0;
        }

        public static bool IsOperator(char value)
        {
            if (GetPriority(value) > 0) return true;
            return false;
        }


        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class NumericValue: Value
    {
        public NumericValue()
        {
        }

        public NumericValue(double value)
        {
            Value = value;
        }

        public double Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

    }
    

    

public class Calculator
    {
        //List<Value> _output = new List<Value>();
         //private static List<OperatorValue> standartOperators =
         // new List<OperatorValue>()
         //     {
         //         new OperatorValue('+'), 
         //         new OperatorValue('-'), 
         //         new OperatorValue('*'), 
         //         new OperatorValue('/'), 
         //         new OperatorValue('^')
         //     };
    private static List<string> standartOperators =
     new List<string>(new string[] { "+", "-", "*", "/", "^" });
    public static List<Value> ReversePolishNotashion(string input)
    {
        Stack<OperatorValue> stack = new Stack<OperatorValue>();
        List<Value> output = new List<Value>();
        int countInt=0;
        for (int i=0;i<input.Length; i++)
        {
            if (OperatorValue.IsOperator(input[i]))
            {
                AddOperatorToStack(stack,output,new OperatorValue(input[i]));
                countInt = 0;
            } else
                if (Char.IsNumber(input[i])&& countInt==0)
                {
                    int value = Convert.ToInt32(input[i].ToString());
                    output.Add(new NumericValue(value));
                    countInt++;
                }
                else if (Char.IsNumber(input[i])&& countInt!=0)
                {
                    var number=output.Last();
                    int j = output.Count() - 1;
                    int value = Convert.ToInt32(number.ToString()) * 10 + Convert.ToInt32(input[i].ToString());
                    output[j] = new NumericValue(value);
                    countInt++;
                }
            else if (input[i].ToString()=="(")
            {
                stack.Push(new OperatorValue(input[i]));
            }
                else if (input[i].ToString()==")")
                {
                    while (stack.Peek().ToString()!="(")
                    {
                        output.Add(stack.Pop());
                    }
                    stack.Pop();
                }
            //if (Char.IsNumber(input[i]))
            //{
            //    int value = Convert.ToInt32(input[i].ToString());
            //    output.Add(new NumericValue(value));
            //}
            /*Если символ является открывающей скобкой, помещаем его в стек.
            Если символ является закрывающей скобкой:
            До тех пор, пока верхним элементом стека не станет открывающая скобка, выталкиваем элементы из стека в выходную строку. 
             * При этом открывающая скобка удаляется из стека, но в выходную строку не добавляется. Если после этого шага на вершине стека оказывается символ функции, выталкиваем его в выходную строку. 
             * Если стек закончился раньше, чем мы встретили открывающую скобку, это означает, что в выражении либо неверно поставлен разделитель, либо не согласованы скобки.*/
        }

        PushRemainingOperatorsToOutput(stack,output);

        return output;
    }

    private static void PushRemainingOperatorsToOutput(Stack<OperatorValue> stack, List<Value> output)
    {
        while (stack.Count > 0)
        {
            output.Add(stack.Pop());
        }
    }

    private static void AddOperatorToStack(Stack<OperatorValue> stack, List<Value> output, OperatorValue operatorValue)
    {
        /*
         * 1) пока…
            … (если оператор o1 ассоциированный, либо лево-ассоциированный) приоритет o1 меньше либо равен приоритету оператора, находящегося на вершине стека…
            … (если оператор o1 право-ассоциированый) приоритет o1 меньше приоритета оператора, находящегося на вершине стека…  ----
            … выталкиваем верхние элементы стека в выходную строку;
           2) помещаем оператор o1 в стек.
         */

        if (stack.Count>0)
        {
            var topOperator = stack.Peek();

            while (stack.Count > 0 && operatorValue.Priority <= topOperator.Priority)
            {
                output.Add(stack.Pop());
                //topOperator = stack.Peek();
            }
        }

        stack.Push(operatorValue);
    }

    public static double Calculate (string query)
        {
            if (string.IsNullOrEmpty(query)) return 0;
            
            //var numbers=query.Split('+');
            //var result = numbers.Where(n => n != "").Select(n => Convert.ToDouble(n)).Sum();
            Stack<Value> result=new Stack<Value>();
            Queue<Value> queue=new Queue<Value>(ReversePolishNotashion(query));
           
            while (queue.Count>0)
            {
            var _value = queue.Dequeue(); 
                if (!standartOperators.Contains(_value.ToString()))
                {
                    result.Push(_value);
                    //_value = queue.Dequeue();
                }
                else
                {
                    double _res = 0;
                    switch(_value.ToString())
                    {
                        case "+":
                            {
                                var op1 = Convert.ToDouble(result.Pop().ToString());
                                var op2 = Convert.ToDouble(result.Pop().ToString());
                                _res = op1 + op2;
                                break;
                            }
                        case "-":
                            {
                                var op1 = Convert.ToDouble(result.Pop().ToString());
                                var op2 = Convert.ToDouble(result.Pop().ToString());
                                _res = op2 - op1;
                                break;
                            }
                        case "*":
                            {
                                var op1 = Convert.ToDouble(result.Pop().ToString());
                                var op2 = Convert.ToDouble(result.Pop().ToString());
                                _res = op1 * op2;
                                break;
                            }
                        case "/":
                            {
                                var op1 = Convert.ToDouble(result.Pop().ToString());
                                var op2 = Convert.ToDouble(result.Pop().ToString());
                                _res = op2/op1;
                                break;
                            }
                        case "^":
                            {
                                var op1 = Convert.ToDouble(result.Pop().ToString());
                                var op2 = Convert.ToDouble(result.Pop().ToString());
                                _res = Math.Pow(op2, op1);
                                break;
                            }

                    }
                    result.Push(new NumericValue(_res));
                }
            }
            return Convert.ToDouble(result.Pop().ToString());
        }

    }
   
}
