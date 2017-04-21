using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static uint EnterValue(string inputvalue)
        {
            Console.WriteLine(inputvalue);
            uint arg;
            while (!uint.TryParse(Console.ReadLine(), out arg))
            {
                Console.WriteLine("необходимо ввести целое положительное число");
                Console.WriteLine(inputvalue);
            }
            return arg;
        }

        static void OutPutResultArray(Array A)
        {
            int resultArray = A.Rank;
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.Write("{0} ", A.GetValue(i, j));
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        static int stepone(int [,] A, uint tl , uint tc , int c, int cv)
        {
            for (int j = c; j < tc - 1; j++)
            {
                if (A[c, j] == 0)
                {
                    A[c, j] = cv;
                    cv++;
                }
                else
                {
                    break;
                }
            }
            return cv;
        }

        static int steptwo(int[,] A, uint tl, uint tc, int c, int cv)
        {
            for (int i = c; i < tl - 1; i++)
            {
                if (A[i, tc - 1] == 0)
                {
                    A[i, tc - 1] = cv;
                    cv++;
                }
                else
                {
                    break;
                }
            }
            return cv;
        }

        static int stepthree(int[,] A, uint tl, uint tc, int c, int cv)
        {
            for (uint j = tc - 1; j > c; j--)
            {
                if (A[tl - 1, j] == 0)
                {
                    A[tl - 1, j] = cv;
                    cv++;
                }
                else
                {
                    break;
                }
            }
            return cv;
        }

        static int stepfour(int[,] A, uint tl, uint tc, int c, int cv)
        {
            for (uint i = tl - 1; i > c; i--)
            {
                if (A[i, c] == 0)
                {
                    A[i, c] = cv;
                    cv++;
                }
                else
                {
                    break;
                }
            }
            return cv;
        }

        static void stepfive(int[,] A, uint l, uint c, int cv)
        {
            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    if (A[i, j] == 0)
                    {
                        A[i, j] = cv;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            uint lines = EnterValue("Введите количество строк");
            uint columns = EnterValue("Введите количество столбцов");

            int[,] resultArray = new int[lines, columns];
            uint tmpline = lines;
            uint tmpcolumn = columns;
            int cellvalue = 1;
            int counter = 0;
            uint tact;

            if (lines < columns)
            {
                tact = lines / 2;
            }
            else
            {
                tact = columns / 2;
            }

            for (int y = 0; y < tact; y++)
            {
                cellvalue = stepone(resultArray, tmpline, tmpcolumn, counter, cellvalue);

                cellvalue = steptwo(resultArray, tmpline, tmpcolumn, counter, cellvalue);

                cellvalue = stepthree(resultArray, tmpline, tmpcolumn, counter, cellvalue);

                cellvalue = stepfour(resultArray, tmpline, tmpcolumn, counter, cellvalue);

                tmpline--;
                tmpcolumn--;
                counter++;
            }

            stepfive(resultArray, lines, columns, cellvalue);

            OutPutResultArray(resultArray);
        }
    }
}
