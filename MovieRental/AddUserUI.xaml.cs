using System;
using System.Collections.Generic;
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

namespace MovieRental
{
    /// <summary>
    /// Interaction logic for AddUserUI.xaml
    /// </summary>
    public partial class AddUserUI : Window
    {
        private string connectionString;

        public AddUserUI(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            DatabaseTransactions.UyeEkle(connectionString, textBoxUsername.Text, textBoxPassword.Password, textBoxEmployeeName.Text);
            MessageBox.Show("Başarılı!");
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
