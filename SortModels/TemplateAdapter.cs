using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SortModels
{
    public abstract class TemplateAdapter
    {
        protected FrameworkElement container;

        public virtual FrameworkElementFactory Element { get; set; }

        public virtual FrameworkElement Container
        {
            get => container;
            set => container = value;
        }

        public virtual DataTemplate Template { get; set; }
    }
}
