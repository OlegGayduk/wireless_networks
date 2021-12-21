using System;
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

                    textBox5.Text = higher_border(pp, d, a_length).ToString() + "%";
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
                number /= 2;
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

        private int poly_counter(int number)
        {
            int i = 0;

            while (number != 0)
            {
                if (number % 2 == 1) i++;
                number /= 2;
            }

            return i;
        }
        private int poly_div(int m, int gx)
        {
            for (int gx_temp; (degree_counter(m) >= degree_counter(gx));)
            {
                gx_temp = gx << (degree_counter(m) - degree_counter(gx));
                m = m ^ gx_temp;
            }

            return m;
        }
        private int[] arr_counter(int g, int amount, int d)
        {
            int[] res = new int[amount];
            int mr, cx;
            int newm = 1;
            int deg = Convert.ToInt32(Math.Pow(2, degree_counter(g)));

            for (int i = 0; i != amount; i++, newm++)
            {
                mr = newm * deg;
                cx = poly_div(mr, g);
                res[i] = mr + cx;
            }

            return res;
        }

        private int weight(int[] arr, int d)
        {
            int c = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (poly_counter(arr[i]) >= (d + 1)) c++;
            }

            return c;
        }

        private double exact_val(double p, int d, int[] arr, int n)
        {
            int Ai = 0;
            double res = 0;

            while (d != (n + 1))
            {
                Ai = weight(arr, d);
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
                    int g = Convert.ToInt32(textBox1.Text, 2);
                    int l = Convert.ToInt32(textBox2.Text);
                    int p = Convert.ToInt32(textBox4.Text);

                    int d = degree_counter(g);

                    int m = Convert.ToInt32(l * Math.Pow(2, degree_counter(g)));

                    double pp = p * 0.01;

                    int[] arr = arr_counter(g, l, d);

                    int n = degree_counter(m);

                    double res = exact_val(pp, d, arr, n);

                    textBox3.Text = res.ToString() + "%";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
