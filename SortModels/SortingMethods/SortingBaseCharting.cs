using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

namespace SortModels
{
    public abstract class SortingBaseCharting : SortingBase
    {
        public System.Windows.Forms.DataVisualization.Charting.Chart Chart { get; set; }
        public System.Windows.Forms.DataVisualization.Charting.Series Series { get; set; }

        public System.Windows.Forms.DataVisualization.Charting.Chart ChartHeap { get; set; }
        public System.Windows.Forms.DataVisualization.Charting.Series SeriesHeap { get; set; }

        public UIProperty<int> ChatingPoints { get; set; } = new UIProperty<int>(new Templates.TemplateTextBox()) { Name = "Chating points", Value = 500 };
        public UIProperty<int> StartChatingPoint { get; set; } = new UIProperty<int>(new Templates.TemplateTextBox()) { Name = "Start chating points", Value = 0 };

        public SortingBaseCharting()
        {
            base.Propertys.Add(StartChatingPoint);
            base.Propertys.Add(ChatingPoints);

            ChatingPoints.EventValueChanged += ChatingPointsChanged;
            StartChatingPoint.EventValueChanged += ChatingPointsChanged;
        }

        private void ChatingPointsChanged(UIProperty<int> arg)
        {
            UpdateTemplate();
        }

        public override void UpdateTemplate()
        {
            Series.Points.Clear();
            SeriesHeap.Points.Clear();

            if (Sequence != null)
            {
                data_synchronize.WaitOne();

                int i = StartChatingPoint.Value;
                int j = 0;
                while (i < Sequence.Length && i < Heap.Length && j < ChatingPoints.Value)
                {
                    Series.Points.AddXY(i, Sequence[i]);
                    SeriesHeap.Points.AddXY(i, Heap[i]);
                    i++;
                    j++;
                }

                data_synchronize.Set();
            }
        }

        protected override void TemplateInit()
        {
            WindowsFormsHost host1 = new WindowsFormsHost();
            WindowsFormsHost host2 = new WindowsFormsHost();

            host1.Width = double.NaN;
            host1.Margin = new Thickness(0, 0, 0, 0);
            host1.VerticalAlignment = VerticalAlignment.Stretch;
            host1.HorizontalAlignment = HorizontalAlignment.Stretch;

            host2.Width = double.NaN;
            host2.Margin = new Thickness(0, 0, 0, 0);
            host2.VerticalAlignment = VerticalAlignment.Stretch;
            host2.HorizontalAlignment = HorizontalAlignment.Stretch;

            Grid grid = new Grid();
            grid.Width = double.NaN;
            grid.Height = double.NaN;
            grid.Margin = new Thickness(0, 0, 0, 0);
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.HorizontalAlignment = HorizontalAlignment.Stretch;

            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            grid.RowDefinitions.Add(new RowDefinition());

            Grid.SetRow(host1, 0);
            Grid.SetColumn(host1, 0);

            Grid.SetRow(host2, 0);
            Grid.SetColumn(host2, 1);

            grid.Children.Add(host1);
            grid.Children.Add(host2);

            Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            Chart.BackColor = System.Drawing.Color.FromName("#FF641818");
            Chart.ForeColor = System.Drawing.Color.FromName("#FFDEC316");
            Chart.BorderlineColor = System.Drawing.Color.FromName("#FFDEC316");

            Series = new System.Windows.Forms.DataVisualization.Charting.Series();
            Series.Name = "main";
            Series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea { Name = "Area1" };
            chartArea.BackColor = System.Drawing.Color.FromName("#FF641818");

            Chart.ChartAreas.Add(chartArea);

            Chart.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Green;
            Chart.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.Green;

            Chart.ChartAreas[0].AxisX.MajorTickMark.LineColor = System.Drawing.Color.Green;
            Chart.ChartAreas[0].AxisY.MajorTickMark.LineColor = System.Drawing.Color.Green;

            Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Green;

            Chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Orange;
            Chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Orange;

            Series.Points.AddXY(0, 0);

            Chart.Series.Add(Series);
            host2.Child = Chart;

            ChartHeap = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ChartHeap.BackColor = System.Drawing.Color.FromName("#FF641818");
            ChartHeap.ForeColor = System.Drawing.Color.FromName("#FFDEC316");
            ChartHeap.BorderlineColor = System.Drawing.Color.FromName("#FFDEC316");

            SeriesHeap = new System.Windows.Forms.DataVisualization.Charting.Series();
            SeriesHeap.Name = "main";
            SeriesHeap.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            var chartArea_heap = new System.Windows.Forms.DataVisualization.Charting.ChartArea { Name = "Area1" };
            chartArea_heap.BackColor = System.Drawing.Color.FromName("#FF641818");

            ChartHeap.ChartAreas.Add(chartArea_heap);

            ChartHeap.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Green;
            ChartHeap.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.Green;

            ChartHeap.ChartAreas[0].AxisX.MajorTickMark.LineColor = System.Drawing.Color.Green;
            ChartHeap.ChartAreas[0].AxisY.MajorTickMark.LineColor = System.Drawing.Color.Green;

            ChartHeap.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            ChartHeap.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Green;

            ChartHeap.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Orange;
            ChartHeap.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Orange;

            SeriesHeap.Points.AddXY(0, 0);

            ChartHeap.Series.Add(SeriesHeap);
            host1.Child = ChartHeap;

            Container = grid;
        }
    }
}
