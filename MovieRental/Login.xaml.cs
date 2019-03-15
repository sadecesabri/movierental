using System;
using System.Windows;
using System.Windows.Input;
using System.IO;

namespace MovieRental

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(textBoxID);
            //textBoxID.Text = Security.Hash("admin");
        }
        
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "data source = " + Path.GetFullPath(@"..\..\") + "Database\\database.db";
            string[] value = DatabaseTransactions.SifreKontrol(connectionString, textBoxID.Text);

            if (!string.IsNullOrEmpty(value[0]) && Security.ValidatePassword(textBoxPassword.Password, value[0]))
            {
                MainUI win = new MainUI(connectionString, Convert.ToInt32(value[1]));
                win.Show();
                this.Hide();                
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                buttonLogin_Click(sender, e);
            }
        }
    }
}
