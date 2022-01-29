using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public abstract class SortingBase : TemplateAdapter, INotifyPropertyChanged
    {
        public ObservableCollection<UIProperty> Propertys { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public UIProperty<string> Name { get; set; } = new UIProperty<string> { Name = "Metod", Value = "base" };
        public UIProperty<int> SortingTime { get; set; } = new UIProperty<int> { Name = "Sorting time" };

        protected AutoResetEvent data_synchronize = new AutoResetEvent(true);

        public SortingBase()
        {
            TemplateInit();

            Propertys = new ObservableCollection<UIProperty>
            {
                Name,
                SortingTime
            };
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

        }

        public virtual void Sort(object data)
        {

        }

        public virtual void Sort()
        {

        }

        public virtual void Generate(int size)
        {

        }
    }
}
