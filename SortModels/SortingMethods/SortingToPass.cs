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
            int[] array = data as int[]; // входной массив

            if (array != null && array.Length > 0)
            {
                int[] sequence = new int[array.Length]; //выходной массив

                HeapSize.Value = array.Length;
                IsComplete.Value = false;
                timer.Restart();

                //массив количества входимостей для каждого символа размерностью 1 байт
                //символ выступает в роли индекса массива
                int[] entrys = new int[0xff];

                //подсчет количества входимостей
                foreach (byte element in array)
                {
                    entrys[element]++;
                }

                //сортировка массива
                int j = 0; // исполняет роль символа
                int i = 0; // индекс сортируемого массива
                while (i < array.Length)
                {
                    if (entrys[j] > 0) // проверка на входимость по j
                    {
                        sequence[i] = j; //запись в массив символа
                        entrys[j]--; // декремент количества входимостей
                        i++; // инкремент индекса сортируемого массива
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
