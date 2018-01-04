using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PrimeNumberChecker
{
    class MyPartitioner : Partitioner<int>
    {

        List<int> source;
        double rateOfIncrease = 0;

        public MyPartitioner(List<int> source, double rate)
        {
            this.source = source;
            rateOfIncrease = rate;
        }

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


        IEnumerator<int> GetItemsForPartition(int start, int end)
        {
            for (int i = start; i < end; i++)
                yield return source[i];
        }
    }
}
