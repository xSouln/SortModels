using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SortModels
{
    public class SortingToPass : SortingBase
    {
        public double[] Sequence;
        public byte[] Heap;

        public SortingToPass()
        {
            Name = "Occurrence";
        }

        public override void Sort(object data)
        {
            byte[] array = data as byte[];

            if (array != null && array.Length > 0)
            {
                Stopwatch timer = new Stopwatch();
                List<double> vector = new List<double>();

                timer.Start();
                //массив количества входимостей
                int[] entry = new int[0xff];

                foreach (byte element in array)
                {
                    entry[element]++;
                }

                int i = 0;
                int j = 0;
                while (i < array.Length)
                {
                    if (entry[j] > 0)
                    {
                        vector.Add(j);
                        entry[j]--;
                        i++;
                    }
                    else
                    {
                        j++;
                    }
                }

                timer.Stop();
                SortingTime = (int)timer.ElapsedMilliseconds;

                this.Heap = array;
                this.Sequence = vector.ToArray();
            }
        }

        public override void UpdateCharts(DispatcherObject context)
        {
            context.Dispatcher.Invoke(() =>
            {
                series.Points.Clear();
                series_heap.Points.Clear();

                if (Sequence != null)
                {
                    data_synchronize.WaitOne();

                    foreach (double element in Sequence)
                    {
                        series.Points.Add(element);
                    }

                    foreach (byte element in Heap)
                    {
                        series_heap.Points.Add(element);
                    }

                    data_synchronize.Set();
                }
            });
        }

        public override void Generate(int size)
        {
            List<byte> vector = new List<byte>();
            Random random = new Random();

            while (size > 0)
            {
                vector.Add((byte)random.Next(0, 0xff));
                size--;
            }

            Heap = vector.ToArray();
        }
    }
}
