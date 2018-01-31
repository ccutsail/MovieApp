using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Entities
{
    [Table("filminfo")]
    public class FilmInfo
    {
        public string Rating { get; set; }
        public string Title { get; set; }
        public string ReleaseYear { get; set; }

    }

}