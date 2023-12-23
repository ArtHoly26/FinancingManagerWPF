using System.Windows;
using System;
using System.ComponentModel;
using System.Windows.Data;
using LiveCharts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using LiveCharts.Wpf;
using System.Collections.Generic;

namespace MyWindowProject
{
    public class UserViewModel: INotifyPropertyChanged
    {
        private UserData _user;
        private SeriesCollection pieSeriesCollection;
        private SeriesCollection incomeSeries;
        
        public UserData User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    OnPropertyChanged(nameof(User));
                }
            }
        }
        public SeriesCollection PieSeriesCollection
        {
            get { return pieSeriesCollection; }
            set
            {
                if (pieSeriesCollection != value)
                {
                    pieSeriesCollection = value;
                    OnPropertyChanged(nameof(PieSeriesCollection));
                }
            }
        }
        public SeriesCollection IncomeSeries
        {
            get { return incomeSeries; }
            set
            {
                if (incomeSeries != value)
                {
                    incomeSeries = value;
                    OnPropertyChanged(nameof(IncomeSeries));
                }
            }
        }
        public UserViewModel()
        {
            User = new UserData();      
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
