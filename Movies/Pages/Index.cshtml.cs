using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Movie> Movies;

        [BindProperty]
        public string search { get; set; }

        [BindProperty]
        public List<string> mpaa { get; set; } = new List<string>();

        [BindProperty]
        public float? minIMDB { get; set; }

        [BindProperty]
        public float? maxIMDB { get; set; }

        [BindProperty]
        public string sort { get; set; }

        public void OnGet()
        {
            Movies = MovieDatabase.All.OrderBy(movie => movie.Title);
        }

        public void OnPost()
        {
            Movies = MovieDatabase.All;

            if (search != null)
            {
                //Movies = MovieDatabase.Search(Movies, search);

                Movies = Movies.Where(movie => movie.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase) || 
                (movie.Director != null && movie.Director.Contains(search, StringComparison.CurrentCultureIgnoreCase)));
            }

            if(mpaa.Count != 0)
            {
                //Movies = MovieDatabase.FilterByMPAA(Movies, mpaa);

                Movies = Movies.Where(movie => mpaa.Contains(movie.MPAA_Rating));
            }

            if(minIMDB != null)
            {
                //Movies = MovieDatabase.FilterByMinIMDB(Movies, (float)minIMDB);

                Movies = Movies.Where(movie => movie.IMDB_Rating != null && movie.IMDB_Rating >= minIMDB);
            }

            if (maxIMDB != null)
            {
                Movies = Movies
                    .Where(movie => movie.IMDB_Rating != null)
                    .Where(movie => movie.IMDB_Rating <= maxIMDB);
            }
            
            if (sort != null)
            {
                Movies = Movies.OrderBy(movie => 
                { 
                    switch (sort)
                    {
                        case "title":
                            return movie.Title;
                        case "director":
                            if (movie.Director == null)
                            {
                                return "";
                            }
                            else
                            {
                                string[] parts = movie.Director.Split(" ");
                                string name = parts[parts.Length - 1];
                                if (parts.Length > 1)
                                {
                                    name += ",";

                                    for (int i = 0; i < parts.Length - 1; i++)
                                    {
                                        name += " " + parts[i];
                                    }
                                }
                                return name;
                            }
                        case "year":
                            return movie.Release_Year;
                        case "imdb":
                            return movie.IMDB_Rating.ToString();
                        case "rt":
                            return movie.Rotten_Tomatoes_Rating.ToString();
                        default:
                            return movie.Title;
                    }
                });
            }
        }
    }
}
