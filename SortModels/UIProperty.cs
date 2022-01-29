using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static SortModels.UIProperty;

namespace SortModels
{
    public abstract class UIProperty : NotifyPropertyChanged
    {
        public delegate void Event<TArgument>(TArgument arg);

        protected string name = "";

        protected object _value;
        protected Brush background;
        protected TemplateAdapter template_adapter;

        protected object event_selection;
        protected object event_value_changed;

        public UIProperty()
        {

        }

        public UIProperty(TemplateAdapter adapter)
        {
            TemplateAdapter = adapter;
        }

        public static Brush GetBrush(string request)
        {
            Brush brush = null;
            try { brush = (Brush)new BrushConverter().ConvertFrom(request); }
            catch { }
            return brush;
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public TemplateAdapter TemplateAdapter
        {
            get => template_adapter;
            set
            {
                template_adapter = value;
                OnPropertyChanged(nameof(TemplateAdapter));
            }
        }

        protected virtual void UpdateValue()
        {

        }

        public virtual object GetValue() => _value;

        public virtual void SetValue(object request)
        {
            if (_value == null)
            {
                _value = request;
                UpdateValue();
                return;
            }

            if (request != null && _value.GetType() == request.GetType())
            {
                try
                {
                    if (Comparer<object>.Default.Compare(_value, request) == 0) { return; }
                }
                catch { }
                _value = request;
                UpdateValue();
            }
        }

        public Brush Background
        {
            get => background;
            set
            {
                background = value;
                OnPropertyChanged(nameof(Background));
            }            
        }

        public virtual void Select()
        {

        }
    }

    public class UIProperty<TValue> : UIProperty //where TValue : IComparable
    {
        public Event<UIProperty<TValue>> EventValueChanged;

        public UIProperty() : base()
        {
            _value = default(TValue);
        }

        public UIProperty(TemplateAdapter adapter) : base(adapter)
        {
            _value = default(TValue);
        }

        protected override void UpdateValue()
        {
            OnPropertyChanged(nameof(Value));
            EventValueChanged?.Invoke(this);
        }

        public TValue Value
        {
            get => _value != null ? (TValue)_value : default;
            set
            {
                try { if (Comparer<object>.Default.Compare(_value, value) == 0) { return; } }
                catch { }

                _value = value;
                UpdateValue();
            }
        }
    }
}
