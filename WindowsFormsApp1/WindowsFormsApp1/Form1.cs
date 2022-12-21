using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        abstract class PO
        {
            abstract public string show_inf();
            abstract public string find(int n);
        }

        class Free : PO
        {
            public string title;
            public string maker;
            public Free(string a, string b)
            {
                title = a;
                maker = b;
            }
            public override string show_inf()
            {
                string a = "";
                a+=("Основная информация: ") + Environment.NewLine;
                a+=("Вид программного обеспечения - свободное") + Environment.NewLine;
                a+=("Название: " + title) + Environment.NewLine;
                
                a+=("Производитель: " + maker)+Environment.NewLine;
                
                a+=find()+Environment.NewLine;
                return a;
            }
            public override string find(int n = 0)
            {
                return ("Программное обеспечение использовать возможно!");
            }
        }

        class Shareware : PO
        {
            public string title;
            public string maker;
            DateTime date;
            int months;
            public Shareware(string a, string b, string dt, int n)
            {
                title = a;
                maker = b;
                string[] arr = dt.Split('.');
                date = new DateTime(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), Convert.ToInt32(arr[0]));
                months = n;
            }
            public override string show_inf()
            {
                string a = "";
                a+=("Основная информация: ") + Environment.NewLine;
                a+=("Вид программного обеспечения - условно-свободное") + Environment.NewLine;
                a+=("Название: " + title) + Environment.NewLine;
                a+=("Производитель: " + maker) + Environment.NewLine;
                a+=("Дата установки: " + date.ToShortDateString()) + Environment.NewLine;
                a+=("Срок бесплатного обеспечения: " + months + "месяцев") + Environment.NewLine;
                a+=find(months) + Environment.NewLine;
                return a;
            }
            public override string find(int month)
            {
                if (date.AddMonths(month) < DateTime.Now) return ("Программное обеспечение не доступно");
                else return ("Программное обеспечение доступно");
            }
        }

        class Commericial : PO
        {
            public string title;
            public string maker;
            public DateTime date;
            public int months;
            public double price;
            public Commericial(string a, string b, string dt, int n, double pr)
            {
                title = a;
                maker = b;
                string[] arr = dt.Split('.');
                date = new DateTime(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), Convert.ToInt32(arr[0]));
                months = n;
                price = pr;
            }
            public override string show_inf()
            {
                string a = "";
                a+=("Основная информация: ") + Environment.NewLine;
                a+=("Вид программного обеспечения - коммерческое") + Environment.NewLine;
                a+=("Название: " + title) + Environment.NewLine;
                a+=("Производитель: " + maker) + Environment.NewLine;
                a+=("Дата установки: " + date.ToShortDateString()) + Environment.NewLine;
                a+=("Срок использования: " + months + "месяцев") + Environment.NewLine;
                a+=("Цена: " + price + "руб.") + Environment.NewLine;
                a+=find(months) + Environment.NewLine;
                return a;
            }
            public override string find(int month)
            {
                if (date.AddMonths(month) < DateTime.Now) return ("Программное обеспечение не доступно");
                else return ("Программное обеспечение доступно");
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        PO[][] bd1;
        Free[] res;
        Shareware[] res1;
        Commericial[] res2;
        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            foreach (var item in bd1)
            {
                foreach (var items in item)
                {
                    textBox3.Text+=items.show_inf();
                    
                }
                textBox3.Text += "----------------------------"+Environment.NewLine;
                

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("./res.txt");
            string line = sr.ReadLine();
            line = sr.ReadLine();
            Free[] arr1 = new Free[Convert.ToInt32(line)];
            res = new Free[10];
            line = sr.ReadLine();
            int i = 0;
            while (line != "Shareware")
            {
                string arg1 = line;
                line = sr.ReadLine();
                string arg2 = line;
                arr1[i] = new Free(arg1, arg2);
                res[i] = new Free(arg1, arg2);
                i++;
                line = sr.ReadLine();
            }
            line = sr.ReadLine();

            string const_date = DateTime.Now.ToShortDateString();
            Shareware[] arr2 = new Shareware[Convert.ToInt32(line)];
            line = sr.ReadLine();
            res1 = new Shareware[10];
            i = 0; int l = 0;
            while (line != "Commericial")
            {
                string arg1 = line;
                line = sr.ReadLine();
                string arg2 = line;
                line = sr.ReadLine();
                string arg3 = line;
                line = sr.ReadLine();
                int arg4 = Convert.ToInt32(line);
                arr2[i] = new Shareware(arg1, arg2, arg3, arg4);
                if (arr2[i].find(arg4) == "Программное обеспечение доступно")
                {
                    res1[l] = new Shareware(arg1, arg2, arg3, arg4);
                    l++;
                }
                i++;
                line = sr.ReadLine();
            }
            line = sr.ReadLine();

            Commericial[] arr3 = new Commericial[Convert.ToInt32(line)];
            i = 0; l = 0;
            res2 = new Commericial[10];
            line = sr.ReadLine();
            while (line != null)
            {
                string arg1 = line;
                line = sr.ReadLine();
                string arg2 = line;
                line = sr.ReadLine();
                string arg3 = line;
                line = sr.ReadLine();
                int arg4 = Convert.ToInt32(line);
                line = sr.ReadLine();
                double arg5 = Convert.ToDouble(line);
                arr3[i] = new Commericial(arg1, arg2, arg3, arg4, arg5);
                if (arr3[i].find(arg4) == "Программное обеспечение доступно")
                {
                    res2[l] = new Commericial(arg1, arg2, arg3, arg4, arg5);
                    l++;
                }
                i++;
                line = sr.ReadLine();
            }
            sr.Close();

            bd1 = new PO[3][];
            bd1[0] = arr1;
            bd1[1] = arr2;
            bd1[2] = arr3;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString();
            textBox2.Text = "";
            foreach (var item in res)
            {
                if (item == null) break;
                item.show_inf();
                textBox2.Text += Environment.NewLine;

            }
            foreach (var item in res1)
            {
                if (item == null) break;
                textBox2.Text+=item.show_inf();
                textBox2.Text += Environment.NewLine;

            }
            foreach (var item in res2)
            {
                if (item == null) break;
                textBox2.Text+=item.show_inf();
                textBox2.Text += Environment.NewLine;

            }
        }
    }
}
