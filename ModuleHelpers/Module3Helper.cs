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
                        Console.WriteLine($"\t{film.FilmID}\t{film.ReleaseYear}\t{film.Title}");
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

            var filmId = filmsQuery.Skip(skip).First().FilmID;

            var film2 = MoviesContext.Instance.Films.First(f => f.FilmID == filmId);
            var rating2 = MoviesContext.Instance.Rating.FirstOrDefault(r => r.RatingId == film2.RatingID);
            Console.WriteLine($"{film2.FilmID}\t{film2.Title}\t{rating2.Code}\t{rating2.Name}");

            }            
        }

        public static void ManyToManySelect()
        {
            var films = MoviesContext.Instance.Films
                .Include(f => f.FilmActor)
                .ThenInclude(a => a.Actor);

            foreach(var film in films)
            {
                Console.WriteLine(new string('-',78));
                Console.WriteLine($"{film.FilmID}\t{film.ReleaseYear}\t{film.Title}");
                Console.WriteLine(new string('-',78));
                foreach(var fa in film.FilmActor)
                {
                    Console.WriteLine($"{fa.ActorId}\t{fa.Actor.FirstName}\t{fa.Actor.LastName}");

                }

            }

            var actors = MoviesContext.Instance.Actors
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .Include(a => a.FilmActor)
                .ThenInclude(f => f.Film);
            foreach(var actor in actors)
            {
                Console.WriteLine(new string('-',78));
                Console.WriteLine($"{actor.LastName}, {actor.FirstName}");
                Console.WriteLine(new string('-',78));

                foreach(var fa in actor.FilmActor){
                    Console.WriteLine($"{fa.FilmId}\t{fa.Film.ReleaseYear}\t{fa.Film.Title}");
                }
            }
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