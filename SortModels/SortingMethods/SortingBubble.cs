using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Threading;

namespace SortModels
{
    public class SortingBubble : SortingBaseCharting
    {
        public SortingBubble()
        {
            Name.Value = "Bubble";

            thread.Start();
        }

        protected override void Handler()
        {
            while (true)
            {
                SortingTime.Value = (int)timer.ElapsedTicks;
                Thread.Sleep(300);
            }
        }

        public override void Sort(object data)
        {
            int[] array = data as int[];
            int[] clone;

            if (array != null && array.Length > 0)
            {
                clone = new int[array.Length];

                for (int i = 0; i < array.Length; i++)
                {
                    clone[i] = array[i];
                }

                HeapSize.Value = array.Length;
                IsComplete.Value = false;
                timer.Restart();

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
                IsComplete.Value = true;

                data_synchronize.WaitOne();

                this.Heap = clone;
                this.Sequence = array;

                data_synchronize.Set();
            }
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
