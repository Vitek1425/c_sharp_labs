using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ContactList
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ContactViewModel();
        }

        private void DG1_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "Id":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "BirthDate":
                    e.Column.IsReadOnly = true;
                    DataGridTextColumn dgtc = e.Column as DataGridTextColumn;
                    DateTimeToDateConverter con = new DateTimeToDateConverter();
                    (dgtc.Binding as Binding).Converter = con;
                    break;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
