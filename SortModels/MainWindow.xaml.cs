using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortModels
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<UIProperty> Propertys { get; set; }
        public List<SortingBase> SortingMethods { get; set; }
        public UIProperty<int> ArraySize { get; set; } = new UIProperty<int>(new Templates.TemplateTextBox()) { Name = "Array size", Value = 100 };
        public UIProperty<SortingBase> SortingMethod { get; set; } = new UIProperty<SortingBase>(new Templates.TemplateComboBox()) { Name = "Sorting metod" };

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            SortingMethods = new List<SortingBase>
            {
                new SortingToPass(),
                new SortingBubble()
            };

            Propertys = new ObservableCollection<UIProperty>
            {
                SortingMethod,
                ArraySize
            };

            SortingMethod.TemplateAdapter.Element.SetValue(ComboBox.DisplayMemberPathProperty, "Name.Value");
            SortingMethod.TemplateAdapter.Element.SetValue(ComboBox.ItemsSourceProperty, SortingMethods);         
            SortingMethod.TemplateAdapter.Element.SetBinding(ComboBox.SelectedValueProperty, new Binding { Path = new PropertyPath("Value") });
            
            SortingMethod.EventValueChanged = SelectedMetodChanged;
            SortingMethod.Value = SortingMethods[0];
        }

        private void SelectedMetodChanged(UIProperty<SortingBase> arg)
        {
            if (arg.Value != null && arg.Value.Container != null)
            {
                GridTemplate.Children.Clear();
                GridTemplate.Children.Add(arg.Value.Container);
            }
        }

        public byte[] GenerateArray(int size)
        {
            List<byte> vector = new List<byte>();
            Random random = new Random();

            while (size > 0)
            {
                vector.Add((byte)random.Next(0, 0xff));
                size--;
            }

            return vector.ToArray();
        }

        private void ButGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (SortingMethod.Value != null)
            {
                SortingMethod.Value.Generate(ArraySize.Value);
                SortingMethod.Value.Sort();
                SortingMethod.Value.UpdateTemplate(this);
            }
        }
    }
}
