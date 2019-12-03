using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies
{
    public class Movie
    {
        public string Title { get; set; }

        public string Director { get; set; }

        public string MPAA_Rating { get; set; }

        public string Major_Genre { get; set; }

        public float? IMDB_Rating { get; set; }

        public float? Rotten_Tomatoes_Rating { get; set; }

        public string Release_Date { get; set; }

        public string Release_Year
        {
            get
            {
                string[] parts = Release_Date.Split(" ");
                return parts[parts.Length - 1];
            }
        }
    }
}
