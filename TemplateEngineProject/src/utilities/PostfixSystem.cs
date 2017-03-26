using System;
using System.Collections.Generic;
using System.Text;
using TemplateEngineProject.exceptions;

namespace TemplateEngineProject.utilities
{
    class PostfixSystem
    {
        private static uint GetPriority(string operatorName)
        {
            switch (operatorName)
            {
                case "||":
                    return 1;
                case "&&":
                    return 1;
                case "==":
                    return 2;
                case "!=":
                    return 2;
                case ">":
                    return 2;
                case ">=":
                    return 2;
                case "<":
                    return 2;
                case "<=":
                    return 2;
                case "!":
                    return 3;
                default:
                    throw new ParserException("[PostfixSystem]Improper operator");
            }
        }

        private static bool IsFunction(string functionName)
        {
            switch (functionName)
            {
                case "!":
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsOperator(string operatorName)
        {
            switch (operatorName)
            {
                case "&&":
                    return true;
                case "||":
                    return true;
                case "!=":
                    return true;
                case "==":
                    return true;
                case "<":
                    return true;
                case "<=":
                    return true;
                case ">":
                    return true;
                case ">=":
                    return true;
                default:
                    return false;
            }
        }

        private static string ReadFunctional(string expression, ref int i)
        {
            StringBuilder functionName = new StringBuilder();
            for (; i < expression.Length; ++i)
            {
                char ch = expression[i];
                if (Char.IsWhiteSpace(ch) || Char.IsLetter(ch) || Char.IsDigit(ch) || ch == '\"'
                    || ch == '$' || ch == '(' || ch == ')')
                {
                    --i;
                    break;
                }
                functionName.Append(expression[i]);
            }

            return functionName.ToString();
        }

        private static string ReadVariable(string expression, ref int i)
        {
            StringBuilder variableName = new StringBuilder();
            variableName.Append('$');

            for (++i; i < expression.Length; ++i)
            {
                char ch = expression[i];
                if (!(Char.IsLetter(ch) || Char.IsDigit(ch)) && ch != '.')
                {
                    --i;
                    break;
                }
                variableName.Append(expression[i]);
            }

            if (variableName.Length <= 1) throw new ParserException("[PostfixSystem]Invalid variable");

            return variableName.ToString();
        }

        private static string ReadConstant(string expression, ref int i)
        {
            StringBuilder constant = new StringBuilder();
            if (expression[i] == '\"')
            {
                for (++i; i < expression.Length; ++i)
                {
                    char ch = expression[i];
                    if (i == 0 || (ch == '\"' && expression[i - 1] != '\\'))
                        break;

                    constant.Append(ch);
                }
            }
            else
            {
                for (; i < expression.Length; ++i)
                {
                    char ch = expression[i];

                    if (Char.IsDigit(ch) || ch == '.')
                        constant.Append(ch);
                    else
                    {
                        i--;
                        break;
                    }
                }
            }
            return constant.ToString();
        }


        private static void ProcessOpenBracket(Stack<string> stack, List<String> result)
        {
            try
            {
                while (stack.Peek() != "(")
                    result.Add(stack.Pop());

                stack.Pop();

                if (IsFunction(stack.Peek())) result.Add(stack.Pop());
            }
            catch (InvalidOperationException)
            {
                throw new ParserException("[PostfixSystem]Improper epression");
            }
        }

        private static void ProcessOperator(string operatorName, Stack<string> stack, List<String> result)
        {
            if (stack.Count == 0) stack.Push(operatorName);
            else
            {
                while (stack.Count > 0 && (IsOperator(stack.Peek()) || IsFunction(stack.Peek())) &&
                       GetPriority(operatorName) <= GetPriority(stack.Peek()))
                    result.Add(stack.Pop());

                stack.Push(operatorName);
            }
        }


        public static List<String> ToPostfixSystem(string expression)
        {
            List<String> result = new List<String>();
            Stack<string> stack = new Stack<string>();

            for (int i = 0; i < expression.Length; ++i)
            {
                char symbol = expression[i];
                if (symbol == '$')
                    result.Add(ReadVariable(expression, ref i));
                else if (symbol == '(')
                    stack.Push(symbol + "");
                else if (symbol == ')')
                    ProcessOpenBracket(stack, result);
                else if (Char.IsDigit(symbol) || symbol == '\"' || symbol == '-')
                    result.Add(ReadConstant(expression, ref i));
                else if (!Char.IsWhiteSpace(symbol) && !Char.IsLetter(symbol))
                {
                    string functionalElement = ReadFunctional(expression, ref i);

                    if (IsFunction(functionalElement))
                        stack.Push(functionalElement);
                    else if (IsOperator(functionalElement))
                        ProcessOperator(functionalElement, stack, result);
                }
                else if (Char.IsLetter(symbol)) throw new ParserException("[PostfixSystem]Improper epression format");
            }
            while (stack.Count != 0)
                result.Add(stack.Pop());

            return result;
        }

        private static bool EvaluateOperator(string operatorName, Object par1, Object par2)
        {
            switch (operatorName)
            {
                case "==":
                    return par1.Equals(par2);
                case "!=":
                    return !par1.Equals(par2);
                case "&&":
                    return (Boolean) par1 && (Boolean) par2;
                case "||":
                    return (Boolean) par1 || (Boolean) par2;
                case "<":
                    return Double.Parse(par1.ToString()) < Double.Parse(par2.ToString());
                case "<=":
                    return Double.Parse(par1.ToString()) <= Double.Parse(par2.ToString());
                case ">":
                    return Double.Parse(par1.ToString()) > Double.Parse(par2.ToString());
                case ">=":
                    return Double.Parse(par1.ToString()) >= Double.Parse(par2.ToString());
                default:
                    throw new Exception("[PostfixSystem]No such operator");
            }
        }

        private static bool EvaluateFunction(string functionName, Object par)
        {
            switch (functionName)
            {
                case "!":
                    return !(Boolean) par;
                default:
                    throw new Exception("[PostfixSystem]No such function");
            }
        }

        public static bool EvaluatePostfix(List<string> expression, Func<string, object> mapper)
        {
            bool result;
            Stack<Object> stack = new Stack<object>();

            try
            {
                foreach (string el in expression)
                {
                    if (IsOperator(el))
                    {
                        object s = stack.Pop();
                        object f = stack.Pop();
                        stack.Push(EvaluateOperator(el, f, s));
                    }
                    else if (IsFunction(el))
                        stack.Push(EvaluateFunction(el, stack.Pop()));
                    else if (el.StartsWith("$"))
                        stack.Push(mapper(el.Substring(1)));
                    else stack.Push(el);
                }

                result = (Boolean) stack.Pop();
            }
            catch (InvalidCastException)
            {
                throw new Exception("[PostfixSystem]Invalid variable types");
            }

            return result;
        }
    }
}