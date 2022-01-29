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
        public System.Windows.Forms.DataVisualization.Charting.Chart chart;
        public System.Windows.Forms.DataVisualization.Charting.Series series;

        public System.Windows.Forms.DataVisualization.Charting.Chart chart_heap;
        public System.Windows.Forms.DataVisualization.Charting.Series series_heap;

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

            chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart.BackColor = System.Drawing.Color.FromName("#FF641818");
            chart.ForeColor = System.Drawing.Color.FromName("#FFDEC316");
            chart.BorderlineColor = System.Drawing.Color.FromName("#FFDEC316");

            series = new System.Windows.Forms.DataVisualization.Charting.Series();
            series.Name = "main";
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            var chartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea { Name = "Area1" };
            chartArea.BackColor = System.Drawing.Color.FromName("#FF641818");

            chart.ChartAreas.Add(chartArea);

            chart.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Green;
            chart.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.Green;

            chart.ChartAreas[0].AxisX.MajorTickMark.LineColor = System.Drawing.Color.Green;
            chart.ChartAreas[0].AxisY.MajorTickMark.LineColor = System.Drawing.Color.Green;

            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Green;

            chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Orange;
            chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Orange;

            series.Points.AddXY(0, 0);

            chart.Series.Add(series);
            host2.Child = chart;

            chart_heap = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chart_heap.BackColor = System.Drawing.Color.FromName("#FF641818");
            chart_heap.ForeColor = System.Drawing.Color.FromName("#FFDEC316");
            chart_heap.BorderlineColor = System.Drawing.Color.FromName("#FFDEC316");

            series_heap = new System.Windows.Forms.DataVisualization.Charting.Series();
            series_heap.Name = "main";
            series_heap.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            var chartArea_heap = new System.Windows.Forms.DataVisualization.Charting.ChartArea { Name = "Area1" };
            chartArea_heap.BackColor = System.Drawing.Color.FromName("#FF641818");

            chart_heap.ChartAreas.Add(chartArea_heap);

            chart_heap.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.Green;
            chart_heap.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.Green;

            chart_heap.ChartAreas[0].AxisX.MajorTickMark.LineColor = System.Drawing.Color.Green;
            chart_heap.ChartAreas[0].AxisY.MajorTickMark.LineColor = System.Drawing.Color.Green;

            chart_heap.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Transparent;
            chart_heap.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Green;

            chart_heap.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.Orange;
            chart_heap.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.Orange;

            series_heap.Points.AddXY(0, 0);

            chart_heap.Series.Add(series_heap);
            host1.Child = chart_heap;

            Container = grid;
        }
    }
}
