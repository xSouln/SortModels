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
    public class SortingToPass : SortingBaseCharting
    {
        public SortingToPass()
        {
            Name.Value = "To Pass";

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

            if (array != null && array.Length > 0)
            {
                int[] sequence = new int[array.Length];

                HeapSize.Value = array.Length;
                IsComplete.Value = false;
                timer.Restart();

                //массив количества входимостей
                int[] entrys = new int[0xff];

                foreach (byte element in array)
                {
                    entrys[element]++;
                }

                int j = 0;
                int i = 0;
                while (i < array.Length)
                {
                    if (entrys[j] > 0)
                    {
                        sequence[i] = j;
                        entrys[j]--;
                        i++;
                    }
                    else
                    {
                        j++;
                    }
                }

                timer.Stop();
                SortingTime.Value = (int)timer.ElapsedTicks;
                IsComplete.Value = true;

                data_synchronize.WaitOne();

                this.Heap = array;
                this.Sequence = sequence;

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
