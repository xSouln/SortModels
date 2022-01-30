using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SortModels.Templates
{
    public class TemplateContent : TemplateAdapter
    {
        public TemplateContent()
        {
            Element = new FrameworkElementFactory(typeof(ContentControl));

            Element.SetBinding(ContentControl.ContentProperty, new Binding { Path = new PropertyPath("Value") });

            this.Template = new DataTemplate { VisualTree = Element };
        }
    }
}
