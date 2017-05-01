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
            // тут бы лучше подошло название value или readedValue
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
            // Локальные переменные называются с маленькой буквы, иначе их можно принять за поля класса 
            // ( поймешь когда дойдем до классов)
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
        /// <param name="CurrentColumnValue"> внутренние переменные и параметры функций называются с маленькой буквы</param>
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

                // Ответ:
                // есть 1000 и 1 способ как сделать выход тут )
                // тут идет речь об оптимизации
                // если у тебя код не оптимизирован по количеству операций 
                // (в данном случает оператор [] более затратен чем сравнение двух чисел по скорости), то 
                // такое решение может не подойти заказчику ПО
                // тебе надо выявить зависимость длинны твоего цикла от круга, тоесть Counter

                // посмотри в дебаге внимательно
                // у тебя цикл уже начинается и заканчивается в нужных координатах массива
                // проверка на содержимое ячейки излишняя

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

            // главное замечание все локальные переменные функции, 
            // которые будут "удалены" по окончании функции надо называть с маленькой буквы!


            Console.WriteLine("Press any key to start, press ESC to exit app");
            ConsoleKey Key = Console.ReadKey().Key;
            
            // всегда делай полное форматироваие тело if должно находиться на следующей строке под if
            // иначе тут с первого взгляду можно подумать, что ты по Escape просто чистишь экран
            // вместо Environment.Exit(0); лучше просто написать return;
            if (Key == ConsoleKey.Escape) Environment.Exit(0); 
            Console.Clear();

            uint LinesCount = EnterValue("Введите количество строк");
            uint ColumnsCount = EnterValue("Введите количество столбцов");

            int[,] ResultArray = new int[LinesCount, ColumnsCount];
            uint CurrentLineValue = LinesCount;
            uint CurrentColumnValue = ColumnsCount;
            int CurrentCellValue = 1;
            int Counter = 0;
            uint Tact; // лучше назвать это переменную maxCountOfTacts а то можно подумать что это текущий такт
            
            Tact = LinesCount < ColumnsCount ? LinesCount / 2 + 1: ColumnsCount / 2 + 1;

            for (int y = 0; y < Tact; y++)
            {
                // коментарий не обязательный к исправлению, потом тебе пригодится когда будем делать на основе классов
                // переменные CurrentColumnValue и CurrentLineValue излишние
                // их можно вычислять из LinesCount - Counter и ColumnsCount - Counter
                // их можно оставить для читаемости кода, но не вести итерационно а вычислять 

                
                CurrentCellValue = FillArrayLeftRight(ResultArray, CurrentColumnValue, Counter, CurrentCellValue);

                CurrentCellValue = FillArrayUpDown(ResultArray, CurrentLineValue, CurrentColumnValue, Counter, CurrentCellValue);

                CurrentCellValue = FillArrayRightLeft(ResultArray, CurrentLineValue, CurrentColumnValue, Counter, CurrentCellValue);

                CurrentCellValue = FillArrayDownUp(ResultArray, CurrentLineValue, Counter, CurrentCellValue);

                CurrentLineValue--;
                CurrentColumnValue--;
                // тут у тебя Counter дублирует y зачем использвоать две переменные для одного и того же
                // сделай такой цикл for (int Counter = 0; Counter < Tact; Counter++)
                // int y можно удалить
                Counter++;
            }

            OutPutResultArray(ResultArray);

            Console.ReadLine();
        }
    }
}
