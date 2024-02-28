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

    /// Логика взаимодействия для RegisterWindow.xaml

    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        public void UserRegisterButton_Click_1(object sender, RoutedEventArgs e)
        {
            //string connectionString = "Data Source=SERENYAPC;Initial Catalog=BaseStore_tusyuksa; Integrated Security=true";
            string connectionString = "Data Source=DBSRV\\GOR2023;Initial Catalog=BaseStore_tusyuksa; Integrated Security=true";
            
            string username = UsernameTextBox_1.Text;
            string email = EmailTextBox.Text;
            string phone = PhoneTextBox.Text;
            string password = PasswordBox.Text;

            bool userExists = CheckIfUserExists(username, connectionString);
            bool adminExists = CheckIfAdminExists(username, connectionString);

            if (userExists ^ adminExists)
            {
                MessageBox.Show("Логин " + username + " занят", "Профиль с введённым логином занят");
            }
            else
            {
                CreateUser(username, email, phone, password, connectionString);

                MessageBox.Show("Вы зарегистрировались как " + username, "Регистрация выполнена");

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
            }
        }

        public void UserLoginButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }

        public bool CheckIfUserExists(string username, string connectionString)
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
        public bool CheckIfAdminExists(string username, string connectionString)
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

        public void CreateUser(string username, string email, string phone, string password, string connectionString)
        {
            string query = "INSERT INTO Users (Username, Email, Phone, Password) VALUES (@Username, @Email, @Phone, @Password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Password", password);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
