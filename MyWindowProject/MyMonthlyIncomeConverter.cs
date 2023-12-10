using LiveCharts.Defaults;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts.Wpf;

namespace MyWindowProject
{
    public class MyMonthlyIncomeConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ChartValues<MonthIncome> monthlyIncomes)
            {
                var columnSeries = new ChartValues<ColumnSeries>();

                foreach (var income in monthlyIncomes)
                {
                    columnSeries.Add(new ColumnSeries
                    {
                        Title = income.Month,
                        Values = new ChartValues<decimal> { income.Income }
                    });
                }

                return columnSeries;
            }

            return null;
        }
    }
}
