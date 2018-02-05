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
            // var films = MoviesContext.Instance.Films.Include(i => f.FilmImageId)
            //                 .Select(CreateFilmDetailModel);
            // ConsoleTable.From(films).Write();
            // ConsoleTable.From(films).Write();
            var films = MoviesContext.Instance.FilmImages.Include(i => i.Film)
                            .Select(CreateFilmDetailModel);
            ConsoleTable.From(films).Write();
        }

        public static void OneToMany()
        {
            Console.WriteLine(nameof(OneToMany));
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