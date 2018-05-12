using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }
        #region variables
        private double a = 0;
        private int c = 0;
        private int count = 0;
        private string s = String.Empty;       
        private string str = String.Empty;
        private string stroka = String.Empty;
        private string[] mas;

        private Stack<string> stackNumbers = new Stack<string>();
        private Stack<string> stackOperations = new Stack<string>();
        
        CalcClass calc = new CalcClass();
        #endregion

        private void ZeroButton_Click(object sender, EventArgs e)
        {
            str += 0;
            Calculate.Text += 0;
        }
        private void OneButton_Click(object sender, EventArgs e)
        {
            str += 1;
            Calculate.Text += 1;
        }
        private void TwoButton_Click(object sender, EventArgs e)
        {
            str += 2;
            Calculate.Text += 2;
        }
        private void ThreeButton_Click(object sender, EventArgs e)
        {
            str += 3;
            Calculate.Text += 3;
        }

        private void FourButton_Click(object sender, EventArgs e)
        {
            str += 4;
            Calculate.Text += 4;
        }

        private void FiveButton_Click(object sender, EventArgs e)
        {
            str += 5;
            Calculate.Text += 5;
        }

        private void SixButton_Click(object sender, EventArgs e)
        {
            str += 6;
            Calculate.Text += 6;
        }

        private void SevenButton_Click(object sender, EventArgs e)
        {
            str += 7;
            Calculate.Text += 7;
        }

        private void EightButton_Click(object sender, EventArgs e)
        {
            str += 8;
            Calculate.Text += 8;
        }

        private void NineButton_Click(object sender, EventArgs e)
        {
            str += 9;
            Calculate.Text += 9;
        }
        private void PlusButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += PlusButton.Tag.ToString();
            str += " " + PlusButton.Tag.ToString() + " ";
            DotButton.Enabled = true;
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += MinusButton.Tag.ToString();
            str += " " + MinusButton.Tag.ToString() + " ";
            DotButton.Enabled = true;
        }

        private void MultiplyButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += MultiplyButton.Tag.ToString();
            str += " " + MultiplyButton.Tag.ToString() + " ";
            DotButton.Enabled = true;
        }

        private void DivideButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += DivideButton.Tag.ToString();
            str += " " + DivideButton.Tag.ToString() + " ";
            DotButton.Enabled = true;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Calculate.Clear();
            str = String.Empty;
            DotButton.Enabled = true;
        }

        private void EqualyButton_Click(object sender, EventArgs e)
        {
            mas = str.Split(' ');
            UsingStack(mas);
        }
        #region MyPrivateMethods
        /// <summary>
        /// Performs mathematical actions using the stack
        /// </summary>
        /// <param name="mas">all elements of expression</param>
        /// <returns>result of calculations</returns>
        private string UsingStack(string[] mas)
        {
            for (int i = 0; i < mas.Length; i++)
            {
                if (IsANumber(mas[i]))
                {
                    stackNumbers.Push(mas[i]);
                }
                else if (IsOperation(mas[i]))
                {
                    while (stackOperations.Count != 0 && IsOperation(stackOperations.Peek()))
                    {
                        if (Priority(mas[i]) <= Priority(stackOperations.Peek()) && Priority(stackOperations.Peek()) == 3)
                        {
                            string proced = EngenerCalculation(stackNumbers.Pop(), stackOperations.Pop()).ToString();
                            stackNumbers.Push(proced);
                        }
                        else if (Priority(mas[i]) <= Priority(stackOperations.Peek()))
                        {
                            string procedure = Calculation(stackNumbers.Pop(), stackNumbers.Pop(), stackOperations.Pop()).ToString();
                            stackNumbers.Push(procedure);
                        }
                        else
                        {
                            stackOperations.Push(mas[i]);
                            break;
                        }
                    }
                    if (stackOperations.Count == 0 || IsOpenBracket(stackOperations.Peek()))
                    {
                        stackOperations.Push(mas[i]);
                    }
                }
                else if (IsOpenBracket(mas[i]))
                {
                    stackOperations.Push(mas[i]);
                }
                else if (IsCloseBracket(mas[i]))
                {
                    while (!IsOpenBracket(stackOperations.Peek()))
                    {
                        string procedure = Calculation(stackNumbers.Pop(), stackNumbers.Pop(), stackOperations.Pop()).ToString();
                        stackNumbers.Push(procedure);
                    }
                    var x = stackOperations.Pop();
                }
                if (i == mas.Length - 1)
                {
                    while (stackOperations.Count > 0)
                    {
                        if (Priority(stackOperations.Peek()) == 3)
                        {
                            string proced = EngenerCalculation(stackNumbers.Pop(), stackOperations.Pop()).ToString();
                            stackNumbers.Push(proced);
                        }
                        else
                        {
                            string procedure = Calculation(stackNumbers.Pop(), stackNumbers.Pop(), stackOperations.Pop()).ToString();
                            stackNumbers.Push(procedure);
                        }
                    }
                }
            }
            return Calculate.Text = stackNumbers.Pop();
        }
        /// <summary>
        /// Give a priority to operations
        /// </summary>
        /// <param name="stroka">current element of expression</param>
        /// <returns>priority of operations</returns>
        static int Priority(string stroka)
        {
            if (stroka.Equals("+") || stroka.Equals("-"))
            {
                return 1;
            }
            else if (stroka.Equals("/") || stroka.Equals("*"))
            {
                return 2;
            }
            return 3;
        }
        /// <summary>
        /// Checks the number it is or not
        /// </summary>
        /// <param name="stroka">current element of expression</param>
        /// <returns>True if number, false if it is not a number</returns>
        private bool IsANumber(string stroka)
        {
            bool number = Double.TryParse(stroka, out double Result);
            return number;
        }
        /// <summary>
        /// Checks the operation it is or not
        /// </summary>
        /// <param name="stroka">current element of expression</param>
        /// <returns>True if operation, false if it is not a operation</returns>
        private bool IsOperation(string stroka)
        {
            string operation = "+-*/cossintanlnlg%√²";
            return operation.Contains(stroka);
        }
        /// <summary>
        /// Checks is it the open bracket or not
        /// </summary>
        /// <param name="stroka">current element of expression</param>
        /// <returns>True if open bracket, false if it is not the open bracket</returns>
        private bool IsOpenBracket(string stroka)
        {
            return stroka.Equals("(");
        }
        /// <summary>
        /// Checks is it the close bracket or not
        /// </summary>
        /// <param name="stroka">current element of expression</param>
        /// <returns>True if close bracket, false if it is not the close bracket</returns>
        private bool IsCloseBracket(string stroka)
        {
            return stroka.Equals(")");
        }
        /// <summary>
        /// Return simple calculation, such as +,-,/,*
        /// </summary>
        /// <param name="value2">second number</param>
        /// <param name="value1">first number</param>
        /// <param name="operation">operation that must been done</param>
        /// <returns>result of calculations</returns>
        private double Calculation(string value2, string value1, string operation)
        {
            double result = 0;
            switch(operation)
            {
                case "+":
                    result = calc.Plus(Double.Parse(value1), Double.Parse(value2));
                    break;
                case "-":
                    result = calc.Minus(Double.Parse(value1), Double.Parse(value2));
                    break;
                case "*":
                    result = calc.Multiply(Double.Parse(value1), Double.Parse(value2));
                    break;
                case "/":
                    result = calc.Div(Double.Parse(value1), Double.Parse(value2));
                    break;
            }
            return result;
        }
        /// <summary>
        /// Return engener calculation, such as cos,sin,tan etc.
        /// </summary>
        /// <param name="value1">number</param>
        /// <param name="operation">operation that must been done</param>
        /// <returns>result of calculations</returns>
        private double EngenerCalculation(string value1, string operation)
        {
            double result = 0;
            switch (operation)
            {
                case "cos":
                    result = calc.Cos(Double.Parse(value1));
                    break;
                case "sin":
                    result = calc.Sin(Double.Parse(value1));
                    break;
                case "tan":
                    result = calc.Tan(Double.Parse(value1));
                    break;
                case "lg":
                    result = calc.Lg(Double.Parse(value1));
                    break;
                case "ln":
                    result = calc.Ln(Double.Parse(value1));
                    break;
                case "%":
                    result = calc.Percent(Double.Parse(value1));
                    break;
                case "√":
                    result = calc.Sqrt(Double.Parse(value1));
                    break;
                case "²":
                    result = calc.Pow(Double.Parse(value1));
                    break;
            }
            return result;
        }
        #endregion
        private void DeleteOneElementButton_Click(object sender, EventArgs e)
        {
            Calculate.Text = Calculate.Text.Remove(Calculate.Text.Length - 1, 1);
            str = str.Remove(str.Length - 1, 1);
        }

        private void BracketsButton_Click(object sender, EventArgs e)
        {
            if (count == 0 || str[str.Length - 1] == ' ')
            {
                Calculate.Text += "(";
                str += "( ";
                count++;
            }
            else
            {
                Calculate.Text += ")";
                str += " )";
                count--;
            }
        }

        private void DotButton_Click(object sender, EventArgs e)
        {
            DotButton.Enabled = false;
            if (Calculate.Text == "" || str.EndsWith(" "))
            {
                Calculate.Text += "0" + DotButton.Tag.ToString();
                str += "0" + ",";               
            }
            else
            {
                Calculate.Text += ",";
                str += ",";
            }
        }

        private void SinButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += SinButton.Tag.ToString();
            str += SinButton.Tag.ToString() + " ";
        }

        private void CosButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += CosButton.Tag.ToString();
            str += CosButton.Tag.ToString() + " ";
        }

        private void TanButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += TanButton.Tag.ToString();
            str += TanButton.Tag.ToString() + " ";
        }

        private void LnButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += LnButton.Tag.ToString();
            str += LnButton.Tag.ToString() + " ";
        }

        private void LgButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += LgButton.Tag.ToString();
            str += LgButton.Tag.ToString() + " ";
        }

        private void SqrtButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += SqrtButton.Tag.ToString();
            str += SqrtButton.Tag.ToString() + " ";
        }

        private void PiButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += PiButton.Tag.ToString();
            str += Math.PI.ToString() + " ";
        }

        private void EButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += EButton.Tag.ToString();
            str += Math.E.ToString() + " ";
        }

        private void PowButton_Click(object sender, EventArgs e)
        {
            Calculate.Text += PowButton.Tag.ToString();
            str += " " + PowButton.Tag.ToString();
        }

        private void Percentbutton_Click(object sender, EventArgs e)
        {
            Calculate.Text += Percentbutton.Tag.ToString();
            str += " " + Percentbutton.Tag.ToString();
        }
        
        private void SignButton_Click(object sender, EventArgs e)
        {
            mas = str.Split(' ');
            s = mas[mas.Length - 1];
            a = Double.Parse(mas[mas.Length - 1]);
            str = str.Replace(s, (a * -1).ToString());
            Calculate.Text = str;
            c++;
        }
    }
}
