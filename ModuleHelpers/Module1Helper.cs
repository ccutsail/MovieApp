using System;
using System.Linq;
using ConsoleTables;
using MovieApp.Entities;
using MovieApp.Extensions;
using MovieApp.Models;


namespace MovieApp
{
    public static class Module1Helper
    {
        internal static void SelectList()
        {
            var movie = MoviesContext.Instance.Films.Select(m => m.Copy<Film,MovieModel>());
            var actors = MoviesContext.Instance.Actors.Select(a => a.Copy<Actor,ActorModel>());
            var categories = MoviesContext.Instance.FilmCategories;
            Console.WriteLine(actors.Count(a => a.FirstName == "Robert"));
            ConsoleTable.From(actors).Write();
            ConsoleTable.From(movie).Write();

            
        }

        internal static void SelectById()
        {
            int begin = 0;
            while(begin == 0){
            Console.WriteLine("Do you want to select an actor? Or a movie?");
            Console.WriteLine("To select an actor, enter 1, for a movie, enter 2.");
            int newvar = Console.ReadLine().ToInt(); 
            
                if(newvar == 1){
                    Console.WriteLine("Enter actor ID: ");
                    int actorid = Console.ReadLine().ToInt();
                    var actor = MoviesContext.Instance.Actors.SingleOrDefault(a => a.ActorId == actorid);
                    if(actor==null){
                        Console.WriteLine($"No actor could be found with ID:{actorid}!");
                    }
                    else{
                        Console.WriteLine($"Actor ID: {actor.ActorId}       Actor Name: {actor.FirstName} {actor.LastName}");
                    }
                    begin = 1;
                }
                else if(newvar==2){
                    Console.WriteLine("Enter part of the film name: ");
                    string moviename = Console.ReadLine();
                    var mv = MoviesContext.Instance.Films.FirstOrDefault(m=>m.Title.Contains(moviename));
                    if(mv==null){
                        Console.WriteLine($"No movie titles contain: {moviename}!");
                    }
                    else{
                        Console.WriteLine($"ID: {mv.FilmId}  Title: {mv.Title}  Year: {mv.ReleaseYear}  Rating: {mv.Rating}");
                    }
                    begin = 1;

                }
            }
        }
        internal static void CreateItem()
        {
            Console.WriteLine("Enter actor last name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter actor first name: ");
            string firstName = Console.ReadLine();
            Actor actor = new Actor {FirstName = firstName, LastName = lastName};
            MoviesContext.Instance.Actors.Add(actor);
            MoviesContext.Instance.SaveChanges();
            var actors = MoviesContext.Instance.Actors
                        .Where(a => a.ActorId == actor.ActorId)
                        .Select(a => a.Copy<Actor, ActorModel>());
            ConsoleTable.From(actors).Write();

        Console.WriteLine("Add a Film");

    	Console.WriteLine("Enter a Title");
    	var title = Console.ReadLine();

    	Console.WriteLine("Enter a Description");
    	var description = Console.ReadLine();

    	Console.WriteLine("Enter a Release Year");
    	var year = Console.ReadLine().ToInt();

    	Console.WriteLine("Enter a Rating");
    	var rating = Console.ReadLine();

    	var film = new Film {Title = title, Description = description, ReleaseYear = year, Rating = rating};

        MoviesContext.Instance.Films.Add(film);
        MoviesContext.Instance.SaveChanges();
        var films = MoviesContext.Instance.Films.Where(f => f.Title == film.Title).Select(f => f.Copy<Film,MovieModel>());
        ConsoleTable.From(films).Write();

        }

        internal static void UpdateItem()
        {   
            Console.WriteLine("Enter Actor ID to edit: ");
            int actorid = Console.ReadLine().ToInt(); 
            var actor = MoviesContext.Instance.Actors.SingleOrDefault(a=>a.ActorId == actorid);
            if(actor!=null){
                Console.WriteLine("What property would you like to update? Enter 'first' for First Name, or ");
                Console.WriteLine("'last' for Last Name.");
                string t = Console.ReadLine();
                
                if(t == "first")
                {
                    Console.WriteLine("Enter new first name: ");
                    string newName = Console.ReadLine();
                    actor.FirstName = newName;
                    ConsoleTable.From(MoviesContext.Instance.Actors).Write();
                }
                if(t == "last")
                {
                    Console.WriteLine("Enter new last name: ");
                    string newName = Console.ReadLine();
                    actor.LastName = newName;
                    ConsoleTable.From(MoviesContext.Instance.Actors).Write();
                }
                else{Console.WriteLine("Ok then, Fuck off!");}

            }
            else{Console.WriteLine("That actor doesn't exist!");}
        }
        
        internal static void DeleteItem()
        {
            Console.WriteLine(nameof(DeleteItem));
        }
    }
}