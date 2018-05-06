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
        delegate double Operation(double x, double y);
        private double a = 0;
        private double b = 0;
        private string s = String.Empty;
        private int count = 0;
        private string str = String.Empty;
        private string action = String.Empty;
        private string[] mas;
        CalcClass calc = new CalcClass();

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
            action += "plus";
            Calculate.Text += PlusButton.Tag.ToString();
            str += " " + PlusButton.Tag.ToString() + " ";
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            action += "minus";
            Calculate.Text += MinusButton.Tag.ToString();
            str += " " + MinusButton.Tag.ToString() + " ";
        }

        private void MultiplyButton_Click(object sender, EventArgs e)
        {
            action += "multiply";
            Calculate.Text += MultiplyButton.Tag.ToString();
            str += " " + MultiplyButton.Tag.ToString() + " ";
        }

        private void DivideButton_Click(object sender, EventArgs e)
        {
            action += "divide";
            Calculate.Text += DivideButton.Tag.ToString();
            str += " " + DivideButton.Tag.ToString() + " ";
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Calculate.Clear();
            str = String.Empty;
        }


        private void EqualyButton_Click(object sender, EventArgs e)
        {
            //in the brackets
            if (str.Contains('('))
            {
                for (int i = str.IndexOf('(') + 1; i < str.IndexOf(')'); i++)
                {
                    s += str[i];
                }
                mas = s.Split(' ');
                str = str.Remove(str.IndexOf('('), 1);
                str = str.Remove(str.IndexOf(')'), 1);
                Calcul(s);
            }
            mas = str.Split(' ');
            Calcul(str);
        }
        private void SimpleCalc(ref int i, Operation del)
        {
            s = mas[i - 1].ToString() + " " + mas[i].ToString() + " " + mas[i + 1].ToString();
            a = Double.Parse(mas[i - 1]);
            b = Double.Parse(mas[i + 1]);
            str = str.Replace(s, del.Invoke(a, b).ToString());
            mas = str.Split(' ');
            i = 0;
        }
        private void Calcul(string str)
        {
            if (str.Contains('x') || str.Contains('/'))
            {
                for (int i = 0; i < mas.Length; i++)
                {
                    if (mas[i] == "x")
                    {
                        SimpleCalc(ref i, calc.Multiply);
                    }
                    else if (mas[i] == "/")
                    {
                        SimpleCalc(ref i, calc.Div);
                    }
                }
            }
            if (str.Contains('-') || str.Contains('+'))
            {
                for (int i = 0; i < mas.Length; i++)
                {
                    if (mas[i] == "-")
                    {
                        SimpleCalc(ref i, calc.Minus);
                    }
                    else if (mas[i] == "+")
                    {
                        SimpleCalc(ref i, calc.Plus);
                    }
                }
            }
            foreach(var mss in mas)
            Calculate.Text = mss.ToString();
        }

        private void DeleteOneElementButton_Click(object sender, EventArgs e)
        {
            Calculate.Text = Calculate.Text.Remove(Calculate.Text.Length - 1, 1);
            str = str.Remove(str.Length - 1, 1);
        }

        private void BracketsButton_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                Calculate.Text += "(";
                str += "(";
                count++;
            }
            else
            {
                Calculate.Text += ")";
                str += ")";
                count = 0;
            }
        }

        private void DotButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
