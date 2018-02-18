using System;
using System.Linq;
using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using MovieApp.Entities;
using MovieApp.Extensions;
using MovieApp.Models;

namespace MovieApp
{
    public static class Module3Helper
    {
        public static void OneToOne()
        {
            // var films = MoviesContext.Instance.Films.Include(f => f.FilmImageId)
            //                 .Select(CreateFilmDetailModel);
            // ConsoleTable.From(films).Write();
            
            var films = MoviesContext.Instance.FilmImages.Include(i => i.Film)
                            .Select(CreateFilmDetailModel);
            ConsoleTable.From(films).Write();
        }

        public static void OneToMany()
        {
            var ratings = MoviesContext.Instance.Rating.Include(r => r.Films);
            foreach(var rating in ratings)
            {
               Console.WriteLine(new string('-', 78));
                Console.WriteLine($"{rating.Code}\t{rating.Name}");
                Console.WriteLine(new string('-', 78));
                if (rating.Films.Any())
                {
                    Console.WriteLine($"\tID\tYear\tTitle");
                    Console.WriteLine($"\t{new string('-', 70)}");
                    foreach (var film in rating.Films.OrderByDescending(f=>f.ReleaseYear))
                    {
                        Console.WriteLine($"\t{film.FilmId}\t{film.ReleaseYear}\t{film.Title}");
                    }
                }
                else
                {
                    Console.WriteLine("\tNo Films");
                }
            Console.WriteLine();
            Console.WriteLine(new string('-', 78));
            var filmsQuery = MoviesContext.Instance.Films;
            int skip = new Random().Next(0, filmsQuery.Count());

            var filmId = filmsQuery.Skip(skip).First().FilmId;

            var film2 = MoviesContext.Instance.Films.First(f => f.FilmId == filmId);
            var rating2 = MoviesContext.Instance.Rating.FirstOrDefault(r => r.RatingId == film2.RatingID);
            Console.WriteLine($"{film2.FilmId}\t{film2.Title}\t{rating2.Code}\t{rating2.Name}");

            }            
        }

        public static void ManyToManySelect()
        {
            Console.WriteLine(nameof(ManyToManySelect));
        }

        public static void ManyToManyInsert()
        {
            Console.WriteLine(nameof(ManyToManyInsert));
        }

        public static void ManyToManyDelete()
        {
            Console.WriteLine(nameof(ManyToManyDelete));
        }

        private static FilmDetailModel CreateFilmDetailModel(Film film)
        {
            var model = film.Copy<Film, FilmDetailModel>();

            if (film.FilmImage != null)
            {
                model.FilmImageId = film.FilmImage.FilmImageId;
            }

            return model;
        }
        private static FilmDetailModel CreateFilmDetailModel(FilmImage image)
        {
            var model = image.Film.Copy<Film, FilmDetailModel>();

            model.FilmImageId = image.FilmImageId;

            return model;
        }
    }
    
}