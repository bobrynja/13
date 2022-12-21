using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ConsoleApp1
{
    abstract class PO
    {
        public DateTime date;
        abstract public void show_inf();
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
        public override void show_inf()
        {
            Console.WriteLine("Основная информация: ");
            Console.WriteLine("Вид программного обеспечения - свободное");
            Console.Write("Название: " + title);
            Console.WriteLine();
            Console.Write("Производитель: " + maker);
            Console.WriteLine();
            Console.WriteLine(find());
        }
        public override string find(int n = 0)
        {
            return("Программное обеспечение использовать возможно!");
        }

    }

    class Shareware : PO
    {
        public string title;
        public string maker;
        public new DateTime date;
        int months;
        public Shareware(string a, string b, string dt, int n)
        {
            title = a;
            maker = b;
            string[] arr = dt.Split('.');
            date = new DateTime(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), Convert.ToInt32(arr[0]));
            months = n;
        }
        public override void show_inf()
        {
            Console.WriteLine("Основная информация: ");
            Console.WriteLine("Вид программного обеспечения - условно-свободное");
            Console.WriteLine("Название: " + title);
            Console.WriteLine("Производитель: " + maker);
            Console.WriteLine("Дата установки: " + date.ToShortDateString());
            Console.WriteLine("Срок бесплатного обеспечения: " + months + "месяцев");
            Console.WriteLine(find(months));
        }
        public override string find(int month)
        {
            if (date.AddMonths(month) < DateTime.Now) return("Программное обеспечение не доступно");
            else return("Программное обеспечение доступно");
        }


    }

    class Commericial : PO
    {
        public string title;
        public string maker;
        public new DateTime date;
        public int months;
        public double price;
        public Commericial(string a, string b, string dt, int n, double pr)
        {
            title = a;
            maker = b;
            string[] arr = dt.Split('.');
            this.date = new DateTime(Convert.ToInt32(arr[2]), Convert.ToInt32(arr[1]), Convert.ToInt32(arr[0]));
            months = n;
            price = pr;
        }
        public override void show_inf()
        {
            Console.WriteLine("Основная информация: ");
            Console.WriteLine("Вид программного обеспечения - коммерческое");
            Console.WriteLine("Название: " + title);
            Console.WriteLine("Производитель: " + maker);
            Console.WriteLine("Дата установки: " + date.ToShortDateString());
            Console.WriteLine("Срок использования: " + months + "месяцев");
            Console.WriteLine("Цена: " + price + "руб.");
            Console.WriteLine(find(months));
        }
        public override string find(int month)
        {
            if (date.AddMonths(month) < DateTime.Now) return("Программное обеспечение не доступно");
            else return("Программное обеспечение доступно");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Меню:");
            Console.WriteLine("1. Вывод всей информации");
            Console.WriteLine("2. Поиск по текущей дате");
            Console.WriteLine("3. Выход");
            string an = Console.ReadLine();
            while (an != "3")
            {

                StreamReader sr = new StreamReader("./res.txt");
                string line = sr.ReadLine();
                line = sr.ReadLine();
                Free[] res = new Free[10];
                Free[] arr1 = new Free[Convert.ToInt32(line)];
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
                Shareware[] res1 = new Shareware[10];
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
                Commericial[] res2 = new Commericial[10];
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

                PO[][] bd1 = new PO[3][];
                bd1[0] = arr1;
                bd1[1] = arr2;
                bd1[2] = arr3;

                if (an == "1")
                {
                    Console.WriteLine("Вывод информации: ");
                    foreach (var item in bd1)
                    {
                        foreach (var items in item)
                        {
                            items.show_inf();
                            Console.WriteLine();
                        }
                        Console.WriteLine("------------------------------------");

                    }
                }

                if (an == "2")
                {

                    Console.WriteLine("Поиск: ");
                    foreach (var item in res)
                    {
                        if (item == null) break;
                        item.show_inf();
                        Console.WriteLine();

                    }
                    foreach (var item in res1)
                    {
                        if (item == null) break;
                        item.show_inf();
                        Console.WriteLine();

                    }
                    foreach (var item in res2)
                    {
                        if (item == null) break;
                        item.show_inf();
                        Console.WriteLine();

                    }
                }
                Console.WriteLine("Что будем делать дальше?");
                an = Console.ReadLine();
            }
            



          


        }
    }
}

