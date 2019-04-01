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
    /// Interaction logic for RentMovie.xaml
    /// </summary>
    public partial class RentMovie : Window
    {
        private int id;
        private string movieName;
        private string returnDate;
        private string renterName;
        private int stockCount;
        private string connectionString;
        private MainUI main;

        public RentMovie(int id, string movieName, int stockCount, string connectionString, MainUI mw)
        {
            InitializeComponent();
            this.id = id;
            this.movieName = movieName;
            this.stockCount = stockCount - 1;

            this.connectionString = connectionString;
            this.main = mw;

            textBoxMovieID.Text = id.ToString();
            textBoxMovieName.Text = movieName;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            renterName = textBoxRenterName.Text;
            returnDate = textBoxReturnDate.Text;

            DatabaseTransactions.RentMovie(connectionString, renterName, id, returnDate, stockCount, movieName);
            main.LoadDataGrid(main.dataGridRents, "rents");
            main.LoadDataGrid(main.dataGridMovies, "movies");
            this.Close();
        }
    }
}
