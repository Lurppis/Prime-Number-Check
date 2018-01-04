using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeNumberChecker
{
    class Program
    {
        static Random random = new Random();
        static List<int> PrimesList = new List<int>();
        static Stopwatch sw = new Stopwatch();
        static void Main(string[] args)
        {
            sw.Start();
            var numbers = Generator.GenerateRandomies(int.Parse(args[0]));
            sw.Stop();
            Console.WriteLine("Generating random numbers. (" + sw.ElapsedMilliseconds + "ms)");
            sw.Reset();

            sw.Start();
            Generator.SaveNumbersToFile(numbers, args[1]);
            sw.Stop();
            Console.WriteLine("Saving generated numbers to file. " + sw.ElapsedMilliseconds + "ms)");
            sw.Reset();

            sw.Start();
            numbers = Generator.LoadNumbersFromFile(args[1]);
            sw.Stop();
            Console.WriteLine("Loading generated numbers to field. " + sw.ElapsedMilliseconds + "ms)");
            sw.Reset();

            sw.Start();
            MyPartitioner myPartitioner = new MyPartitioner(numbers, 2);
            var query = from n in myPartitioner.AsParallel()
                        where IsPrime(n)
                        select n;
            Parallel.ForEach(query, item =>
            {
                PrimesList.Add(item);
            });
            sw.Stop();
            Console.WriteLine("Multithreaded checking prime numbers. " + sw.ElapsedMilliseconds + "ms)");
            sw.Reset();

            sw.Start();
            Generator.SaveNumbersToFile(PrimesList, args[2]);
            sw.Stop();
            Console.WriteLine("Saving prime numbers to file. " + sw.ElapsedMilliseconds + "ms)");

            Console.WriteLine($"Application generate {PrimesList.Count} prime nubers!\n\n\n" +
                $"\tPress enter to exit!");
            Console.ReadLine();
        }

        private static bool IsPrime(int candidate)
        {
            if ((candidate & 1) == 0)
            {
                if (candidate == 2)
                    return true;
                else
                    return false;
            }

            for (int i = 3; (i * i) < candidate; i += 2)
            {
                if ((candidate % i) == 0)
                    return false;
            }
            return candidate != 1;
        }
    }
}
