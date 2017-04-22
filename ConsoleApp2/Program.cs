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
        /// К функция надо писать вот такие комментарии
        /// Для того чтобы сделать такой комментарий надо встать выше функции и напечатеть подряд три символа /
        /// Тогда студия автоматически сгенерирует такой комментарий - 
        /// описание функции ее входных параметров и результат
        /// тут Описание что делает функция
        /// </summary>
        /// <param name="inputvalue"></param>
        /// <returns></returns>
        static uint EnterValue(string inputvalue)
        {
            Console.WriteLine(inputvalue);
            uint arg;
            while (!uint.TryParse(Console.ReadLine(), out arg))
            {
                Console.WriteLine("необходимо ввести целое положительное число");
                Console.WriteLine(inputvalue);
            }

            // что делать если пользователь захотел выйти из твоей программы ???
            // надо предусмотреть выход из программы на всех этапах ее работы
            return arg;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="A">что за параметр A название параметра функции должно состоять минимум из 1-2 слов
        /// иначе тебе тут надо  будет писать подробное описание описание 
        /// </param>
        static void OutPutResultArray(Array A)
        {
            // что делает эта строка ?
            // старайся чистить код после отладки функции, не оставляй уже не используемых переменных и т.д.
            int resultArray = A.Rank;


            // тут функции A.GetLength(0) могут что-то вычислять внутри
            // почитай про цикл for код сравнения вызывает на каждом такте цикла, а это значит, что 
            // A.GetLength(0) будет вычисляться на каждый такт цикла
            // если подумать, то A.GetLength(0) можно вычислить один раз и запомнить в переменную
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    // вообще говоря возьми себе на заметку, что для формирования строки с параметрами 
                    // надо использовать string.Format тут он используется неявно
                    // в C# 6 уже есть специальный символ для таких строк $ почитай про него
                    Console.Write("{0} ", A.GetValue(i, j));
                }
                Console.WriteLine();
            }

            // если эта функция вывода то она должна только выводить 
            // опять размазываешь логику 
            // ожидание вода пользователя нужно вынести к контекст вызова этой функции
            Console.ReadLine();
        }

        static int stepone(int[,] A, uint tl, uint tc, int c, int cv)
        {
            // если папаметры функции не используются в функции то не надо их принимать
            

            // функции называются в конвенции с большой буквы каждое слово 
            // эта функция должна быть названа StepOne

            // название функции не определяет ее суть 
            // эта функция бежит по массиву слева направо и заполняет массив
            // придумай своим функциям более информативное название например FillArrayFromLeftToRight

            // тоже самое с названиями всех переменных, расшифруй их хотябы двумя словами

            for (int j = c; j < tc - 1; j++)
            {
                // вопрос что выполнится быстрее сравнение двух числе
                // или взятие индексатора массива и сравнение двух чисел ?
                // у тебя есть counter это значение определяющее количество пройденных кругов
                // используй его и длины массива для того, чтобы определить, когда тебе надо
                // выходить из цикла
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
            // lines я бы понял как коллекцию линий
            // а это на самом деле linesCount
            // назови переменные так, чтобы было более понятен их смысл
            uint lines = EnterValue("Введите количество строк");
            uint columns = EnterValue("Введите количество столбцов");

            int[,] resultArray = new int[lines, columns];
            // каждое слово в имени переменной пишется с большой буквы

            // избегай в названиях следующие слова tmp такие переменные обычно используют для отладки 
            // и удаляются в релизе
            // непонятно зачем именно tmpline и tmpcolumn
            // если это текущий размер массива с которым работают функции нашего алгоритма, то так и надо их назвать
            uint tmpline = lines;
            uint tmpcolumn = columns;
            // лучше назвать такущее значение ячейки
            int cellvalue = 1;
            int counter = 0;
            uint tact;

            // почитай про оператор ?
            // нижние 8 строк можно записать в одну следующим образом
            // tact = lines < columns ? lines/2 : columns/2;
            if (lines < columns)
            {
                tact = lines / 2;
            }
            else
            {
                tact = columns / 2;
            }


            // в каждой из функций есть один лишний параметр зачем ?
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

            // непонятно зачем тебе нужен этот шаг 5 ?
            // у тебя уже есть все функции, которые заполняют значения во всех направлениях
            // измени алгоритм так, чтобы исключить эту фнкцию
            stepfive(resultArray, lines, columns, cellvalue);

            // вот посмотри находясь тут неочевидно что программа не заканчивается 
            // а еще ожидает ввода пользователя
            OutPutResultArray(resultArray);
        }
    }
}
