using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
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
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : Window
    {
        private string connectionString;

        public MainUI(string conString, int userType)
        {
            connectionString = conString;
            InitializeComponent();
            if(userType == 0)
            {
                buttonAddUser.IsEnabled = true;
            }

            LoadDataGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void buttonAddMovie_Click(object sender, RoutedEventArgs e)
        {
                BookLogicUI bookLogicUI = new BookLogicUI(connectionString, "new", this);
            bookLogicUI.Show();
        }

        private void buttonDeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                int movieID = Convert.ToInt32(((DataRowView)dataGrid.SelectedItem).Row.ItemArray[0]);
                DatabaseTransactions.DeleteMovie(connectionString, movieID);
                LoadDataGrid();
            }
            else
            {
                MessageBox.Show("Bir seçim yapın");
            }
        }

        private void buttonEditMovie_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Movies movie = new Movies();
                movie.MovieID = Convert.ToInt32(((DataRowView)dataGrid.SelectedItem).Row.ItemArray[0]);
                movie.MovieName = ((DataRowView)dataGrid.SelectedItem).Row.ItemArray[1].ToString();
                movie.Director = ((DataRowView)dataGrid.SelectedItem).Row.ItemArray[2].ToString();
                movie.ReleaseDate = Convert.ToInt32(((DataRowView)dataGrid.SelectedItem).Row.ItemArray[3]);
                movie.Imdb = Convert.ToDouble(((DataRowView)dataGrid.SelectedItem).Row.ItemArray[4]);
                movie.Category = ((DataRowView)dataGrid.SelectedItem).Row.ItemArray[5].ToString(); ;
                movie.Duration = Convert.ToInt32(((DataRowView)dataGrid.SelectedItem).Row.ItemArray[6]);
                movie.StockCount = Convert.ToInt32(((DataRowView)dataGrid.SelectedItem).Row.ItemArray[7]);
                movie.Price = Convert.ToDouble(((DataRowView)dataGrid.SelectedItem).Row.ItemArray[8]);

                BookLogicUI bookLogicUI = new BookLogicUI(movie, connectionString, "edit", this);
                bookLogicUI.Show();
            }
            else
            {
                MessageBox.Show("Bir seçim yapın");
            }

        }

        void SetDataGrid()
        {


            //var style = new Style(typeof(System.Windows.Controls.Primitives
            //    .DataGridColumnHeader));
            //style.Setters.Add(new Setter
            //{
            //    Property = BackgroundProperty,
            //    Value = Brushes.Yellow
            //});


            //dataGrid.Columns[0].HeaderStyle = style;



            dataGrid.Columns[0].Header = "ID";
            dataGrid.Columns[0].Width = 50;

            dataGrid.Columns[1].Header = "Movie Name";
            dataGrid.Columns[1].Width = 275;

            dataGrid.Columns[2].Header = "Director";
            dataGrid.Columns[2].Width = 250;

            dataGrid.Columns[3].Header = "Release Date";
            dataGrid.Columns[3].Width = 150;

            dataGrid.Columns[4].Header = "IMDB";
            dataGrid.Columns[4].Width = 120;

            dataGrid.Columns[5].Header = "Category";
            dataGrid.Columns[5].Width = 140;

            dataGrid.Columns[6].Header = "Duration";
            dataGrid.Columns[6].Width = 90;

            dataGrid.Columns[7].Header = "Stock Count";
            dataGrid.Columns[7].Width = 100;

            dataGrid.Columns[8].Header = "Price";
            dataGrid.Columns[8].Width = 60;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void buttonAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUserUI addUserUI = new AddUserUI(connectionString);
            addUserUI.Show();
        }

        public void LoadDataGrid()
        {
            dataGrid.ItemsSource = DatabaseTransactions.MovieQuery(connectionString).Tables[0].DefaultView;
            dataGrid.Items.Refresh();
            if (this.IsLoaded)
            {
                SetDataGrid();
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetDataGrid();
        }
    }
}
