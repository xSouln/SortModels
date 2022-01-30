using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
    public abstract class SortingBase : TemplateAdapter, INotifyPropertyChanged, IDisposable
    {
        public ObservableCollection<UIProperty> Propertys { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public UIProperty<string> Name { get; set; } = new UIProperty<string>(new Templates.TemplateContent()) { Name = "Metod", Value = "base" };
        public UIProperty<int> SortingTime { get; set; } = new UIProperty<int>(new Templates.TemplateContent()) { Name = "Elapsed ticks" };
        public UIProperty<int> PercentCompleted { get; set; } = new UIProperty<int>(new Templates.TemplateContent()) { Name = "Percent completed" };
        public UIProperty<bool> IsComplete { get; set; } = new UIProperty<bool>(new Templates.TemplateContent()) { Name = "IsComplete", Value = true };
        public UIProperty<int> HeapSize { get; set; } = new UIProperty<int>(new Templates.TemplateContent()) { Name = "Heap size" };

        public int[] Sequence { get; set; }
        public int[] Heap { get; set; }

        protected AutoResetEvent data_synchronize = new AutoResetEvent(true);
        protected Stopwatch timer = new Stopwatch();
        protected Thread thread;

        public SortingBase()
        {
            TemplateInit();

            Propertys = new ObservableCollection<UIProperty>
            {
                Name,
                SortingTime,
                IsComplete,
                PercentCompleted,
                HeapSize
            };

            thread = new Thread(Handler);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual object GetSequence() => null;

        public virtual object GetHeap() => null;

        public override FrameworkElement Container
        {
            get => base.Container;
            set => base.Container = value;
        }

        protected virtual void TemplateInit()
        {
            
        }
        
        public virtual void UpdateTemplate(DispatcherObject context)
        {
            if (context != null)
            {
                context.Dispatcher.Invoke(UpdateTemplate);
            }
        }

        public virtual void UpdateTemplate()
        {

        }

        public virtual void Sort(object data)
        {

        }

        public virtual Task SortAsync(object data)
        {
            return Task.Run(() => Sort(data));
        }

        protected virtual void Handler()
        {

        }

        public virtual void Sort()
        {

        }

        public virtual void Stop()
        {

        }

        public virtual Task SortAsync()
        {
            return Task.Run(() => Sort());
        }

        public virtual void Generate(int size)
        {

        }

        public virtual bool Save(string file_name)
        {
            if (file_name == null)
            {
                return false;
            }

            string str_heap = "";
            string str_sequence = "";

            data_synchronize.WaitOne();
            foreach(int element in Heap)
            {
                str_heap += "" + element + "\r";
            }
            foreach (int element in Sequence)
            {
                str_sequence += "" + element + "\r";
            }
            data_synchronize.Set();

            try
            {
                using (FileStream Stream = new FileStream(file_name + "_heap.txt", FileMode.Create))
                {
                    byte[] data = Encoding.ASCII.GetBytes(str_heap);
                    Stream.Write(data, 0, data.Length);
                }

                using (FileStream Stream = new FileStream(file_name + "_sequence.txt", FileMode.Create))
                {
                    byte[] data = Encoding.ASCII.GetBytes(str_sequence);
                    Stream.Write(data, 0, data.Length);
                }
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
        }

        protected virtual bool Convert(string str)
        {
            if (str != null && str.Length > 0)
            {
                string[] rows = str.Split('\r');

                if (rows != null && rows.Length > 0)
                {
                    List<int> Heap = new List<int>();
                    try
                    {
                        foreach (string row in rows)
                        {
                            Heap.Add(int.Parse(row));
                        }
                    }
                    catch
                    {

                    }

                    if (Heap.Count > 0)
                    {
                        data_synchronize.WaitOne();

                        this.Heap = Heap.ToArray();

                        data_synchronize.Set();

                        return true;
                    }                    
                }
            }
            return false;
        }

        public virtual bool Open(string file_name)
        {
            if (file_name == null)
            {
                return false;
            }

            byte[] data;
            string str;

            try
            {
                using (FileStream Stream = new FileStream(file_name, FileMode.Open))
                {
                    data = new byte[Stream.Length];
                    Stream.Read(data, 0, (int)Stream.Length);

                    str = System.Text.Encoding.UTF8.GetString(data);

                    return Convert(str);
                }
            }
            catch (Exception e)
            {

            }
            return false;
        }

        public virtual void Dispose()
        {
            thread?.Abort();
        }
    }
}
