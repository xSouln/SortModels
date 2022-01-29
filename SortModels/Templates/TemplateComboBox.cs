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
    public class TemplateComboBox : TemplateAdapter
    {
        public TemplateComboBox()
        {
            var grid = new FrameworkElementFactory(typeof(Grid));
            var free = new FrameworkElementFactory(typeof(FrameworkElement));
            Element = new FrameworkElementFactory(typeof(ComboBox));

            grid.SetValue(Control.MarginProperty, new Thickness(0, 0, 0, 0));
            free.SetValue(FrameworkElement.VisibilityProperty, Visibility.Hidden);
            free.SetValue(FrameworkElement.WidthProperty, 200.0);

            Element.SetValue(Control.ForegroundProperty, UIProperty.GetBrush("#FF000000"));
            Element.SetValue(Control.BackgroundProperty, null);
            Element.SetValue(Control.BorderBrushProperty, null);

            Element.SetValue(Control.PaddingProperty, new Thickness(0, 0, 0, 0));
            Element.SetValue(Control.MarginProperty, new Thickness(0, 0, 0, 0));

            Element.SetValue(Control.HeightProperty, double.NaN);

            //Element.SetBinding(ComboBox.TextProperty, new Binding { Path = new PropertyPath("Value") });

            grid.AppendChild(free);
            grid.AppendChild(Element);

            this.Template = new DataTemplate { VisualTree = grid };
        }
    }
}
