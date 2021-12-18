using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox4.Text.Length == 0)
                {
                    MessageBox.Show("Данные заполнены некорректно!");
                } else
                {
                    int g = Convert.ToInt32(textBox1.Text);
                    int l = Convert.ToInt32(textBox2.Text);
                    int p = Convert.ToInt32(textBox4.Text);

                    int d = poly_counter(g);

                    int a_length = degree_counter(g) + l;

                    double pp = p * 0.01;

                    textBox5.Text = (higher_border(pp, d, a_length) * 100).ToString() + "%";
                }
                
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        double higher_border(double p, int d, int a_length)
        {
            int c = 0;
            double res = 0;
            while ((d - 1) != 0)
            {
                c = fact(a_length) / (fact(d) * fact(a_length - d));
                res = res + c * Math.Pow(p, d) * Math.Pow(1 - p, a_length - d);
                d--;
            }
            return (1 - res);
        }
        private int degree_counter(int number)
        {
            int i = 0;
            while (number != 0)
            {
                number /= 10;
                i++;
            }
            i--;
            return i;
        }

        private int fact(int A)
        {
            if (A <= 1)
            {
                return 1;
            }
            return A * fact(A - 1);
        }

        int poly_counter(int number)
        {
            int i = 0;
            while (number != 0)
            {
                if (number % 10 == 1) i++;
                number /= 10;
            }
            return i;
        }

        private int degree(string m)
        {
            int t = 0;

            for (int i = 0; i < m.Length - 1; i++)
            {
                if (m[i] == '1')
                {
                    t = (m.Length - 1) - i;
                    break;
                }
            }

            return t;
        }

        private int poly_div(int m, int gx)
        {
            for (int gx_temp; (degree(Convert.ToString(m, 2)) >= degree_counter(gx));)
            {
                gx_temp = Convert.ToInt32(Convert.ToString(gx), 2) << (degree(Convert.ToString(m, 2)) - degree_counter(gx));
                m = m ^ gx_temp;
            }
            return m;
        }

        private int weight(string[] A, int d)
        {
            int icount = 0;
            for (int i = 0, temp; i != A.Length; i++)
            {
                temp = poly_counter(Convert.ToInt32(A[i]));
                if (temp >= d + 1) icount++;
            }
            return icount;
        }

        private string[] A_counter(int gx, int amount, int d)
        {
            string[] res = new string[amount - d];
            int mr, cx;
            int newm = d;
            int deg = Convert.ToInt32(Math.Pow(2, degree(Convert.ToString(gx, 2))));

            for (int i = 0; i != (amount - d); i++, newm++)
            {
                mr = newm * deg;
                cx = poly_div(mr, gx);
                res[i] = Convert.ToString(mr, 2) + Convert.ToString(cx, 2);
            }

            return res;
        }

        private double exact_val(double p, int d, string[] A, int n)
        {
            double res = 0;
            int Ai;
            while ((d) != n + 1)
            {
                Ai = weight(A, d);
                res = res + (Ai * Math.Pow(p, d) * Math.Pow(1 - p, n - d));
                d++;
            }
            return res;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox4.Text.Length == 0)
                {
                    MessageBox.Show("Данные заполнены некорректно!");
                }
                else
                {
                    int g = Convert.ToInt32(textBox1.Text);
                    int l = Convert.ToInt32(textBox2.Text);
                    int p = Convert.ToInt32(textBox4.Text);

                    int d = degree_counter(g);

                    double pp = p * 0.01;

                    int n = l + degree_counter(g);

                    string[] a = A_counter(Convert.ToInt32(Convert.ToString(g), 2), Convert.ToInt32(Math.Pow(2, 3)), d);

                    double res = exact_val(pp, d, a, n);

                    textBox3.Text = (res * 100).ToString() + "%";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
