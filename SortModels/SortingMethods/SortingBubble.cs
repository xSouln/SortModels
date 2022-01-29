using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Threading;

namespace SortModels
{
    public class SortingBubble : SortingBaseCharting
    {
        public int[] Sequence;
        public int[] Heap;

        public SortingBubble()
        {
            Name.Value = "Bubble";
        }

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
                for (int i = 0; i < array.Length; i++)
                {
                    for (int step = 0; step < array.Length - 1; step++)
                    {
                        if (array[step] > array[step + 1])
                        {
                            temp = array[step + 1];
                            array[step + 1] = array[step];
                            array[step] = temp;
                        }
                    }
                }

                timer.Stop();
                SortingTime.Value = (int)timer.ElapsedTicks;

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
                vector.Add(random.Next(0, 0xff));
                size--;
            }

            data_synchronize.WaitOne();
            Heap = vector.ToArray();
            data_synchronize.Set();
        }
    }
}
