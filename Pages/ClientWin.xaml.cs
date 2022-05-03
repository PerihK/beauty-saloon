using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
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
using System.Windows.Shapes;
using beauty_saloon.model;

namespace beauty_saloon.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientWin.xaml
    /// </summary>
    public partial class ClientWin : Window
    {
        ObservableCollection<ClientService> ClientBase;
        public static beauty_saloonEntities5 DataEntitiesEmployee { get; set; }
        public bool isDirty { get; set; }

        private Service svr;


        public ClientWin(Service emp)
        {
            DataEntitiesEmployee = new beauty_saloonEntities5();
            ObservableCollection<Service> ListEmployee;
            InitializeComponent();
            ClientBase = new ObservableCollection<ClientService>();
            ListEmployee = new ObservableCollection<Service>();
            titleText.Text = emp.Title;
            sectext.Text = emp.DurationInSeconds.ToString();
     
            svr = emp;

        }
        static ClientService emp;
        private void AddClient(object sender, RoutedEventArgs e)
        {
  
            ClientService employee = ClientService.CreateEmp(Convert.ToInt32((fiocombo.SelectedItem as Client).ID),svr.ID,Convert.ToDateTime(datapick.Text), opistext.Text);
            try
            {
                DataEntitiesEmployee.ClientServices.Add(employee);
                ClientBase.Add(employee);

                isDirty = false;
            }
            catch (DataServiceRequestException ex)
            {
                throw new ApplicationException("Ошибка добавления" + ex.ToString());
            }
            DataEntitiesEmployee.SaveChanges();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)this.Owner;
            ClientService emp = mainWindow.SalonList.SelectedItem as ClientService;
            emp.Service.ID = Convert.ToInt32(titleText.Text);
            DataEntitiesEmployee.SaveChanges();
        }
    }
}
