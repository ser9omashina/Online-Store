using StoreWpfApp1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using System.Data;

namespace StoreWpfApp1.View
{
    /// Логика взаимодействия для MainWindow.xaml

    public partial class MainWindow : Window
    {
        List<PcComponents> components = new List<PcComponents>();

        public ObservableCollection<PcComponents> filteredComponents = new ObservableCollection<PcComponents>();

        public ObservableCollection<PcComponents> addedComponents = new ObservableCollection<PcComponents>();

        public MainWindow()
        {            
            InitializeComponent();
            InitializeComponents();
            ListComponents.ItemsSource = components;
            ListAddedComponents.ItemsSource = addedComponents;
        }

        public void InitializeComponents()
        {
            components.Add(new PcComponents(1, "ASUS", "GTX 1060", "Видеокарта", 21000, 3));
            components.Add(new PcComponents(2, "COUGAR", "CG650", "Блок питания", 7000, 3));
            components.Add(new PcComponents(3, "MSI", "A320M-A PRO", "Материнская плата", 6000, 3));
            components.Add(new PcComponents(4, "ASRock", "A320M-DVS", "Материнская плата", 10000, 3));
            components.Add(new PcComponents(6, "AMD", "FX-4300", "Процессор", 6700, 3));
            components.Add(new PcComponents(7, "Intel", "G4400 OEM", "Процессор", 7700, 3));
            components.Add(new PcComponents(8, "AMD", "Athlon 300g OEM", "Процессор", 2000, 3));
            components.Add(new PcComponents(9, "Adata", "Ultimate SU630", "Жёсткий диск", 3000, 3));
            components.Add(new PcComponents(10, "DEEPCOOL", "Theta 15 PWM", "Кулер для процессора", 3000, 3));
            components.Add(new PcComponents(11, "AMD", "R334G1339", "Оперативная память", 4500, 3));
            components.Add(new PcComponents(12, "HP", "S650", "Жёсткий диск", 3000, 3));
            components.Add(new PcComponents(13, "ID-COLING", "DK-17 PWM", "Кулер для процессора", 2100, 3));
            components.Add(new PcComponents(14, "Aerocool", "HERO-775", "Блок питания", 4600, 3));
            components.Add(new PcComponents(16, "Patriot", "P210", "Жёсткий диск", 3700, 3));
            components.Add(new PcComponents(17, "MASHINIST", "HGD510 X79", "Материнская плата", 3300, 3));
            components.Add(new PcComponents(18, "HUANAN", "HDGH10 X99", "Материнская плата", 4300, 3));
            components.Add(new PcComponents(19, "Intel", "I3 2120", "Процессор", 4500, 3));
            components.Add(new PcComponents(20, "AMD", "FX-4400", "Процессор", 7000, 3));
            components.Add(new PcComponents(21, "AGI", "IO900", "Оперативная память", 6000, 3));
            components.Add(new PcComponents(22, "DEXP", "P210", "Жёсткий диск", 3000, 3));
            components.Add(new PcComponents(23, "Apacer", "ROI-960", "Жёсткий диск", 1700, 3));
            components.Add(new PcComponents(24, "DEXP", "RG-600", "Блок питания", 3200, 3));
            components.Add(new PcComponents(25, "Patriot", "P710", "Жёсткий диск", 3800, 3));
            components.Add(new PcComponents(26, "Silicon", "HGFH565", "Оперативная память",6000, 3));
            components.Add(new PcComponents(27, "XPG", "P210", "Оперативная память", 5000, 3));
        }

        public bool SearchComponent(ref PcComponents component)
        {
            foreach (var item in components)
            {
                if (item.Id == component.Id && item.Manufacturer == component.Manufacturer
                    && item.Title == component.Title && item.Type == component.Type)
                {
                    component = item;
                    return true;
                }
            }
            return false;
        }

        public void NewComponentClick(object sender, RoutedEventArgs e)
        {
            string manufacturer = NewComponentManufacturer.Text;
            string title = NewComponentTitle.Text;
            string type = NewComponentTitle.Text;
            bool idBool = int.TryParse(NewComponentId.Text, out int art);
            bool countBool = int.TryParse(NewComponentCountCopies.Text, out int count);
            bool priceBool = int.TryParse(NewComponentPrice.Text, out int price);

            if (!idBool || !countBool || !priceBool || manufacturer == string.Empty || title == string.Empty || type == string.Empty)
            {
                NewComponentId.Text = string.Empty;
                NewComponentTitle.Text = string.Empty;
                NewComponentManufacturer.Text = string.Empty;
                NewComponentType.Text = string.Empty;
                NewComponentPrice.Text = string.Empty;
                NewComponentCountCopies.Text = string.Empty;
                
            }
            else
            {
                PcComponents component = new PcComponents(art, manufacturer, title, type, price, count);
                if (SearchComponent(ref component))
                {
                    component.CountCopies += count;
                    MessageBox.Show("Количество введенного товара увеличено ", "Количество товара увеличено");
                }
                else
                {
                    components.Add(component);
                    MessageBox.Show("Введёный товар добавлен", "Товар добавлен");
                }
                ListComponents.Items.Refresh();
            }
        }

