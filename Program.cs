using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SP_HW_18_09_2024
{
    class Program
    {
            //Напишите программу, которая создает несколько потоков, 
            //каждый из которых моделирует датчик температуры в отдельной комнате.
            //Каждый поток должен периодически генерировать и выводить случайные 
            //значения температуры для своей комнаты.Программа должна остановить 
            //все потоки через заданное время.
        //для первого задания
        static bool stopRequested = false;
            //Используя потоки создать метод, который находит в строке количество слов, 
            //которые начинаются и оканчиваются на одну и ту же букву.
        //для второго задания
        static int wordCount = 0; 
        static object lockObject = new object(); 
        static void Main(string[] args)
        {
            Console.WriteLine("First task start");
            int numberOfRooms = 5; 
            int simulationDuration = 10000; 
            Thread[] threads = new Thread[numberOfRooms];

            for (int i = 0; i < numberOfRooms; i++)
            {
                int roomNumber = i + 1;
                threads[i] = new Thread(() => SimulateTemperature(roomNumber));
                threads[i].Start();
            }

            Thread.Sleep(simulationDuration);

            stopRequested = true;

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("First task end");
            Console.WriteLine("Second task start");

            string input = "Every year we go to Florida. We like to go to the beach." +
                " My favorite beach is called Emerson Beach. " +
                "It is very long, with soft sand and palm trees. " +
                "It is very beautiful. I like to make sandcastles and watch the sailboats go by." +
                " Sometimes there are dolphins and whales in the water! Every morning we look for shells in the sand." +
                " I found fifteen big shells last year. I put them in a special place in my room. " +
                "This year I want to learn to surf. It is hard to surf, but so much fun!" +
                " My sister is a good surfer. She says that she can teach me. I hope I can do it!";

           
            string[] words = input.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int numberOfThreads = 3;
            int chunkSize = words.Length / numberOfThreads; 

            Thread[] threadsT2 = new Thread[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                int start = i * chunkSize;
                int end = (i == numberOfThreads - 1) ? words.Length : start + chunkSize;
                threadsT2[i] = new Thread(() => CountWords(words, start, end));
                threadsT2[i].Start();
            }

            foreach (Thread thread in threadsT2)
            {
                thread.Join();
            }

            Console.WriteLine($"Количество слов, которые начинаются и оканчиваются на одну и ту же букву: {wordCount}");
            Console.WriteLine("Second task End");

            int numberOfCustomers = 20; // Количество покупателей
            int numberOfRegisters = 3; // Количество касс
            Queue<Customer> customerQueue = new Queue<Customer>();

            // Создание очереди покупателей
            for (int i = 1; i <= numberOfCustomers; i++)
            {
                customerQueue.Enqueue(new Customer(i));
            }

            // Создание и запуск потоков для каждой кассы
            Thread[] threadsT3 = new Thread[numberOfRegisters];
            for (int i = 0; i < numberOfRegisters; i++)
            {
                CashRegister register = new CashRegister(i + 1, customerQueue);
                threadsT3[i] = new Thread(register.ProcessCustomers);
                threadsT3[i].Start();
            }

            // Ожидание завершения всех потоков
            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Все кассы завершили работу.");
        }
        //для первого задания
        static void SimulateTemperature(int roomNumber)
        {
            Random random = new Random();
            while (!stopRequested)
            {

                int temperature = random.Next(15, 31);
                Console.WriteLine($"Комната {roomNumber}: Температура = {temperature}°C");
                Thread.Sleep(1000);
            }

            Console.WriteLine($"Комната {roomNumber}: Поток остановлен.");
        }

        //для второго задания
        static void CountWords(string[] words, int start, int end)
        {
            int localCount = 0;

            for (int i = start; i < end; i++)
            {
                string word = words[i].Trim().ToLower();
                if (word.Length > 1 && word[0] == word[^1])
                {
                    localCount++;
                }
            }

            lock (lockObject)
            {
                wordCount += localCount;
            }
        }
    }
}
