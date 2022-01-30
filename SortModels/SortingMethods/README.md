## Содержание
1. [Алгоритмы сортировки](#алгоритмы-сортировки)
2. [Код сортировки SortingToPass](#код-сортировки-sortingtopass)

## Алгоритмы сортировки
- Алгоритмы сортировки содержаться в папке SortingMethods.
- Включены два метода сартировки это:
>- сорторовка пузырьком (SortingBubble)
>- сортировка в два прохода (SortingToPass).

## Код сортировки SortingToPass
``` c#
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
```
