using System;
using System.Windows;
using System.Windows.Input;
using System.IO;

namespace LibraryAutomation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(textBoxID);
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            //String value = Security.Hash(textBoxPassword.Password);
            //Security.ValidatePassword(textBoxPassword.Password, value);
            string connectionString = Path.GetFullPath(@"..\..\") + "Database\\database.db";
        }
    }
}
