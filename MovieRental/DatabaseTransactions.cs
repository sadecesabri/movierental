using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental
{
    public static class DatabaseTransactions
    {
        public static void AddMovie(string connectionString, Movies movie)
        {     
            string query = "INSERT INTO movies (movie_name, director, release_date, imdb, category, duration, stock_count, price) VALUES(@movie_name, @director, @release_date, @imdb, @category, @duration, @stock_count, @price);";
            //INSERT INTO books (book_name, author, publish_date, translater, category, Page, borrowed_by) VALUES('aa', 'aa', 1, 'a', 'a', 1, 'a');

            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();
                SQLiteCommand command = new SQLiteCommand(con);
                command.CommandText = query;
                command.Parameters.AddWithValue("@book_name", movie.MovieName);
                command.Parameters.AddWithValue("@author", movie.Director);
                command.Parameters.AddWithValue("@publish_date", movie.ReleaseDate);
                command.Parameters.AddWithValue("@translater", movie.Imdb);
                command.Parameters.AddWithValue("@category", movie.Category);
                command.Parameters.AddWithValue("@Page", movie.Duration);
                command.Parameters.AddWithValue("@borrowed_by", movie.StockCount);
                command.Parameters.AddWithValue("@price", movie.Price);

                command.ExecuteNonQuery();    
            }
        }

        public static void DeleteMovie(string connectionString, int id)
        {
            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();
                string query = "DELETE FROM movies WHERE Id = '" + id + "'";
                using (SQLiteCommand command = new SQLiteCommand(query, con))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void EditMovie(string connectionString, Movies movie)
        {
            string query = "UPDATE movies SET movie_name = @MovieName, director = @Director, release_date = @ReleaseDate, imdb = @IMDB, category = @Category, duration = @Duration, stock_count = @StockCount, price = @Price WHERE Id = @ID;";

            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();
                SQLiteCommand command = new SQLiteCommand(con);
                command.CommandText = query;
                command.Parameters.AddWithValue("@MovieName", movie.MovieName);
                command.Parameters.AddWithValue("@Author", movie.Director);
                command.Parameters.AddWithValue("@ReleaseDate", movie.ReleaseDate);
                command.Parameters.AddWithValue("@IMDB", movie.Imdb);
                command.Parameters.AddWithValue("@Category", movie.Category);
                command.Parameters.AddWithValue("@Duration", movie.Duration);
                command.Parameters.AddWithValue("@StockCount", movie.StockCount);
                command.Parameters.AddWithValue("@Price", movie.Price);
                command.Parameters.AddWithValue("@ID", movie.MovieID);
                command.ExecuteNonQuery();
            }
        }
        
        public static string[] SifreKontrol(String connectionString, string username)
        {
            string[] val = { "", "" };
            string query = "select * from users where username='" + username + "'";
            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, con))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        val[0] = reader["password"].ToString();
                        val[1] = reader["user_type"].ToString();
                        return val;
                    }
                    else
                    {
                        return val;
                    }
                }
            }
        }

        public static DataSet MovieQuery(String connectionString)
        {
            string query = "select * from movies";
            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, con))
                {
                    DataSet dataSet = new DataSet();
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, con);
                    dataAdapter.Fill(dataSet);
                    con.Close();
                    return dataSet;
                }
            }
        }

        public static void UyeEkle(string connectionString, string userName, string password)
        {
            string query = "INSERT INTO users (username, password, user_type) VALUES (@username, @password, 1)";
            string hashedPass = Security.Hash(password);

            using (var con = new SQLiteConnection(connectionString))
            {
                con.Open();
                SQLiteCommand command = new SQLiteCommand(con);
                command.CommandText = query;
                command.Parameters.AddWithValue("@username", userName);
                command.Parameters.AddWithValue("@password", hashedPass);

                command.ExecuteNonQuery();
            }
        }
    }
}
