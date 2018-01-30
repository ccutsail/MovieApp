using System;
using System.Linq;
using System.Linq.Expressions;
using ConsoleTables;
using MovieApp.Entities;
using MovieApp.Extensions;
using MovieApp.Models;

namespace MovieApp
{
    public static class Module2Helper
    {
        public static void Sort()
        {
            var actors = MoviesContext.Instance.Actors
                .OrderBy(a => a.LastName)
                .Select(a => a.Copy<Actor, ActorModel>());
                ConsoleTable.From(actors).Write();
            var films = MoviesContext.Instance.Films
                .OrderBy(f => f.Rating)
                .ThenBy(f => f.ReleaseYear)
                .ThenBy(f => f.Title)
                .Select(f => f.Copy<Film, MovieModel>());
                ConsoleTable.From(films).Write();
        }
        public static void SortDescending()
        {
            var actors = MoviesContext.Instance.Actors
                .OrderByDescending(a => a.FirstName)
                .Select(a => a.Copy<Actor, ActorModel>());
            ConsoleTable.From(actors).Write();
            var films = MoviesContext.Instance.Films
                .OrderByDescending(f => f.ReleaseYear)
                .ThenByDescending(f => f.Rating)
                .Select(f => f.Copy<Film, MovieModel>());
            ConsoleTable.From(films).Write();
        }

        public static void Skip()
        {
            var films = MoviesContext.Instance.Films.Select(f => f.Copy<Film, MovieModel>());
            ConsoleTable.From(films).Write();
            var films2 = MoviesContext.Instance.Films.Skip(5).Select(f => f.Copy<Film, MovieModel>());
            ConsoleTable.From(films2).Write();

        }
        public static void Take()
        {
            var actors = MoviesContext.Instance.Actors
                .Take(5)
                .OrderBy(a => a.FirstName)
                .ThenByDescending(a => a.LastName)
                .Select(a => a.Copy<Actor, ActorModel>());
            ConsoleTable.From(actors).Write();
            var actors2 = MoviesContext.Instance.Actors
                .OrderBy(a => a.FirstName)
                .ThenByDescending(a => a.LastName)
                .Take(5)
                .Select(a => a.Copy<Actor, ActorModel>());
            ConsoleTable.From(actors2).Write();
        }
        public static void Paging()
        {
            Console.WriteLine("Enter a page size:");
            var pageSize = Console.ReadLine().ToInt();

            Console.WriteLine("Enter a page number:");
            var pageNumber = Console.ReadLine().ToInt();

            Console.WriteLine("Enter a sort column:");
            Console.WriteLine("\ti - Film ID");
            Console.WriteLine("\tt - Title");
            Console.WriteLine("\ty - Year");
            Console.WriteLine("\tr - Rating");
            var key = Console.ReadKey();

            Console.WriteLine();

            var films = MoviesContext.Instance.Films
                        .OrderBy(GetSort(key))
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .Select(f => f.Copy<Film, MovieModel>());
            ConsoleTable.From(films).Write();
        }
        public static void LinqBasics()
        {
            var actors = (from a in MoviesContext.Instance.Actors 
                        orderby a.FirstName
                        select a.Copy<Actor,ActorModel>());
            ConsoleTable.From(actors).Write();
        }

        public static void LinqBasics(int actorid)
        {
            var actors = (from a in MoviesContext.Instance.Actors
                where a.ActorId == actorid
                select a.Copy<Actor, ActorModel>());
            ConsoleTable.From(actors).Write();
        }

        public static void LinqBasics(string namesub)
        {
            var actors  = (from a in MoviesContext.Instance.Actors
                            where a.FirstName.Contains(namesub)
                            orderby a.FirstName
                            select a.Copy<Actor,ActorModel>());
            ConsoleTable.From(actors).Write();
        }

        public static void LambdaBasics()
        {
            var films = MoviesContext.Instance.Films
                        .OrderBy(f => f.Title)
                        .Select(f => f.Copy<Film,MovieModel>());
            ConsoleTable.From(films).Write();
        }

        public static void LambdaBasics(string FilmName)
        {
            var films = MoviesContext.Instance.Films
                        .Where(f => f.Title.Contains(FilmName))
                        .OrderBy(f => f.Title)
                        .Select(f => f.Copy<Film, MovieModel>());
            ConsoleTable.From(films).Write();
        }

        private static Expression<Func<Film, object>> GetSort(ConsoleKeyInfo info)
        {
            switch (info.Key)
            {
                case ConsoleKey.I:
                    return f => f.FilmId;
                case ConsoleKey.Y:
                    return f => f.ReleaseYear;
                case ConsoleKey.R:
                    return f => f.Rating;
                default:
                    return f => f.Title;
            }
        }

        public static void MigrationAddCol()
        {
            var film = MoviesContext.Instance.Films.Last();

            if(film != null) 
            {
                Console.WriteLine($"Updating runtime for {film.Title}");
                film.RunTime = 121;
                MoviesContext.Instance.SaveChanges();
            } 

            var films = MoviesContext.Instance.Films
                .OrderByDescending(f => f.RunTime)
                .Select(f => f.Copy<Film, MovieModel>());
            ConsoleTable.From(films).Write();

        }
        public static void MigrationAddTable()
        {
            var user = new ApplicationUser{
                UserName = "testuser",
                InvalidLoginAttempts = 0
            };

            MoviesContext.Instance.Users.Add(user);
            MoviesContext.Instance.SaveChanges();

            var users = MoviesContext.Instance.Users;
            ConsoleTable.From(users).Write();

        }

    }
    
}

