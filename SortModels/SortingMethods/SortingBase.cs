using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Threading;

namespace SortModels
{
    public abstract class SortingBase : NotifyPropertyChanged
    {
        protected Chart chart;
        protected Series series;

        protected Chart chart_heap;
        protected Series series_heap;

        protected string name = "base";
        protected int sorting_time = -1;

        protected AutoResetEvent data_synchronize = new AutoResetEvent(true);

        public SortingBase()
        {
            ChartInit();
            ChartHeapInit();
        }

        public virtual object GetSequence() => null;

        public virtual object GetHeap() => null;

        protected virtual void ChartInit()
        {
            chart = new Chart();
            chart.BackColor = System.Drawing.Color.FromName("#FF641818");
            chart.ForeColor = System.Drawing.Color.FromName("#FFDEC316");
            chart.BorderlineColor = System.Drawing.Color.FromName("#FFDEC316");

            series = new Series();
            series.Name = "main";
            series.ChartType = SeriesChartType.Point;

            var chartArea = new ChartArea { Name = "Area1" };
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
        }

        protected virtual void ChartHeapInit()
        {
            chart_heap = new Chart();
            chart_heap.BackColor = System.Drawing.Color.FromName("#FF641818");
            chart_heap.ForeColor = System.Drawing.Color.FromName("#FFDEC316");
            chart_heap.BorderlineColor = System.Drawing.Color.FromName("#FFDEC316");

            series_heap = new Series();
            series_heap.Name = "main";
            series_heap.ChartType = SeriesChartType.Point;

            var chartArea = new ChartArea { Name = "Area1" };
            chartArea.BackColor = System.Drawing.Color.FromName("#FF641818");

            chart_heap.ChartAreas.Add(chartArea);

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
        }

        public virtual Chart Chart => chart;

        public virtual Chart ChartHeap => chart_heap;

        public virtual Series Series => series;

        public virtual Series SeriesHeap => series_heap;

        public virtual string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public virtual int SortingTime
        {
            get => sorting_time;
            set
            {
                sorting_time = value;
                OnPropertyChanged(nameof(SortingTime));
            }
        }

        public virtual void UpdateCharts(DispatcherObject context)
        {

        }

        public virtual void Sort(object data)// where TElement : unmanaged, IConvertible
        {

        }

        public virtual void Generate(int size)
        {

        }
    }
}