        public void UserLoginButton_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=DBSRV\\GOR2023;Initial Catalog=BaseStore_tusyuksa; Integrated Security=true";
            //string connectionString = "Data Source=SERENYAPC;Initial Catalog=BaseStore_tusyuksa; Integrated Security=true";

            string username = UsernameTextBox.Text;
            string password = PasswordBox.Text;

            bool userExists = CheckIfUserExists(username, connectionString);
            bool adminExists = CheckIfAdminExists(username, connectionString);
            bool right_password = CheckPassword(password, connectionString);

            if (adminExists)
            {
                if (userExists)
                {
                    if (right_password)
                    {
                        CatalogTabItem.IsSelected = true;
                        MessageBox.Show("Вы вошли как " + username, "Вход выполнен");
                        LoginButton.Visibility = Visibility.Collapsed;
                        DeleteAddedComponentButton.Visibility = Visibility.Visible;
                        ListAddedComponents.Visibility = Visibility.Visible;
                        BuyComponentButton.Visibility = Visibility.Visible;
                    }
                    else
                        MessageBox.Show("Вы ввели неверный пароль", "Неверный пароль");
                }
                if (right_password)
                {
                    MessageBox.Show("Вы вошли как админ " + username, "Вход выполнен");
                    CatalogTabItem.IsSelected = true;
                    AddComponentFromAdmin.Visibility = Visibility.Visible;
                    LoginButton.Visibility = Visibility.Collapsed;
                    MyComponents.Visibility = Visibility.Collapsed;
                    DeleteAddedComponentButton.Visibility = Visibility.Visible;
                    ListAddedComponents.Visibility = Visibility.Visible;
                    BuyComponentButton.Visibility = Visibility.Visible;

                }
                else
                    MessageBox.Show("Вы ввели неверный пароль", "Неверный пароль");
            }
            else
            {
                if (userExists)
                {
                    if (right_password)
                    {
                        CatalogTabItem.IsSelected = true;
                        MessageBox.Show("Вы вошли как " + username, "Вход выполнен");
                        LoginButton.Visibility = Visibility.Collapsed;
                        DeleteAddedComponentButton.Visibility = Visibility.Visible;
                        ListAddedComponents.Visibility = Visibility.Visible;
                        BuyComponentButton.Visibility = Visibility.Visible;
                    } 
                    else
                    {
                        MessageBox.Show("Вы ввели неверный пароль", "Неверный пароль");
                    }
                }

                else
                    MessageBox.Show("У вас нет аккауна", "Пройдите регистрацию");
            }

        }

        private bool CheckIfUserExists(string username, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private bool CheckIfAdminExists(string username, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Admins WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private bool CheckPassword(string password, string connectionString)
        {
            string query = "SELECT COUNT(*) FROM Admins WHERE Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Password", password);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public void ApplyFilter(string filterText)
        {
            filteredComponents.Clear();

            TextBlock searchBlock = (TextBlock)Choose.SelectedItem;

            foreach (var component in components)
            {                
                int id;
                if (int.TryParse(filterText, out id) && component.Id == id)
                    filteredComponents.Add(component);

                if (component.Manufacturer.Contains(filterText))
                    filteredComponents.Add(component);

                if (component.Title.Contains(filterText))
                    filteredComponents.Add(component);

                if (component.Type.Contains(filterText))
                    filteredComponents.Add(component);
            }
            ListComponents.ItemsSource = filteredComponents;
        }

        public void FilterButton_Click(object sender, RoutedEventArgs e)
        {                
            string filterText = FilterTextBox.Text;
            if (!string.IsNullOrEmpty(filterText))         
                ApplyFilter(filterText);

            else
                MessageBox.Show("Пожалуйста, введите текст для поска!");
        } 

        public void CancelFilterButton_Click(object sender, RoutedEventArgs e)
        {
            filteredComponents.Clear();

            foreach (var component in components)
                filteredComponents.Add(component);
        }

        public void AddComponentButton_Click(object sender, RoutedEventArgs e)
        {
            PcComponents selectedComponent = (PcComponents)ListComponents.SelectedItem;
            addedComponents.Add(selectedComponent);
            MessageBox.Show("Вы добавили товар в корзину");

        }

        public void DeleteAddedComponentButton_Click(object sender, RoutedEventArgs e)
        {
            PcComponents selectedComponent = (PcComponents)ListComponents.SelectedItem;
            if (selectedComponent != null)
            {
                addedComponents.Remove(selectedComponent);
                MessageBox.Show("Вы удалили товар из корзины");
            }
        }

        public void UserRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();

            this.Close();
        }

        private void BuyComponentButton_Click(object sender, RoutedEventArgs e)
        {
            PcComponents selectedComponent = (PcComponents)ListComponents.SelectedItem;
            if (selectedComponent != null)
            {
                addedComponents.Remove(selectedComponent);
                MakingOrder makingOrder= new MakingOrder();
                makingOrder.Show();

            }
        }
    }
}
