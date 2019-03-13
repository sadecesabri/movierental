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
            textBoxBookName.Text = movie.MovieName;
            textBoxAuthor.Text = movie.Director;
            textBoxPublishDate.Text = movie.ReleaseDate.ToString();
            textBoxTranslater.Text = movie.Imdb.ToString();
            textBoxCategory.Text = movie.Category;
            textBoxPage.Text = movie.Duration.ToString();
            textBoxBorrowedBy.Text = movie.StockCount.ToString();
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
            if (textBoxBookName.Text != null && textBoxAuthor.Text != null && textBoxPublishDate.Text != null && textBoxCategory.Text != null && textBoxPage.Text != null)
            {
                int tmp;
                bool date = Int32.TryParse(textBoxPublishDate.Text, out tmp);
                bool page = Int32.TryParse(textBoxPage.Text, out tmp);
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
            movie.MovieName = textBoxBookName.Text;
            movie.Director = textBoxAuthor.Text;
            movie.ReleaseDate = Convert.ToInt32(textBoxPublishDate.Text);
            movie.Imdb = Convert.ToInt32(textBoxTranslater.Text);
            movie.Category = textBoxCategory.Text;
            movie.Duration = Convert.ToInt32(textBoxPage.Text);
            movie.StockCount = Convert.ToInt32(textBoxBorrowedBy.Text);

            return movie;
        }
    }
}
