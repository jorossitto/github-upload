using System;
using System.Linq;
using System.Collections.Generic;
using Common;

namespace ACM.BL
{
    public class Movie 
    {
        public string Title { get; set; }
        public float Rating { get; set; }
        int _year;
        public int Year
        {
            get
            {
                //throw new Exception("Error!");
                Console.WriteLine($"Returning {_year} for {Title}");
                return _year;
            }

            set
            {
                _year = value;
            }
        }

        public static void Main()
        {
            string branch;
            Console.WriteLine("Enter 1 to Read Stock Data, 2 to ConvertLocaltoSidney");

            branch = Console.ReadLine();
            if (branch == "1")
            {
                PrintMovies();

            }
            else if (branch == "2")
            {
                //ConvertLocalToSidney();
            }

            Console.ReadLine();
        }

        private static void PrintMovies()
        {
            var movies = CreateTestMovieList();

            //var query = Enumerable.Empty<Movie>();

            try
            {
                var query = movies.Filter(m => m.Year > 2000);

                foreach (var movie in query)
                {
                    Console.WriteLine(movie.Title);
                }

                PrintHelper.LineBreak();

                query = movies.Where(m => m.Year > 2000).OrderByDescending(m => m.Rating);

                foreach (var movie in query)
                {
                    Console.WriteLine(movie.Title);
                }

                PrintHelper.LineBreak();

                Console.WriteLine(query.Count());

                var enumerator = query.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Console.WriteLine(enumerator.Current.Title);
                }
                PrintHelper.LineBreak();
                query = from movie in movies
                        where movie.Year > 200
                        orderby movie.Rating descending
                        select movie;

                foreach (var movie in query)
                {
                    Console.WriteLine(movie.Title);
                }
                PrintHelper.LineBreak();
                var numbers = UsingLinq.Random().Where(n => n > .5).Take(10);
                foreach(var number in numbers)
                {
                    Console.WriteLine(number);
                }
                PrintHelper.LineBreak();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static List<Movie> CreateTestMovieList()
        {
            var movies = new List<Movie>
            {
                new Movie {Title = "The Dark Knight", Rating = 8.9f, Year = 2008},
                new Movie {Title = "The Kings Speach", Rating = 8.0f, Year = 2010},
                new Movie {Title = "Casablanca", Rating = 8.5f, Year = 1942},
                new Movie {Title = "Star Wars V", Rating = 8.7f, Year = 1980}
            };

            return movies;
        }
    }
}
