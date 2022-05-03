using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace beauty_saloon.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminWin.xaml
    /// </summary>
    public partial class AdminWin : Window
    {
        public static beauty_saloonEntities5 DataEntitiesEmployee { get; set; }
        public bool isDirty { get; set; }

        ObservableCollection<ClientService> ListEmployee;
        DispatcherTimer timer;
        int timerCounter = 0;
            
        public AdminWin()
        {
            DataEntitiesEmployee = new beauty_saloonEntities5();    
            InitializeComponent();
            timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 30) };//таймиер обновления страницы
            timer.Tick += new EventHandler(refreshpage);
            timer.Start();
            ListEmployee = new ObservableCollection<ClientService>();
            GetEmployees();
            AdminList.SelectedIndex = 0;
            
        }
        void refreshpage(object sender, EventArgs e)//функция обновления страницы каждые 30 секунд
        {
            AdminList.Items.Refresh();
        }
        private void GetEmployees()
        {
            var employees = DataEntitiesEmployee.ClientServices;
            var queryEmployee = from employee in employees
                                orderby employee.StartTime
                                select employee;
            foreach (ClientService emp in queryEmployee)
            {
                ListEmployee.Add(emp);
            }
            AdminList.ItemsSource = ListEmployee;
        }
    }
}
