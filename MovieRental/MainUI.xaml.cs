using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MovieRental

{
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
                buttonDeleteUser.IsEnabled = true;
            }

            LoadDataGrid(dataGridMovies, "movies");
            LoadDataGrid(dataGridUsers, "users");
            LoadDataGrid(dataGridSales, "sales");
            LoadDataGrid(dataGridRents, "rents");
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
            if (dataGridMovies.SelectedItem != null)
            {
                int movieID = Convert.ToInt32(((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[0]);
                DatabaseTransactions.DeleteMovie(connectionString, movieID);
                LoadDataGrid(dataGridMovies, "movies");
            }
            else
            {
                MessageBox.Show("Bir seçim yapın");
            }
        }

        private void buttonEditMovie_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMovies.SelectedItem != null)
            {
                Movies movie = new Movies();
                movie.MovieID = Convert.ToInt32(((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[0]);
                movie.MovieName = ((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[1].ToString();
                movie.Director = ((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[2].ToString();
                movie.ReleaseDate = Convert.ToInt32(((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[3]);
                movie.Imdb = Convert.ToDouble(((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[4]);
                movie.Category = ((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[5].ToString();
                movie.Duration = Convert.ToInt32(((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[6]);
                movie.StockCount = Convert.ToInt32(((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[7]);
                movie.Price = Convert.ToDouble(((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[8]);

                BookLogicUI bookLogicUI = new BookLogicUI(movie, connectionString, "edit", this);
                bookLogicUI.Show();
            }
            else
            {
                MessageBox.Show("Bir seçim yapın");
            }

        }

        void SetMoviesDataGrid()
        {

            dataGridMovies.Columns[0].Header = "ID";
            dataGridMovies.Columns[0].Width = 50;

            dataGridMovies.Columns[1].Header = "Movie Name";
            dataGridMovies.Columns[1].Width = 275;

            dataGridMovies.Columns[2].Header = "Director";
            dataGridMovies.Columns[2].Width = 250;

            dataGridMovies.Columns[3].Header = "Release Date";
            dataGridMovies.Columns[3].Width = 150;

            dataGridMovies.Columns[4].Header = "IMDB";
            dataGridMovies.Columns[4].Width = 120;

            dataGridMovies.Columns[5].Header = "Category";
            dataGridMovies.Columns[5].Width = 140;

            dataGridMovies.Columns[6].Header = "Duration";
            dataGridMovies.Columns[6].Width = 90;

            dataGridMovies.Columns[7].Header = "Stock Count";
            dataGridMovies.Columns[7].Width = 100;

            dataGridMovies.Columns[8].Header = "Price";
            dataGridMovies.Columns[8].Width = 60;
        }

        void SetUsersDataGrid()
        {
            dataGridUsers.Items.Refresh();
            dataGridUsers.Columns[0].Header = "IDddd";
            dataGridUsers.Columns[0].Width = 50;

            //dataGridMovies.Columns[1].Width = 50;
            //dataGridMovies.Columns[2].Width = 50;
            //dataGridMovies.Columns[3].Width = 50;
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

        public void LoadDataGrid(DataGrid grid, string type)
        {
            switch (type)
            {
                case "movies":
                    grid.ItemsSource = DatabaseTransactions.MovieQuery(connectionString).Tables[0].DefaultView;
                    break;
                case "users":
                    grid.ItemsSource = DatabaseTransactions.UserQuery(connectionString).Tables[0].DefaultView;
                    break;
                case "rents":
                    grid.ItemsSource = DatabaseTransactions.RentQuery(connectionString).Tables[0].DefaultView;
                    break;
                case "sales":
                    grid.ItemsSource = DatabaseTransactions.SaleQuery(connectionString).Tables[0].DefaultView;
                    break;
            }
            employeesTab.IsEnabled = true;
            grid.Items.Refresh();
            if (grid.IsLoaded)
            {
                SetMoviesDataGrid();
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetMoviesDataGrid();
        }

        private void ButtonRentMovie_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMovies.SelectedItem != null)
            {
                int movieID = Convert.ToInt32(((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[0]);
                string movieName = ((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[1].ToString();
                int stockCount = Convert.ToInt32(((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[7]);

                if (stockCount > 0)
                {
                    RentMovie rm = new RentMovie(movieID, movieName, stockCount, connectionString, this);
                    rm.Show();
                }
                else
                {
                    MessageBox.Show("It's out of stock!");
                }
            }
        }

        private void ButtonSellMovie_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMovies.SelectedItem != null)
            {
                int movieID = Convert.ToInt32(((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[0]);
                string movieName = ((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[1].ToString();
                int stockCount = Convert.ToInt32(((DataRowView)dataGridMovies.SelectedItem).Row.ItemArray[7]);
                stockCount -= 1;
                if (stockCount > 0)
                {
                    DatabaseTransactions.SellMovie(connectionString, movieID, stockCount, movieName);
                    LoadDataGrid(dataGridSales, "sales");
                    LoadDataGrid(dataGridMovies, "movies");
                }
                else
                {
                    MessageBox.Show("It's out of stock!");
                }
            }
        }

        private void ButtonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUsers.SelectedItem != null)
            {
                int userID = Convert.ToInt32(((DataRowView)dataGridUsers.SelectedItem).Row.ItemArray[0]);
                DatabaseTransactions.DeleteUser(connectionString, userID);
                LoadDataGrid(dataGridUsers, "users");
            }
            else
            {
                MessageBox.Show("Bir seçim yapın");
            }
        }
    }
}
