using MovieApp.Entities;
using ConsoleTables;
using System.Linq;

namespace MovieApp
{

    public class joinFuncs
    {

        public static void linqJoin()
        {
            var ratings = new[] {
                new { Code = "G", Name = "General Audiences"},
                new { Code = "PG", Name = "Parental Guidance Suggested"},
                new { Code = "PG-13", Name = "Parents Strongly Cautioned"},
                new { Code = "R", Name = "Restricted"},
                };

            var films = (from f in MoviesContext.Instance.Films
                        join r in ratings on f.RatingCode equals r.Code
                        select new { f.Title, r.Code, r.Name });
            ConsoleTable.From(films).Write();

        }

        public static void lambdaJoin()
        {
            var ratings = new[] {
                new { Code = "G", Name = "General Audiences"},
                new { Code = "PG", Name = "Parental Guidance Suggested"},
                new { Code = "PG-13", Name = "Parents Strongly Cautioned"},
                new { Code = "R", Name = "Restricted"},
                };

            var films = MoviesContext.Instance.Films.Join(ratings,
                        f => f.RatingCode,
                        r => r.Code,
                        (f, r) => new { f.Title, r.Code, r.Name });
            ConsoleTable.From(films).Write();
        }


    }





}