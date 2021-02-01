using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;
namespace Vidly.viewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<GenreType> GenreType { get; set; }

        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte? GenreTypeId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleseDate { get; set; }


        [Required]
        [Display(Name = "Number In Stock")]
        public int? NumberInStock { get; set; }


        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            GenreTypeId = movie.GenreTypeId;
            ReleseDate = movie.ReleseDate;
            NumberInStock = movie.NumberInStock;
        }
    }
}