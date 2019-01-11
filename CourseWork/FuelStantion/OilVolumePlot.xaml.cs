using System.Linq;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace FuelStantion
{
    /// <summary>
    /// Interaction logic for OilVolumePlot.xaml
    /// </summary>
    public partial class OilVolumePlot : UserControl
    {
        public OilVolumePlot()
        {
            InitializeComponent();
            FillChart();
            DataContext = this;
        }

        void FillChart()
        {
            var fuelTypes = MyApplication.Instance.GetFuelStorageViewModel().FuelTypes;

            foreach (var fuelType in fuelTypes)
            {
                double fuelVolume = MyApplication.Instance.GetFuelStorageViewModel().DBConnection.FuelRefillings.Where(
                    r => r.Fuel_type_id == fuelType.id)
                    .Sum(r => r.Volume);

                PieSeries series = new PieSeries();
                chart.Series.Add(series);
                series.Values = new ChartValues<double> { fuelVolume };
                series.Title = fuelType.Name;
                series.DataLabels = true;
            }
        }
    }
}
