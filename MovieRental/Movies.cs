using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental
{
    public class Movies
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public string Director { get; set; }
        public int ReleaseDate { get; set; }
        public double Imdb { get; set; }
        public string Category { get; set; }
        public int Duration { get; set; }
        public int StockCount { get; set; }

        public Movies(int movieID, string movieName, string director, int releaseDate, double imdb, string category, int duration, int stockCount)
        {
            this.MovieID = movieID;
            this.MovieName = movieName;
            this.Director = director;
            this.ReleaseDate = releaseDate;
            this.Imdb = imdb;
            this.Category = category;
            this.Duration = duration;
            this.StockCount = stockCount;
        }

        public Movies()
        {

        }
    }
}
