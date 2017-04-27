using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        /// <summary>
        /// Функция считывает значение, введенное пользователем
        /// </summary>
        /// <param name="inputvalue"> Строка, содержит указания пользователю </param>
        /// <returns> Возвращается значение, введенное пользователем</returns>
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

        /// <summary>
        /// Функция, выводит итоговый массив на экран
        /// </summary>
        /// <param name="A"> Массив, который будем выводить </param>
        static void OutPutResultArray(Array A)
        {
            int ArrayLineLength = A.GetLength(0);
            int ArrayColumnLength = A.GetLength(1);

            for (int i = 0; i < ArrayLineLength; i++)
            {
                for (int j = 0; j < ArrayColumnLength; j++)
                {
                    Console.Write($"{A.GetValue(i, j)} ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Функция, заполняет строки массива слева направо
        /// </summary>
        /// <param name="A"></param>
        /// <param name="CurrentColumnValue"></param>
        /// <param name="Counter"></param>
        /// <param name="CurrentCellValue"></param>
        /// <returns></returns>
        static int FillArrayLeftRight(int[,] A, uint CurrentColumnValue, int Counter, int CurrentCellValue)
        {
            for (int j = Counter; j < CurrentColumnValue; j++)
            {
                // А вот без этого никак. Можно определить, когда надо выходить
                // из цикла, без индексации массива, но суть в том,
                // что тогда значения начинают перезаписываться.
                // И выходить из этого цикла все время при разных условиях надо.
                // Так что я не знаю, как сделать по-другому
                if (A[Counter, j] == 0)
                {
                    A[Counter, j] = CurrentCellValue;
                    CurrentCellValue++;
                }
                else
                {
                    break;
                }
            }
            return CurrentCellValue;
        }

        static int FillArrayUpDown(int[,] A, uint CurrentLineValue, uint CurrentColumnValue, int Counter, int CurrentCellValue)
        {
            for (int i = Counter + 1; i < CurrentLineValue; i++)
            {
                if (A[i, CurrentColumnValue - 1] == 0) 
                {
                    A[i, CurrentColumnValue - 1] = CurrentCellValue;
                    CurrentCellValue++;
                }
                else
                {
                    break;
                }
            }
            return CurrentCellValue;
        }

        static int FillArrayRightLeft(int[,] A, uint CurrentLineValue, uint CurrentColumnValue, int Counter, int CurrentCellValue)
        {
            if (CurrentColumnValue > 1)
            for (uint j = CurrentColumnValue - 2; j > Counter; j--)
            {
                if (A[CurrentLineValue - 1, j] == 0)
                {
                    A[CurrentLineValue - 1, j] = CurrentCellValue;
                    CurrentCellValue++;
                }
                else
                {
                    break;
                }
            }
            return CurrentCellValue;
        }
        static int FillArrayDownUp(int[,] A, uint CurrentLineValue, int Counter, int CurrentCellValue)
        {
            
            for (uint i = CurrentLineValue - 1; i > Counter; i--)
            {
                if (A[i, Counter] == 0)
                {
                    A[i, Counter] = CurrentCellValue;
                    CurrentCellValue++;
                }
                else
                {
                    break;
                }
            }
            return CurrentCellValue;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start, press ESC to exit app");
            ConsoleKey Key = Console.ReadKey().Key;
            if (Key == ConsoleKey.Escape) Environment.Exit(0);
            Console.Clear();

            uint LinesCount = EnterValue("Введите количество строк");
            uint ColumnsCount = EnterValue("Введите количество столбцов");

            int[,] ResultArray = new int[LinesCount, ColumnsCount];
            uint CurrentLineValue = LinesCount;
            uint CurrentColumnValue = ColumnsCount;
            int CurrentCellValue = 1;
            int Counter = 0;
            uint Tact;
            
            Tact = LinesCount < ColumnsCount ? LinesCount / 2 + 1: ColumnsCount / 2 + 1;

            for (int y = 0; y < Tact; y++)
            {
                CurrentCellValue = FillArrayLeftRight(ResultArray, CurrentColumnValue, Counter, CurrentCellValue);

                CurrentCellValue = FillArrayUpDown(ResultArray, CurrentLineValue, CurrentColumnValue, Counter, CurrentCellValue);

                CurrentCellValue = FillArrayRightLeft(ResultArray, CurrentLineValue, CurrentColumnValue, Counter, CurrentCellValue);

                CurrentCellValue = FillArrayDownUp(ResultArray, CurrentLineValue, Counter, CurrentCellValue);

                CurrentLineValue--;
                CurrentColumnValue--;
                Counter++;
            }

            OutPutResultArray(ResultArray);

            Console.ReadLine();
        }
    }
}
