using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SortModels
{
    public class SortingBubble : SortingBase
    {
        public int[] Sequence;
        public int[] Heap;

        public override void Sort(object data)
        {
            int[] array = data as int[];
            int[] clone;

            if (array != null && array.Length > 0)
            {
                Stopwatch timer = new Stopwatch();
                List<double> vector = new List<double>();
                clone = new int[array.Length];

                for (int i = 0; i < array.Length; i++)
                {
                    clone[i] = array[i];
                }

                timer.Start();

                int temp;
                for (int write = 0; write < array.Length; write++)
                {
                    for (int sort = 0; sort < array.Length - 1; sort++)
                    {
                        if (array[sort] > array[sort + 1])
                        {
                            temp = array[sort + 1];
                            array[sort + 1] = array[sort];
                            array[sort] = temp;
                        }
                    }
                }

                timer.Stop();
                SortingTime = (int)timer.ElapsedTicks;

                this.Heap = clone;
                this.Sequence = array;
            }
        }

        public override void UpdateTemplate(DispatcherObject context)
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

        public override void Sort()
        {
            Sort(Heap);
        }

        public override void Generate(int size)
        {
            List<int> vector = new List<int>();
            Random random = new Random();

            while (size > 0)
            {
                vector.Add(random.Next(-1000, 1000));
                size--;
            }

            Heap = vector.ToArray();
        }
    }
}
