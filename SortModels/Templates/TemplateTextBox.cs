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
    public class TemplateTextBox : TemplateAdapter
    {
        public TemplateTextBox()
        {
            var grid = new FrameworkElementFactory(typeof(Grid));
            var free = new FrameworkElementFactory(typeof(FrameworkElement));
            Element = new FrameworkElementFactory(typeof(TextBox));

            grid.SetValue(Control.MarginProperty, new Thickness(0, 0, 0, 0));
            grid.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);

            free.SetValue(FrameworkElement.VisibilityProperty, Visibility.Hidden);
            free.SetValue(FrameworkElement.WidthProperty, 200.0);

            Element.SetValue(Control.FontSizeProperty, 18.0);
            Element.SetValue(Control.ForegroundProperty, UIProperty.GetBrush("#FFDEC316"));
            Element.SetValue(Control.BackgroundProperty, null);
            Element.SetValue(Control.BorderBrushProperty, UIProperty.GetBrush("#FF834545"));

            Element.SetValue(Control.PaddingProperty, new Thickness(-3));
            Element.SetValue(Control.MarginProperty, new Thickness(0, 0, 0, 0));

            Element.SetValue(Control.HeightProperty, double.NaN);
            Element.SetValue(Control.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);

            Element.SetValue(System.Windows.Controls.TextBox.CaretBrushProperty, UIProperty.GetBrush("#FFDEC316"));

            Element.SetBinding(System.Windows.Controls.TextBox.TextProperty, new Binding { Path = new PropertyPath("Value") });
            //element.SetBinding(Control.BackgroundProperty, new Binding { Path = new PropertyPath("BackgroundRequest") });

            grid.AppendChild(free);
            grid.AppendChild(Element);

            this.Template = new DataTemplate { VisualTree = grid };
        }
    }
}
