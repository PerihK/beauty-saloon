using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using beauty_saloon.Pages;
using System.Windows.Shapes;
using System.Data.Services.Client;
using System.Windows.Threading;

namespace beauty_saloon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {//Основная страница подсистемы
     DispatcherTimer timer;
        public static beauty_saloonEntities5 DataEntitiesEmployee { get; set; }
        public bool isDirty { get; set; }
        ObservableCollection<Service> ListEmployee;
        public MainWindow()
        {//принимать сервис
            DataEntitiesEmployee = new beauty_saloonEntities5();
            InitializeComponent();
            ListEmployee = new ObservableCollection<Service>();
            GetEmployees();
            SalonList.SelectedIndex = 0;
            Countfull.Text = SalonList.Items.Count.ToString();
        }
        private void GetEmployees()
        {
            var employees = DataEntitiesEmployee.Services;
            var queryEmployee = from employee in employees
                                orderby employee.Title
                                select employee;
            foreach (Service emp in queryEmployee)
            {
                ListEmployee.Add(emp);
            }
            SalonList.ItemsSource = ListEmployee;
        }

        private void serviceClick(object sender, RoutedEventArgs e)
        {
            Service emp = SalonList.SelectedItem as Service;
            if (emp != null)
            {
            DataEntitiesEmployee.SaveChanges();
            ClientWin clientw = new ClientWin(emp);
            clientw.Owner = this;
            clientw.Show();
            }
           
        }

        private void AddService(object sender, RoutedEventArgs e)
        {
            Service employee = Service.CreateEmp("Название", 1111, 600, null, null, "изображение");
            try
            {
                DataEntitiesEmployee.Services.Add(employee);
                ListEmployee.Add(employee);
                isDirty = false;
                DataEntitiesEmployee.SaveChanges();
            }
            catch (DataServiceRequestException ex)
            {
                throw new ApplicationException("Ошибка добавления" + ex.ToString());
            }
            SalonList.Items.Refresh();
        }
        private void DeleteCommandBinding_Executed()//функция удаления записи
        {
            Service emp = SalonList.SelectedItem as Service;
            if (emp != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить улсугу: " + emp.Title.Trim() + " за "
                    + emp.DurationInSeconds.ToString() + "секунд и " + emp.Cost+" рублей ", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesEmployee.Services.Remove(emp);
                    SalonList.SelectedIndex = SalonList.SelectedIndex == 0 ? 1 : SalonList.SelectedIndex - 1;
                    ListEmployee.Remove(emp);
                    DataEntitiesEmployee.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Выберите строку для удалния");
                }
            }
        }
        private void redakt(object sender, RoutedEventArgs e)//функция редактирования выбранной записи
        {
            ClientService cmp = new ClientService();
            Service emp = SalonList.SelectedItem as Service;
            if (emp != null)
            {
                MessageBoxResult result = MessageBox.Show("Изменить услугу: " + emp.Title + " за "
                    + emp.DurationInSeconds.ToString() + "секунд и " + emp.Cost + " рублей ", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesEmployee.SaveChanges();
                    FormaRedakt formaRedakt = new FormaRedakt(emp);
                    formaRedakt.Owner = this;
                    formaRedakt.Show();
                    
                }
                else
                {
                    MessageBox.Show("Выберите строку для удаления");
                }
            }
        }
        private void RewriteEmployee()
        {
            DataEntitiesEmployee = new beauty_saloonEntities5();
            ListEmployee.Clear();
            GetEmployees();
        }

        private void Delete(object sender, RoutedEventArgs e)//кнопка удаления записи с функцией удаления
        {
            DeleteCommandBinding_Executed();
        }
        private void Refresh_Executed()
        {
            RewriteEmployee();
          
            isDirty = true;
        }
        private void refresh(object sender, RoutedEventArgs e)
        {
            Refresh_Executed();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textadm.Text == "0000")
            {
                
                (sender as FrameworkElement).Style = (Style)Resources["hidden"];
            }
        }

        private void AdminGo(object sender, RoutedEventArgs e)//кнопка перехода на страницу ближайших записей
        {
            AdminWin adminWin = new AdminWin();
            adminWin.Show();
        }
        private void poisk()
        {
            var find = DataEntitiesEmployee.Services.ToList();
          if (filtr.SelectedIndex == 0)
            {
                find = find.OrderByDescending(p => p.Cost).ToList();
            }
            if (filtr.SelectedIndex == 1)
            {
                find = find.OrderBy(p => p.Cost).ToList();
            }
            if (filtr.SelectedIndex == 2)
            {
                find = find.OrderBy(p => p.DurationInSeconds).ToList();
            }
            if (filtr.SelectedIndex == 3)
            {
                find = find.OrderBy(p => p.Discount).ToList();
            }
            SalonList.ItemsSource = find;
        }

        private void findservice(object sender, TextChangedEventArgs e)
        {
            string finde = findbox.Text;
            DataEntitiesEmployee = new beauty_saloonEntities5();
            ListEmployee.Clear();
            var employees = DataEntitiesEmployee.Services;
            var queryEmployee = from employee in employees
                                where employee.Title.StartsWith(finde)
                                select employee;
            foreach (Service emp in queryEmployee)
            {
                SalonList.Items.Refresh();
                Count.Text = SalonList.Items.Count.ToString();
                ListEmployee.Add(emp);
            }

            if (ListEmployee.Count > 0)
               
            { 
                SalonList.ItemsSource = ListEmployee;
            }
            else if (finde == "")
            {
                GetEmployees();
            }
        }
    }
}
