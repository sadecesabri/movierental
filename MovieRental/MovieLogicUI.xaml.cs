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
    /// Interaction logic for BookLogicUI.xaml
    /// </summary>
    public partial class BookLogicUI : Window
    {
        MainUI main;
        private string connectionString;
        private string mode;
        public BookLogicUI(Movies movie, string connectionString, string mode, MainUI mw)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.mode = mode;
            this.main = mw;
            textBoxID.Text = movie.MovieID.ToString();
            textBoxMovieName.Text = movie.MovieName;
            textBoxDirector.Text = movie.Director;
            textBoxReleaseDate.Text = movie.ReleaseDate.ToString();
            textBoxIMDB.Text = movie.Imdb.ToString();
            textBoxCategory.Text = movie.Category;
            textBoxDuration.Text = movie.Duration.ToString();
            textBoxStockCount.Text = movie.StockCount.ToString();
            textBoxPrice.Text = movie.Price.ToString();
        }

        public BookLogicUI(string connectionString, string mode, MainUI mw)
        {
            this.main = mw;
            this.connectionString = connectionString;
            this.mode = mode;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                Movies movie = TextBox2Book();
                if (mode == "edit")
                {
                    movie.MovieID = Convert.ToInt32(textBoxID.Text);
                    DatabaseTransactions.EditMovie(connectionString, movie);
                    main.LoadDataGrid();
                    this.Close();
                }
                else if (mode == "new")
                {
                    DatabaseTransactions.AddMovie(connectionString, movie);
                    main.LoadDataGrid();
                    this.Close();
                }
            }
            else { }
        }

        private bool CheckInputs()
        {
            if (textBoxMovieName.Text != null && textBoxDirector.Text != null && textBoxReleaseDate.Text != null && textBoxCategory.Text != null && textBoxDuration.Text != null)
            {
                int tmp;
                bool date = Int32.TryParse(textBoxReleaseDate.Text, out tmp);
                bool page = Int32.TryParse(textBoxDuration.Text, out tmp);
                if (date && page)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Sayı girilmesi gereken yere karakter girildi");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Boş olmaması gereken yer boş bırakıldı");
                return false;
            }
        }

        private Movies TextBox2Book()
        {
            Movies movie = new Movies();
            movie.MovieName = textBoxMovieName.Text;
            movie.Director = textBoxDirector.Text;
            movie.ReleaseDate = Convert.ToInt32(textBoxReleaseDate.Text);
            movie.Imdb = Convert.ToDouble(textBoxIMDB.Text);
            movie.Category = textBoxCategory.Text;
            movie.Duration = Convert.ToInt32(textBoxDuration.Text);
            movie.StockCount = Convert.ToInt32(textBoxStockCount.Text);
            movie.Price = Convert.ToDouble(textBoxPrice);

            return movie;
        }
    }
}
