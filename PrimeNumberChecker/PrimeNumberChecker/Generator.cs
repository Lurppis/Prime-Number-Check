using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PrimeNumberChecker
{
    class Generator
    {
        static Random random = new Random();
        public static string saveFileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static void SaveNumbersToFile(List<int> numbers, string fileName)
        {
            using (var stream = File.OpenWrite(saveFileName + "\\" + fileName + ".txt"))
            {
                using (var writer = new StreamWriter(stream))
                {
                    foreach (var line in numbers)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }

        public static List<int> LoadNumbersFromFile(string fileName)
        {
            var numbers = new List<int>();
            using (var stream = File.OpenRead(saveFileName + "\\" + fileName + ".txt"))
            {
                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        numbers.Add(int.Parse(reader.ReadLine()));
                    }
                }
            }
            return numbers;
        }

        public static List<int> GenerateRandomies(int count, int min, int max)
        {
            if (max <= min || count < 0 || (count > max - min && max - min > 0))
            {
                throw new ArgumentOutOfRangeException(paramName: "Range " + min + " to " + max + " (" + ((Int64)max - min) 
                    + " values), or count " + count + " is illegal!");
            }

            HashSet<int> candidates = new HashSet<int>();

            for (int top = max - count; top < max; top++)
            {
                if (!candidates.Add(random.Next(min, top + 1)))
                {
                    candidates.Add(top);
                }
            }
            List<int> result = candidates.ToList();

            for (int i = result.Count - 1; i > 0; i--)
            {
                int k = random.Next(i + 1);
                int temp = result[k];
                result[k] = result[i];
                result[i] = temp;
            }
            return result;
        }

        public static List<int> GenerateRandomies(int count)
        {
            return GenerateRandomies(count, 0, Int32.MaxValue);
        }
    }
}
