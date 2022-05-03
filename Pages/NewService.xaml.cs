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

namespace beauty_saloon.Pages
{
    /// <summary>
    /// Логика взаимодействия для NewService.xaml
    /// </summary>
    public partial class NewService : Window
    {
        public static beauty_saloonEntities5 DataEntitiesEmployee { get; set; }
        public bool isDirty { get; set; }

        ObservableCollection<Service> ListBase;
        public NewService()
        {
            InitializeComponent();
            ListBase = new ObservableCollection<Service>();
            DataEntitiesEmployee = new beauty_saloonEntities5();
            GetEmployees();
            DataGridEmployee.SelectedIndex = 0;
        }

        private void GetEmployees()
        {
            var employees = DataEntitiesEmployee.Services;
            var queryEmployee = from employee in employees
                                orderby employee.Title
                                select employee;
            foreach (Service emp in queryEmployee)
            {
                ListBase.Add(emp);
            }
            DataGridEmployee.ItemsSource = ListBase;
        }
        private void RewriteEmployee()
        {
            DataEntitiesEmployee = new beauty_saloonEntities5();
            ListBase.Clear();
            GetEmployees();
        }
        private void SaveCommandBinding_Executed()
        {
            DataEntitiesEmployee.SaveChanges();
            isDirty = true;
            DataGridEmployee.IsReadOnly = true;
        }
        private void Refresh_Executed()
        {
            RewriteEmployee();
            DataGridEmployee.IsReadOnly = false;
            isDirty = true;
        }
        private void addservice(object sender, RoutedEventArgs e)
        {
            Service employee = Service.CreateEmp("Название", 1111, 600, null, null, "изображение");
            try
            {
                DataEntitiesEmployee.Services.Add(employee);
                ListBase.Add(employee);
                isDirty = false;
            }
            catch (DataServiceRequestException ex)
            {
                throw new ApplicationException("Ошибка добавления" + ex.ToString());
            }

        }
          private void EditCommandBinding_Executed()
        {
            DataGridEmployee.IsReadOnly = false;
            isDirty = false;
            DataGridEmployee.BeginEdit();
        }
        private void refresh(object sender, RoutedEventArgs e)
        {
            Refresh_Executed();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            SaveCommandBinding_Executed();
        }
    }
}
