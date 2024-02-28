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
    /// Логика взаимодействия для MakingOrder.xaml
    public partial class MakingOrder : Window
    {
        public MakingOrder()
        {
            InitializeComponent();
        }


        private void UserRegisterButton_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дата доставки: " + DateTextBox.Text + ". Имя получателя: " + UsernameTextBox_2.Text + ". Ваш номер: " + PhoneTextBox_1.Text, "Ожидайте звонка от нашего менеджера");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
