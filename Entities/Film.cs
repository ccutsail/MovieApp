using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Entities
{
    public partial class Film
    {
        public Film()
        {
            FilmActor = new HashSet<FilmActor>();
            FilmCategory = new HashSet<FilmCategory>();
        }

        public int FilmId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ReleaseYear { get; set; }
        public string RatingCode { get; set; }
        public int? RatingID { get; set; }
        [ForeignKey(nameof(RatingID))]
        public Rating Rating { get; set; }
        
        public double RunTime { get; set; }

        public ICollection<FilmActor> FilmActor { get; set; }
        public ICollection<FilmCategory> FilmCategory { get; set; }
        public int? FilmImageId { get; set; }
        [ForeignKey(nameof(FilmImageId))]
        public FilmImage FilmImage { get; set; }
    }
}
