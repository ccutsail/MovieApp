using System;
using System.Linq;
using MovieApp.Entities;

namespace MovieApp
{
    public static class Grouper
    { 
        public static void grouping_func()
        {
            var filmGroups = MoviesContext.Instance.Films
                            .GroupBy(f => f.RatingCode);
            foreach (var filmGroup in filmGroups)
            {
                Console.WriteLine(filmGroup.Key);
                foreach (var film in filmGroup.OrderBy(f => f.Title))
                {
                    Console.WriteLine($"\t{film.Title}");
                }
            }
        }
    }

}