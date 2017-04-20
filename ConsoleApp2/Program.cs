using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
     class Program
    {
        static void Main(string[] args)
        {
            int x;
            int z;

            // посмотри внимательно на следующий код ввода столбцов и строк
            // вынеси одинаковый код в отдельную функцию, которая тебе будет возвращать целое число
            // передавай в функцию текст вопроса для пользователя
            Console.WriteLine("введите количество строк");
            while (!int.TryParse(Console.ReadLine(), out x))
            {
                Console.WriteLine("Это не число");
                Console.WriteLine("введите количество строк");
            }

            Console.WriteLine("введите количество столбцов");
            while (!int.TryParse(Console.ReadLine(), out z))
            {
                Console.WriteLine("Это не число");
                Console.WriteLine("введите количество столбцов");
            }
            ///


            // очень тяжело читать такой код, где имена переменных состоят из 1 буквы
            // это допускается для счетчиков, но лучше всегда писать имена, которые отражают суть 
            // того, для чего эта переменная нужна
            // переименуй массив a во чтото типа resultArray
            // так же придумай имена для других переменных
            int[,] a = new int[x, z];
            int m = x;
            int n = z;
            int k = 1;
            int t = 0;

            // 1. если так посмотреть, то получается, что у тебя тут будет m*n тактов цикла
            // где в каждом выполняются еще 4 цикла с переменным количеством тактов
            // надо оптимизировать работу алгоритма заполнения так, чтобы заполнение 
            // проходило за n*m тактов цикла

            // 2. необходимо выделить каждый из вложенных циклов в отдельную функцию
            // котрая будет принимать на вход массив и начальные положения
            // тогда код станет читабельным
            for (int y = 0; y < m*n + 1; y++)
            {
                for (int j = t; j < n - 1; j++)
                {
                    if (a[t, j] == 0)
                    {
                        a[t, j] = k;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int i = t; i < m - 1; i++)
                {
                    if (a[i, n - 1] == 0)
                    {
                        a[i, n - 1] = k;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int j = n - 1; j > t; j--)
                {
                    if (a[m - 1, j] == 0)
                    {
                        a[m - 1, j] = k;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int i = m - 1; i > t; i--)
                {
                    if (a[i, t] == 0)
                    {
                        a[i, t] = k;
                        k++;
                    }
                    else
                    {
                        break;
                    }
                }

                m--;
                n--;
                t++;
            }
            
            // вывод результата надо выделить в отдельную функцию
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < z; j++)
                {
                    // это что за читы )
                    // все заполнение должно быть выше 
                    // тут только распечатка массива
                    if (a[i, j] == 0)
                    {
                        a[i, j] = k;
                    }
                    Console.Write("{0} ", a[i, j]);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
