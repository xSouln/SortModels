using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        private SortingBase sorting = new SortingToPass();
        private SortingBase sorting_bubble = new SortingBubble();

        public List<SortingBase> Sortings { get; set; }

        public int ArraySize { get; set; } = 1000;

        public SortingBase SelectedSorting { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            //LabelSortingTime.DataContext = sorting;
            ComboBoxSelectedSorting.SelectedValue = sorting;

            Sortings = new List<SortingBase>
            {
                sorting,
                sorting_bubble
            };

            //Loaded += WindowLoaded;
        }
        /*
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Host1.Child = sorting?.ChartHeap;
            Host2.Child = sorting?.Chart;
        }
        */
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
            //byte[] in_data = GenerateArray(ArraySize);

            SelectedSorting?.Generate(ArraySize);
            SelectedSorting?.Sort();
            //sorting?.Sort(in_data);
            //sorting?.Generate(in_data);

            SelectedSorting.UpdateTemplate(this);
        }

        private void ComboBoxSelectedSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxSelectedSorting.SelectedValue != null && ComboBoxSelectedSorting.SelectedValue is SortingBase sorting)
            {
                Host1.Child = sorting.ChartHeap;
                Host2.Child = sorting.Chart;
                SelectedSorting = sorting;
            }
        }
    }
}
