using System;
using System.Collections;
using System.Collections.Generic;

namespace Linear_feedback_shift_register
{
    class Program
    {
        static void Main(string[] args)
        {
            byte init_byte = 5;

            while (true)
            {
                string f_num = "";
                Console.Title = "Регистр сдвига с линейной обратной связью";
                Console.WriteLine("Выбирите функию:");
                Console.WriteLine("1. Сгенерировать и вывести массив чисел");
                Console.WriteLine("2. Длина периода генератора в битах");
                Console.WriteLine("3. Определение количества четных и нечетных чисел в одном периоде при однобайтовом\nпредставлении выходной последовательности");
                Console.WriteLine("4. Определение количества нулей и единиц в одном периоде при битовом представлении\nвыходной последовательности");
                Console.WriteLine("5. Выход");
                Console.Write("Функция: ");
                f_num = Console.ReadLine();

                switch (f_num)
                {
                    case "1":
                        foreach (int i in GenerateArray(init_byte)) Console.Write(i + " ");
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine("Длина периода генератора в битах равна " + FindPeriodLenght(init_byte));
                        break;
                    case "3":
                        CountOfEvenAndOdd(init_byte);
                        break;
                    case "4":
                        CountOfZeroesAndOne(init_byte);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Выберите номер функции!");
                        break;
                }
                Console.ReadLine();
                Console.Clear();
            }
        }

        static byte F(byte initial)
        {
            //1+x^1+x^7+x^8
            byte result = 0;
            byte bit = (byte)((initial >> 0) ^ (initial >> 1) ^ (initial >> 7));
            result = (byte)((initial >> 1) | (bit << 7));
            return result;
        }

        static byte[] GenerateArray(byte initial)
        {
            List<byte> ByteArray = new List<byte>();
            ByteArray.Add(initial);
            byte current_byte = F(initial);
            while (current_byte != initial)
            {
                ByteArray.Add(current_byte);
                current_byte = F(current_byte);
            }
            return ByteArray.ToArray();
        }

        static void CountOfEvenAndOdd(byte initial)
        {
            int countofeven = 0;
            int countofodd = 0;
            byte[] bytes = GenerateArray(initial);
            foreach (byte b in bytes) if (b % 2 == 0) countofeven++;
            Console.WriteLine("Четных: " + countofeven);
            foreach (byte b in bytes) if (b % 2 == 1) countofodd++;
            Console.WriteLine("Нечетных: " + countofodd);
        }

        static void CountOfZeroesAndOne(byte initial)
        {
            int countofzeroes = 0;
            int countofone = 0;
            BitArray array = new BitArray(GenerateArray(initial));
            foreach (bool b in array)
            {
                if (b) countofone++;
                else countofzeroes++;
            }
            Console.WriteLine("Единиц: " + countofone);
            Console.WriteLine("Нулей: " + countofzeroes);
        }

        static int FindPeriodLenght(byte initial)
        {
            BitArray array = new BitArray(GenerateArray(initial));
            return array.Length;
        }
    }
}
