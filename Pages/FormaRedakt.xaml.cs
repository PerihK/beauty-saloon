using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using beauty_saloon.Pages;

namespace beauty_saloon.Pages
{
    /// <summary>
    /// Логика взаимодействия для FormaRedakt.xaml
    /// </summary>
    public partial class FormaRedakt : Window
    {
        public static beauty_saloonEntities5 DataEntitiesEmployee { get; set; }
        public bool isDirty { get; set; }

        ObservableCollection<Service> ListEmployee;
        public FormaRedakt(Service emp)
        {
            DataEntitiesEmployee = new beauty_saloonEntities5();
            ObservableCollection<Service> ListEmployee;
            
            InitializeComponent();
            ListEmployee = new ObservableCollection<Service>();
            titleText.Text = emp.Title;
            costtext.Text = Convert.ToString(emp.Cost);
            sectext.Text=Convert.ToString(emp.DurationInSeconds);
            desktext.Text = emp.Description;
            disctext.Text=emp.Discount.ToString();
            imagetext.Text = emp.MainImagePath;     
        }
      
        private void Save()
        {MainWindow mainWindow = (MainWindow)this.Owner;

            Service emp = mainWindow.SalonList.SelectedItem as Service;
            emp.Title = titleText.Text;
            emp.Cost = Convert.ToDecimal(costtext.Text);
            if(emp.DurationInSeconds <= 14400 && emp.DurationInSeconds > 0)
            {
                emp.DurationInSeconds = Convert.ToInt32(sectext.Text);
            }
            else
            {
                MessageBox.Show("Время указано неверно!");
            }
            emp.Description = desktext.Text;
            emp.MainImagePath = imagetext.Text;
            DataEntitiesEmployee.SaveChanges();
            mainWindow.SalonList.Items.Refresh();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Save();
        }
    }
}
