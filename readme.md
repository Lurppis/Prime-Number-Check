# Prime number checker

## AGENDA
## Table of Contents
- [Introduction](#introduction)
- [Code samples](#code-samples)
- [Instalation](#installation)
- [Testing](#testing)


## Introduction

> Hello, this is my school project.
> A program that reads a text file with integers and searching for prime numbers. The numbers found are to be saved to the result file. The source file should be first generated randomly and contain, for example, 1 billion numbers.

## Code Samples

> MyPartitioner code sample:
>
         Model increasing workloads as a triangle 
         divided into equal areas along vertical lines.
         Each partition  is taller and skinnier
         than the last.
> 
``` C#
        public override IList<IEnumerator<int>> GetPartitions(int partitionCount)
        {
            List<IEnumerator<int>> _list = new List<IEnumerator<int>>();
            int end = 0;
            int start = 0;
            int[] nums = CalculatePartitions(partitionCount, source.Count);

            for (int i = 0; i < nums.Length; i++)
            {
                start = nums[i];
                if (i < nums.Length - 1)
                    end = nums[i + 1];
                else
                    end = source.Count;

                _list.Add(GetItemsForPartition(start, end));
            }
            return _list;
        }
```
Partition calculation:
``` C#
        private int[] CalculatePartitions(int partitionCount, int sourceLength)
        {
            int[] partitionLimits = new int[partitionCount];
            partitionLimits[0] = 0;

            double totalWork = sourceLength * (sourceLength * rateOfIncrease);
            totalWork /= 2;
            double partitionArea = totalWork / partitionCount;

            for (int i = 1; i < partitionLimits.Length; i++)
            {
                double area = partitionArea * i;
                partitionLimits[i] = (int)Math.Floor(Math.Sqrt((2 * area) / rateOfIncrease));
            }
            return partitionLimits;
        }
```

> Generator.

Code below generates specific number of integers from range min to max and return as list of integers:
``` C#
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
```

Save numbers to file:
``` C#
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
```

## Installation

> Not ready yet.

## Testing

> Not ready yet.
>Will be added in the nearest future.
